using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social.API.Models.Authorization;

namespace Social.API.Filters
{
    public class ApiKeyAuthAttribute: Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            DataContext dbContext = context.HttpContext.RequestServices.GetService<DataContext>();
            var apiUsers = await dbContext.Set<ApiUser>().ToListAsync();

            List<string> allKeys = new List<string>();
            apiUsers.ForEach(u => allKeys.Add(u.UserName + ":" + u.ApiKey));

            if(!allKeys.Any(x => x.Equals(potentialApiKey))) {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}