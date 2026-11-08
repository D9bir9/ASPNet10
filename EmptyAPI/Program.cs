using EmptyAPI.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<CustomMiddlewareClass>();

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("From Middleware 1");
    await next(context);
});

app.UseMiddleware<CustomMiddlewareClass>();

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("From Middleware 3");
});

app.Run();
