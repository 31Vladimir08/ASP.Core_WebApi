using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DogCeoService.UserException;

namespace GetPicturesFromDogCeo.Filters
{
    public class NotImplExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DogCeoException)
            {
                context.Result = new ContentResult
                {
                    Content = $"В методе {context.ActionDescriptor.DisplayName} возникло исключение: \n {context.Exception.Message} \n {context.Exception.StackTrace}",
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
