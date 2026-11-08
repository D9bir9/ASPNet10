namespace EmptyAPI.CustomMiddleware
{
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            if (httpcontext.Request.Query.ContainsKey("firstname") && httpcontext.Request.Query.ContainsKey("lastname"))
            {
                string fullName =httpcontext.Request.Query["firstName"] + " " + httpcontext.Request.Query["lastName"];
                await httpcontext.Response.WriteAsync($"Hello {fullName} from Custom Middleware\n");
            }
            await _next(httpcontext);
        }
    }

    public static class HelloCustomMiddlewareExtension
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}