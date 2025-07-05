using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MiniETicaret.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniETicaret.Application.Common.Behaviors
{
    public class CachingBehavior<TRequest, TResponse>(IDistributedCache cache)
         : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheableRequest
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var cachedResponse = await cache.GetAsync(request.CacheKey, cancellationToken);
                if (cachedResponse != null)
                {
                    // Cache'te varsa, veriyi deserialize edip döndür  
                    var cachedResponseString = Encoding.UTF8.GetString(cachedResponse);
                    return JsonSerializer.Deserialize<TResponse>(cachedResponseString);
                }

                // Cache'te yoksa, handler'ı çalıştır  
                var response = await next();

                // Gelen cevabı serialize edip cache'e kaydet  
                var serializedResponseString = JsonSerializer.Serialize(response);
                var responseBytes = Encoding.UTF8.GetBytes(serializedResponseString);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(request.SlidingExpiration ?? TimeSpan.FromHours(1));

                await cache.SetAsync(request.CacheKey, responseBytes, options, cancellationToken);

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
