namespace core.Middleware;

/// <summary>
/// חוסם את כל הבקשות ומחזיר 503 כאשר החנות/המסעדה סגורה.
/// הערכים נקראים מ-appsettings.json תחת המקטע ShopSettings.
/// </summary>
public class ShopClosedMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ShopClosedMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // אם המפתח חסר — GetValue מחזיר false, כלומר "פתוח" (התנהגות בטוחה)
        var isShopClosed = _configuration.GetValue<bool>("ShopSettings:IsShopClosed");

        if (isShopClosed)
        {
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "text/plain; charset=utf-8";

            var message = _configuration.GetValue<string>("ShopSettings:ShopClosedMessage")
                          ?? "השירות אינו זמין כרגע.";

            await context.Response.WriteAsync(message);
            return; // לא קוראים ל-_next — הבקשה לא ממשיכה לקונטרולר
        }

        await _next(context);
    }
}

public static partial class MiddlewareExtensions
{
    /// <summary>
    /// רושם את ה-middleware בצינור הבקשות. יש לקרוא לו ב-Program.cs
    /// אחרי UseHttpsRedirection ולפני MapControllers.
    /// </summary>
    public static IApplicationBuilder UseShopClosed(this IApplicationBuilder builder)
        => builder.UseMiddleware<ShopClosedMiddleware>();
}
