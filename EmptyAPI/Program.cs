using EmptyAPI;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(context => context.Request.Method == "POST", app =>
{
    app.UseLoginSystem();
});
app.Use(async (context, next) =>
{
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("No response\n");
    await next(context);
});

app.Run();
