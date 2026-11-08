using System.IO;
using Microsoft.Extensions.Primitives;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    int? firstNumber = null;
    int? secondNumber = null;
    if (context.Request.Query.ContainsKey("firstNumber"))
    {
        firstNumber = int.TryParse(context.Request.Query["firstNumber"], out int result) ? result : null;
    }
    if (context.Request.Query.ContainsKey("secondNumber"))
    {
        secondNumber = int.TryParse(context.Request.Query["secondNumber"], out int result) ? result : null;
    }
    if (context.Request.Query.ContainsKey("operation"))
    {
        string operation = context.Request.Query["operation"];
        switch (operation.ToLower())
        {
            case "add":
                if (firstNumber.HasValue && secondNumber.HasValue)
                {
                    int sum = firstNumber.Value + secondNumber.Value;
                    await context.Response.WriteAsync($"The sum of {firstNumber} and {secondNumber} is: {sum}");
                }
                else
                {
                    await context.Response.WriteAsync("Please provide valid numbers for addition.");
                }
                break;
            case "subtract":
                if (firstNumber.HasValue && secondNumber.HasValue)
                {
                    int difference = firstNumber.Value - secondNumber.Value;
                    await context.Response.WriteAsync($"The difference between {firstNumber} and {secondNumber} is: {difference}");
                }
                else
                {
                    await context.Response.WriteAsync("Please provide valid numbers for subtraction.");
                }
                break;
            case "multiply":
                if (firstNumber.HasValue && secondNumber.HasValue)
                {
                    int product = firstNumber.Value * secondNumber.Value;
                    await context.Response.WriteAsync($"The product of {firstNumber} and {secondNumber} is: {product}");
                }
                else
                {
                    await context.Response.WriteAsync("Please provide valid numbers for multiplication.");
                }
                break;
            case "divide":
                if (firstNumber.HasValue && secondNumber.HasValue){
                    if (secondNumber.Value != 0)
                    {
                        double quotient = (double)firstNumber.Value / secondNumber.Value;
                        await context.Response.WriteAsync($"The quotient of {firstNumber} divided by {secondNumber} is: {quotient}");
                    }
                    else
                    {
                        await context.Response.WriteAsync("Division by zero is not allowed.");
                    }
                }
                else
                {
                    await context.Response.WriteAsync("Please provide valid numbers for division.");
                }
                break;
            default:
                await context.Response.WriteAsync("Invalid operation. Please use 'add' for addition.");
                break;
        }
    }
});

app.Run();
