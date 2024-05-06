using Application.UseCases.Extensions.Validations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Common.Exceptions;
using Shared.Common.Models.Validators;
using Shared.Localization.Resources.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Models.Middleware.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators, IStringLocalizer _localizer) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                var validationResults = await Task
                    .WhenAll(_validators
                        .Select(v => v.ValidateAsync(context, cancellationToken)));
                
                //foreach (var e in validationResults[0].Errors)
                //{
                //    Console.WriteLine($"PropertyName: {e.PropertyName} - ErrorCode: {e.ErrorCode}");
                //}

                List<ErrorValidator> validations = validationResults?.GetValidationsExceptions() ?? new List<ErrorValidator>();

                if (validations.Any())
                {
                    throw new ValidatorException(_localizer.GetString(Language.OneOrMoreFieldsDoNotMeetValidations), validations);
                }
            }

            return await next();
        }
    }
}