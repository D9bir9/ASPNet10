var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Routing is automatically enabled.
//No need for app.UseRouting() anymore.

// E.g: files/sample.txt
app.Map("files/{filename}.{extension=txt}", async context =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"In files - {fileName}.{extension}");
});

// E.g: employee/profile/john
app.Map("employee/profile/{employeeName=scott}", async context =>
{
    string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
    await context.Response.WriteAsync($"In employee profile - {employeeName}");
});

// E.g: product/details/1
app.Map("product/details/{productId:int=1}", async context =>
{
    int productId = Convert.ToInt32(context.Request.RouteValues["productid"]);
    await context.Response.WriteAsync($"In product details - {productId}");
});

//Fallback for any other request
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

// When you create a middleware with app.Use, it gets executed before the routing middleware. 
// So, if you want to execute some logic before routing, you can use app.Use. 

app.Run();
