using Shared.Common.Exceptions;
using Shared.Common.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Shared.Common.Extensions.Exceptions
{
    public static class ValidatorExceptionExtension
    {

        public static int GetHttpStatusCode(this ValidatorException validatorException)
        {
            if (!validatorException.Validations.Any())
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            List<HttpStatusCode> statusCode = validatorException
                .Validations
                .Select(x => x.StatusCode)
                .Distinct()
                .ToList();

            if (statusCode.Contains(HttpStatusCode.NotFound))
            {
                return (int)HttpStatusCode.NotFound;
            }

            return (int)validatorException.Validations.First().StatusCode;
        }
        public static ResultResponse GetResult(this ValidatorException validationException)
        {
            return new ResultResponse()
            {
                Type = validationException.Type,
                Severity = validationException.Severity,
                Message = validationException.Message,
                //Message = validationException.Code,
                Validations = validationException.Validations

            };
        }
    }
}