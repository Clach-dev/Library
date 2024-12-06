using System.Text;

namespace Presentation.Common.Middleware;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var request = await FormatRequest(context.Request);
        logger.LogInformation("Incoming Request: {Request}", request);

        var originalResponseBody = context.Response.Body;
        
        using (var newResponseBody = new MemoryStream())
        {
            context.Response.Body = newResponseBody;

            await next(context);

            var response = await FormatResponse(context.Response);
            logger.LogInformation("Outgoing Response: {Response}", response);

            await newResponseBody.CopyToAsync(originalResponseBody);
        }
    }

    private async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();

        var body = string.Empty;
        if (request.ContentLength > 0)
        {
            using (var reader = new StreamReader(
                       request.Body,
                       Encoding.UTF8,
                       leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }
        }

        return $"Method: {request.Method}, Path: {request.Path}, Body: {body}";
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return $"Status Code: {response.StatusCode}, Body: {text}";
    }
}