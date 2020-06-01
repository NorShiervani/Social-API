using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Social.API.Filters
{
    public class ApiKeyAuthAttribute: Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName= "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   
           if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
           {
               context.Result = new UnauthorizedResult();
               return;
           }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apikey = configuration.GetValue<string>(key: "ApiKey");
            
            if(!apikey.Equals(potentialApiKey))
            {
               context.Result = new UnauthorizedResult();
               return;
            }

            await next();
        }
    }
}