namespace EmptyAPI.CustomMiddleware
{
    public class Practice
    {
        private readonly RequestDelegate _next;
        public Practice(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("practice"))
            {
                int times = int.TryParse(context.Request.Query["practice"], out int result) ? result : 1;
                await context.Response.WriteAsync($"You've practiced {times} times!\n");
            }
            await _next(context);
        }
    }

    public static class PracticeMiddlewareExtension
    {
        public static IApplicationBuilder UsePracticeMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<Practice>();
        }
    }
}