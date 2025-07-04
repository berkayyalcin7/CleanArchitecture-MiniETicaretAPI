// Konum: Core/MiniETicaret.Application/Common/Behaviors/ValidationBehavior.cs
using FluentValidation;
using MediatR;

namespace MiniETicaret.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(); // Validator yoksa, bir sonraki adıma geç.
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            // Eğer validasyon hatası varsa, exception fırlat.
            throw new ValidationException(failures);
        }

        return await next(); // Validasyon başarılı, asıl Handler'a geç.
    }
}