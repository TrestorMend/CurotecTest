using Application.Responses;
using FluentValidation;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Application.Pipeline
{

    [ExcludeFromCodeCoverage]
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TResponse : ResponseState
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest?>> _validators;
        private readonly IResponseState _responseState;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest?>> validators,
            IResponseState responseState)
        {
            _validators = validators;
            _responseState = responseState;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToke)
        {
            if (_validators.Any())
            {
                var failures = _validators
                    .Select(v => v.Validate(request))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    return (TResponse)_responseState.ResponseWithNotification(request.GetType(), failures);
                }
            }

            return await next();
        }
    }
}