using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AuthTest.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;

namespace AuthTest;

public class Program
{
    public async static Task Main(string[] args)
    {
        string[] valid_tokens = ["123", "token", "456"];
        
        var httpPort = 5300;
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.ConfigureKestrel(options => { options.ListenAnyIP(httpPort); });

        builder.Services.AddControllers();
        
        var app = builder.Build();

        app.UseSimpleAuth(new SimpleAuthOption()
        {
            ValidTokens = valid_tokens,
            TokenPosition = TokenPosition.QueryAndHeader
        });

        app.MapGet("/", async (context) =>
        {
            var response = new
            {
                connection_id = context.Connection.Id
            };

            await context.Response.WriteAsJsonAsync(response);
        });

        app.MapControllers();
        app.Lifetime.ApplicationStarted.Register(() => Console.WriteLine("App started"));
        app.Lifetime.ApplicationStopped.Register(() => Console.WriteLine("App started stopped"));
        app.Lifetime.ApplicationStopping.Register(() => Console.WriteLine("App started stopping"));

        await app.StartAsync();
        app.Logger.LogInformation($"App start, port: {httpPort}");
        await app.WaitForShutdownAsync();
    }
}