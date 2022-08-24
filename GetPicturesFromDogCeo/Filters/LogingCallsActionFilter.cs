﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GetPicturesFromDogCeo.Filters
{
    public class LogingCallsActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogingCallsActionFilter> _loger;

        public LogingCallsActionFilter(ILogger<LogingCallsActionFilter> loger)
        {
            _loger = loger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _loger.LogInformation($"{context.ActionDescriptor.DisplayName}: START");
            await next();
            _loger.LogInformation($"{context.ActionDescriptor.DisplayName}: FINISH");
        }
    }
}