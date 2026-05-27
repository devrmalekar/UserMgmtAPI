using Serilog;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string AUTH_HEADER = "Authorization";
    private const string VALID_TOKEN = "mysecrettoken123"; // In real applications, use a secure token management strategy

    public TokenAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Check if Authorazation header is present
        if (!context.Request.Headers.TryGetValue(AUTH_HEADER, out var token))
        {
            Log.Warning("Unauthorized request: Missing Authorization header.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Missing Authorization header.");
            return;
        }

        //Validate token
        if (!token.ToString().Equals($"Bearer {VALID_TOKEN}", StringComparison.OrdinalIgnoreCase))
        {
            Log.Warning("Unauthorized request: Invalid token.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Invalid token.");
            return;
        }

        await _next(context);
    }
}
