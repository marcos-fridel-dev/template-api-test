using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Models.Middleware.Behaviors
{
    public class LogginBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"Request: {typeof(IRequest).Name} => {JsonSerializer.Serialize(request)}");
            Console.WriteLine($"Request: {typeof(IRequest).Name} => {JsonSerializer.Serialize(request)}");
            var response = await next();
            //_logger.LogInformation($"Response: {typeof(IRequest).Name} => {JsonSerializer.Serialize(response)}");
            //Console.WriteLine($"Response: {typeof(IRequest).Name} => {JsonSerializer.Serialize(response)}");

            return response;
        }
    }
}