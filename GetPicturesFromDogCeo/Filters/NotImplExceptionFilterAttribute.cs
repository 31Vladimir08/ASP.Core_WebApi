using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DogCeoService.UserException;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace GetPicturesFromDogCeo.Filters
{
    public class NotImplExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly ILogger<NotImplExceptionFilterAttribute> _logger;

        public NotImplExceptionFilterAttribute(ILogger<NotImplExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DogCeoException)
            {
                context.Result = new ContentResult
                {
                    Content = $"Ошибка: {context.Exception.Message}",
                    StatusCode = 400
                };
                context.ExceptionHandled = true;

                _logger.LogError($"В методе {context.ActionDescriptor.DisplayName} возникло исключение: \n {context.Exception.Message} \n {context.Exception.StackTrace}");
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = $"Ошибка во время выполнения запроса",
                    StatusCode = 400
                };
                context.ExceptionHandled = true;

                _logger.LogError($"В методе {context.ActionDescriptor.DisplayName} возникло исключение: \n {context.Exception.Message} \n {context.Exception.StackTrace}");
            }
        }
    }
}
