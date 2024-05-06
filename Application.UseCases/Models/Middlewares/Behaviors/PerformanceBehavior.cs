using MediatR;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Models.Middleware.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse>() : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Stopwatch _timer = new Stopwatch();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            long elapsedMilliseconds = _timer.ElapsedMilliseconds;

            Console.WriteLine($"Elapsed: {elapsedMilliseconds}");

            return response;
        }
    }
}