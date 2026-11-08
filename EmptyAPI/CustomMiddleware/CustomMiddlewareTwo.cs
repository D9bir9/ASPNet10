namespace EmptyAPI.CustomMiddleware
{
    public class CustomMiddlewareTwo
    {
        private readonly RequestDelegate _next;
        public CustomMiddlewareTwo(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // before logic
            await context.Response.WriteAsync("Second Custom Middleware - Starts\n");
            await _next(context);
            // after logic
            await context.Response.WriteAsync("Second custom Middleware - Ends\n");
        }
    }

    public static class MiddleWareTwoExtension
    {
        public static IApplicationBuilder UseSecondMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddlewareTwo>();
        }
    }
}