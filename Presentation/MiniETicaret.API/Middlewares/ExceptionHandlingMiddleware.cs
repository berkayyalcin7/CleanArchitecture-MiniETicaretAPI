// Konum: Presentation/MiniETicaret.API/Middlewares/ExceptionHandlingMiddleware.cs
using FluentValidation;
using System.Text.Json;

namespace MiniETicaret.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex) // Sadece FluentValidation hatalarını yakala
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            var errorResponse = JsonSerializer.Serialize(new { errors });

            await context.Response.WriteAsync(errorResponse);
        }
        catch (Exception ex) // Diğer tüm hataları yakala
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = JsonSerializer.Serialize(new { message = "Sunucuda beklenmeyen bir hata oluştu." });

            await context.Response.WriteAsync(errorResponse);
        }
    }
}