using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Models.Responses;
using Shared.Common.Models.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Api.Models.Middleware.Results;

public class ValidationProblemDetailsResult : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        //var keys = context.ModelState.Keys;
        //var dic = context.ModelState.ToDictionary(
        //        kvp => kvp.Key,
        //        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
        //    );
        //var problemDetails = new
        //{
        //    Status = StatusCodes.Status400BadRequest,
        //    Errors = dic
        //};
        //var objectResult = new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
        //await objectResult.ExecuteResultAsync(context);
        List<ErrorValidator> validations = context.ModelState
            .Select(x => new ErrorValidator
                {
                    Type = Shared.Common.Enums.Responses.ResultTypes.Error,
                    Severity = Shared.Common.Enums.Responses.ResultSeverities.Normal,
                    PropertyName = x.Key,
                    Message = x.Value?.Errors.Select(e => e.ErrorMessage).ToArray().First() ?? string.Empty
                })
            .ToList();

        ResultResponse responseMiddleware = new ResultResponse
        {
            Type = Shared.Common.Enums.Responses.ResultTypes.Error,
            Severity = Shared.Common.Enums.Responses.ResultSeverities.Normal,
            Validations = validations
        };

        var objectResult = new ObjectResult(responseMiddleware) { StatusCode = StatusCodes.Status400BadRequest };
        await objectResult.ExecuteResultAsync(context);
    }
}
