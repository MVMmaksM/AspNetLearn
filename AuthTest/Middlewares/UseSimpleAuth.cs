namespace AuthTest.Middlewares;

public static class SimpleAuthExtension
{
    public static IApplicationBuilder UseSimpleAuth(this IApplicationBuilder builder, SimpleAuthOption option)
    {
        builder.UseMiddleware<AuthStatusCodeMiddleware>()
            .UseMiddleware<SimpleAuthMiddleware>(option);

        return builder;
    }
}