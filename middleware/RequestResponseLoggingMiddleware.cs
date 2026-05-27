using Serilog;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Log Request Body
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0; // Reset the stream position for the next middleware

        Log.Information(
            "Incoming Request: {method} {url} - Body: {body}",
            context.Request.Method,
            context.Request.Path,
            requestBody
        );

        //Capture Response Body
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;
        // Call the next middleware in the pipeline
        await _next(context);

        //Read response body
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin); // Reset the stream position for the

        // Log the outgoing response
        Log.Information(
            "Outgoing Response: {statusCode} - Body: {body}",
            context.Response.StatusCode,
            responseBodyText
        );

        await responseBody.CopyToAsync(originalBodyStream); // Copy the response back to the original stream
    }
}
