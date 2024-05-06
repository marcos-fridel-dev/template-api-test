using Infrastructure.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.Common.Exceptions;
using Shared.Common.Extensions.Exceptions;
using Shared.Common.Models.Responses;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Api.Middlewares
{
    public class ExceptionHandlerMiddleware() : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                HttpResponse response = context.Response;
                HttpRequest request = context.Request;
                response.ContentType = "application/json";

                ResultResponse result = new()
                {
                    Type = Shared.Common.Enums.Responses.ResultTypes.Error,
                    Severity = Shared.Common.Enums.Responses.ResultSeverities.Normal,
                    Message = error.Message,
                };


                switch (error)
                {
                    case ValidatorException e:
                        response.StatusCode = e.GetHttpStatusCode();

                        result = e.GetResult();
                        break;

                    case ForbidException:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;

                        break;

                    case BadRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;

                        break;

                    case HttpResponseMessageException e:
                        response.StatusCode = (int)HttpStatusCode.BadGateway;

                        break;
                    default:
                        result.Severity = Shared.Common.Enums.Responses.ResultSeverities.Critical;
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.ContentType = "application/json";

                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                string resultJson = JsonSerializer.Serialize(result, options);
                await context.Response.WriteAsync(resultJson);
            }
        }
    }
}