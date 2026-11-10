var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Routing is automatically enabled.
//No need for app.UseRouting() anymore.

app.Map("map1", async (context) =>
{
    await context.Response.WriteAsync("In Map 1");
});

app.Map("map2", async (context) =>
{
    await context.Response.WriteAsync("In Map 2");
});

//Fallback for any other request
app.MapFallback(async (context) =>
{
    string path = context.Request.Path;
    await context.Response.WriteAsync($"Request received at {path}");
});

// When you create a middleware with app.Use, it gets executed before the routing middleware. 
// So, if you want to execute some logic before routing, you can use app.Use. 

app.Run();
