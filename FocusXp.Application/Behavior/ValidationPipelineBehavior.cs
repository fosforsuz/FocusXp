using FluentResults;
using FluentValidation;
using MediatR;

namespace FocusXp.Application.Behavior;

/// <summary>
///     A behavior that validates the request using FluentValidation.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // If there are no validators, just continue
        if (!_validators.Any())
            return await next();

        // Validate the request
        var context = new ValidationContext<TRequest>(request);
        var validationResults = _validators
            .Select(v => v.Validate(context))
            .Where(r => !r.IsValid)
            .ToList();

        // If the request is valid, continue
        if (!validationResults.Any())
            return await next();

        // If the request is invalid, return a failed result
        var errors = validationResults
            .SelectMany(r => r.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        var errorMessage = string.Join(", ", errors);

        // Create a failed result
        if (typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
            throw new InvalidOperationException("ValidationBehavior can only be used with Result<T> types.");


        var resultType = typeof(TResponse).GetGenericArguments()[0];
        var failMethod = typeof(Result)
            .GetMethods()
            .Where(m => m.Name == nameof(Result.Fail) && m.IsGenericMethod)
            .FirstOrDefault(m => m.GetParameters().Length == 1 &&
                                 m.GetParameters()[0].ParameterType == typeof(string));

        if (failMethod == null)
            throw new InvalidOperationException("Could not find a suitable Result.Fail<T>(string) method.");

        var failGenericMethod = failMethod.MakeGenericMethod(resultType);
        var failResult = failGenericMethod.Invoke(null, new object[] { errorMessage });

        return (TResponse)failResult!;
    }
}