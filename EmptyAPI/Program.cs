using System.IO;
using Microsoft.Extensions.Primitives;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//The extension method "Use" is used to execute a non-terminating / short-circuiting middleware that may / may not forward the request to the next middleware.
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    // Code
    await next(context);
    // after code
});

// One of the method to create a middleware is to use the Run method. 
// The Run method is a terminal middleware that processes the request and generates a response. 
// It does not call the next middleware in the pipeline, so it should be used at the end of the middleware chain.
app.Run(async (HttpContext context) =>
{
    // Code
});