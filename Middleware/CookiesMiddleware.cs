namespace ExpenseTracker.Middleware;

public class CookiesMiddleware
{
    private readonly RequestDelegate _next;
    public CookiesMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        if(context.Request.Cookies.TryGetValue("ClientId", out var id) && context.Request.Path.StartsWithSegments("/"))
        {
            context.Session.SetInt32("ClientId", Convert.ToInt32(id));
            context.Response.Redirect("/Client/ClientMenu");
        }
        
        await _next(context);
    }


}