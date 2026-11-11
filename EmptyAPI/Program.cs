var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Routing is automatically enabled.
//No need for app.UseRouting() anymore.

// E.g: files/sample.txt
app.Map("files/{filename}.{extension}", async context =>
{
    string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
    string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
    await context.Response.WriteAsync($"In files - {fileName}.{extension}");
});

// E.g: employee/profile/john
app.Map("employee/profile/{employeeName?}", async context =>
{
    if (context.Request.RouteValues.ContainsKey("employeeName")){
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
        await context.Response.WriteAsync($"In employee profile - {employeeName}");
    }
    else
    {
        await context.Response.WriteAsync($"In employee profile - No employee name provided");
    }
});

// E.g: product/details/1
app.Map("product/details/{id?}", async context =>
{
    if (context.Request.RouteValues.ContainsKey("id")){
    int id = Convert.ToInt32(context.Request.RouteValues["id"]);
    await context.Response.WriteAsync($"In product details - {id}");
    }
    else
    {
        await context.Response.WriteAsync($"In product details - No ID provided");
    }
});
// E.g: daily-digest-report/{reportdate}
app.Map("daily-digest-report/{reportdate:datetime}", async context =>
{
    DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
    await context.Response.WriteAsync($"In daily digest report - {reportDate.ToShortDateString()}");
});
//Fallback for any other request
app.MapFallback(async (context) =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

// E.g: cities/{cityid}
app.Map("cities/{cityid:guid}", async context =>
{
    Guid cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"]));
    await context.Response.WriteAsync($"City information - {cityId}");
});

// When you create a middleware with app.Use, it gets executed before the routing middleware. 
// So, if you want to execute some logic before routing, you can use app.Use. 

app.Run();
