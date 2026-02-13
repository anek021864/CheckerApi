namespace JigNetApi;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;

        _apiKey = config["Auth:ApiKey"] ?? "";
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // เปิด /health โดยไม่ต้องใช้คีย์
        if (context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }

        // อ่าน metadata ของ endpoint ปัจจุบัน
        var endpoint = context.GetEndpoint();

        // ถ้า endpoint ไม่มี [ApiKeyRequired] → ปล่อยผ่าน
        var requiresApiKey = endpoint?.Metadata.GetMetadata<ApiKeyRequiredAttribute>() != null;
        if (!requiresApiKey)
        {
            await _next(context);
            return;
        }

        if (
            !context.Request.Headers.TryGetValue("X-API-KEY", out var provided)
            || provided != _apiKey
        )
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        await _next(context);
    }
}
