
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Presentation.Attributes
{
    internal class RedisCacheAttribute(int durationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetService<IServicesManager>().CacheSerices;
            var key = GenerateKey(context.HttpContext.Request);
            // check if the cache contains the key [Data in cache]
            var cachedData = await cacheService.GetCachedValueAsync(key);
              if(cachedData != null)
            {
              context.Result = new ContentResult
              {
                  Content = cachedData,
                  ContentType = "application/json",
                  StatusCode = 200
              };
                return;
            }
              var resultContext = await next.Invoke();
                if(resultContext.Result is ObjectResult objectResult)
            {
                await cacheService.SetCacheValueAsync(key, objectResult.Value, TimeSpan.FromSeconds(durationInSeconds));
            }
        }
        private string GenerateKey(HttpRequest request )
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"{item.Key}:{item.Value}");
            }
            return key.ToString();

        }
    }
}
