using System.Globalization;

namespace ExpenseTracker.Middleware;

public class LanguageMiddleware
{
    private readonly RequestDelegate _next;
    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var lang = context.Request.Cookies["lang"];
        if (!string.IsNullOrEmpty(lang))
        {
            // Установка выбранного языка
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            
        }

        await _next.Invoke(context);
    }
}