var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(context => context.Request.Query.ContainsKey("username"),
app =>
{
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync($"Hello {context.Request.Query["username"]} from UseWhen Middleware branch\n");
        await next();
    });
});

app.Use(async (HttpContext context, RequestDelegate next)  =>
{
    await context.Response.WriteAsync("Hello from main branch middleware\n");
});
app.Run();
