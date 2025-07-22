namespace AuthTest.Middlewares;

public class SimpleAuthMiddleware
{
    private SimpleAuthOption _authOptions;
    private readonly RequestDelegate _next;

    public SimpleAuthMiddleware(RequestDelegate next, SimpleAuthOption authOption)
    {
        _next = next;
        _authOptions = authOption;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (TryGetToken(context, out string token))
        {
            if (IsValidToken(token))
                await _next(context);
            else
            {
                context.Response.StatusCode = 419;
            }
        }
        else
        {
            context.Response.StatusCode = 401;
        }
    }

    private bool IsValidToken(string token)
    {
        return _authOptions.ValidTokens.Contains<string>(token);
    }

    private bool TryGetToken(HttpContext context, out string token)
    {
        switch (_authOptions.TokenPosition)
        {
            case TokenPosition.OnlyHeader:
            {
                token = context.Request.Headers["token"];
                break;
            }
            case TokenPosition.OnlyQuery:
            {
                token = context.Request.Query["token"];
                break;
            }
            case TokenPosition.QueryAndHeader:
            {
                token = context.Request.Query["token"];

                if (string.IsNullOrWhiteSpace(token))
                {
                    token = context.Request.Headers["token"];
                }

                break;
            }
            default:
            {
                token = String.Empty;
                break;
            }
        }

        return string.IsNullOrWhiteSpace(token) == false;
    }
}