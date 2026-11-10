using Microsoft.Extensions.Primitives;

namespace EmptyAPI
{
    public class LoginSystem
    {
        private readonly string USERMAIL = "admin@example.com";
        private readonly string PASSWORD = "admin1234";
        private readonly RequestDelegate _next;
        public LoginSystem(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(context.Request.Body);
            string requestBody = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> loginpairs = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(requestBody);
            if (loginpairs.ContainsKey("email") && loginpairs.ContainsKey("password"))
            {
                string email = loginpairs["email"][0].ToString();
                string password = loginpairs["password"][0].ToString();

                if (email == USERMAIL && password == PASSWORD)
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Successful Login!\n");
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Login\n");
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                if (!loginpairs.ContainsKey("email"))
                {
                    await context.Response.WriteAsync("Invalid input for 'email'\n");
                }
                if (!loginpairs.ContainsKey("password"))
                {
                    await context.Response.WriteAsync("Invalid input for 'password'\n");
                }
            }
        }
    }

    public static class LoginSystemExtension
    {
        public static IApplicationBuilder UseLoginSystem(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoginSystem>();
        }
    }
}