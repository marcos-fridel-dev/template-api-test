using FluentValidation.Results;
using Shared.Common.Enums.Responses;
using Shared.Common.Extensions.Core;
using Shared.Common.Models.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Application.UseCases.Extensions.Validations
{
    public static class ValidatorsExtension
    {
        public static List<ErrorValidator> GetValidationsExceptions(this ValidationResult[] validationResults, ResultSeverities severity = ResultSeverities.Normal)
        {
            if(validationResults == null)
            { 
                return new List<ErrorValidator>();
            }

            return validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .Select(x => new ErrorValidator
                {
                    Type = x.Severity switch
                    {
                        FluentValidation.Severity.Error => ResultTypes.Error,
                        FluentValidation.Severity.Warning => ResultTypes.Warning,
                        FluentValidation.Severity.Info => ResultTypes.Info,
                        _ => ResultTypes.Error,
                    },
                    PropertyName = x.PropertyName,
                    Message = x.ErrorMessage,
                    Severity = severity,
                    StatusCode = GetStatusCode(x.ErrorCode)
                })
                .ToList();
        }

        private static HttpStatusCode GetStatusCode(string? errorCode)
        {
            try
            {
                return errorCode?.GetEnum<HttpStatusCode>() ?? HttpStatusCode.NotAcceptable;
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}