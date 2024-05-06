using Microsoft.AspNetCore.Http;
using Shared.Common.Enums.Responses;
using Shared.Common.Extensions.Core;
using Shared.Common.Models.Responses;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Api.Middlewares
{
    public class ResponseHandlerMiddleware() : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //Console.WriteLine(context.Response);
            //await _next(context);
            //Console.WriteLine(context.Response.Body.ToString());
            try
            {
                string resultJson = "";

                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    await next.Invoke(context);
                    return;
                }

                var existingBody = context.Response.Body;
                using (var newBody = new MemoryStream())
                {
                    context.Response.Body = newBody;
                    await next(context);

                    //string a = Encoding.ASCII.GetString(context.Response.Body..ToArray());
                    //context.Response.Body.Flush();
                    if (!context.Response.Body.CanRead)
                    {
                        await next.Invoke(context);
                        return;
                    }

                    context.Response.Body.Seek(0, SeekOrigin.Begin);

                    string content = await new StreamReader(context.Response.Body)
                        .ReadToEndAsync();

                    context.Response.Body = new MemoryStream();
                    newBody.Seek(0, SeekOrigin.Begin);
                    context.Response.Body = existingBody;

                    string newContent = (new StreamReader(newBody)
                        .ReadToEnd())
                        .ToString();

                    if (!newContent.IsJson())
                    {
                        content = content == "null" ? "" : content;

                        if (context.Request.Method == "GET")
                        {
                            //context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                            //ResultResponse resultGetNotFound = new()
                            //{
                            //    Type = Shared.Common.Enums.Responses.ResultTypes.Success,
                            //    Severity = Shared.Common.Enums.Responses.ResultSeverities.Normal,
                            //    Message = "",
                            //};

                            //JsonSerializerOptions optionsNotFound = new JsonSerializerOptions
                            //{
                            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            //};

                            //string resultNotFoundJson = JsonSerializer.Serialize(resultGetNotFound, optionsNotFound);

                            resultJson = GetResultJson(
                                context,
                                ResultTypes.Success,
                                ResultSeverities.Normal,
                                "",
                                null,
                                StatusCodes.Status404NotFound);

                            await context.Response.WriteAsync(resultJson);
                            return;
                        }

                        await context.Response.WriteAsync(content);
                        return;
                    }

                    object? newContentJson = JsonSerializer.Deserialize<object>(newContent);

                    if (context.Response.StatusCode != StatusCodes.Status200OK)
                    {
                        if (newContentJson == null)
                        {
                            await context.Response.WriteAsync("");
                            return;
                        }

                        await context.Response.WriteAsync(content);
                        return;
                    }

                    //ResultResponse result = new()
                    //{
                    //    Type = Shared.Common.Enums.Responses.ResultTypes.Success,
                    //    Severity = Shared.Common.Enums.Responses.ResultSeverities.Normal,
                    //    Message = "",
                    //};

                    ////string requestMethod = context.Request.Method;

                    //result.Data = newContentJson != null && newContentJson.IsArray() ? newContentJson?.ToArray() : newContentJson;

                    //JsonSerializerOptions options = new JsonSerializerOptions { 
                    //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                    //};
                    //string resultJson = JsonSerializer.Serialize(result, options);

                    resultJson = GetResultJson(
                        context,
                        ResultTypes.Success,
                        ResultSeverities.Normal,
                        "",
                        newContentJson != null && newContentJson.IsArray() ? newContentJson?.ToArray() : newContentJson);

                    await context.Response.WriteAsync(resultJson);
                }
            }
            catch (Exception ex)
            {
                //ResultResponse result = new()
                //{
                //    Type = Shared.Common.Enums.Responses.ResultTypes.Error,
                //    Severity = Shared.Common.Enums.Responses.ResultSeverities.High,
                //    Message = ex.Message,
                //};

                //context.Response.ContentType = "application/json";
                //context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //JsonSerializerOptions options = new JsonSerializerOptions { 
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                //};
                //string resultJson = JsonSerializer.Serialize(result, options);

                string resultJson = GetResultJson(
                    context,
                    ResultTypes.Error,
                    ResultSeverities.High,
                    ex.Message,
                    null,
                    StatusCodes.Status500InternalServerError);

                await context.Response.WriteAsync(resultJson);
            }

        }

        private string GetResultJson(HttpContext context, ResultTypes type, ResultSeverities severity, string message, object data, int? statusCode = null)
        {
            ResultResponse result = new()
            {
                Type = type,
                Severity = severity,
                Message = message,
            };

            if (statusCode != null)
            {
                context.Response.StatusCode = statusCode ?? context.Response.StatusCode;
            }

            result.Data = data;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            context.Response.ContentType = "application/json";

            return JsonSerializer.Serialize(result, options);
        }
    }
}