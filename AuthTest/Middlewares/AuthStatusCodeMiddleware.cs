namespace AuthTest.Middlewares;

public class AuthStatusCodeMiddleware
{
    private readonly RequestDelegate _next;

    public AuthStatusCodeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (!context.Response.HasStarted)
            context.Response.ContentType = "text/plain; charset=utf-8";

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized:
                context.Response.WriteAsync("Ошибка авторизации. Нет токена доступа");
                break;
            case StatusCodes.Status419AuthenticationTimeout:
                context.Response.WriteAsync("Ошибка авторизации. Токен недействителен");
                break;
            default:
                break;
        }
    }
}