using Application.UseCases.Interfaces.Middlewares.Behavior;
using Infrastructure.Services.Extensions.Cache;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Models.Middleware.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(IDistributedCache _cache) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICachingBehavior
    {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse? result = await _cache.GetCacheAsync<TResponse>(request.Key);
            if (result == null)
            {
                result = await next();
                await _cache.SetCacheAsync(request.Key, result);
            }

            return result;
        }
    }
}