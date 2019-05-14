using Core.Template.Domain.Exceptions;
using Core.Template.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Template.Infrastructure.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
            if (context.Exception.GetType() == typeof(DomainException))
            {
                context.Result = new BadRequestObjectResult(new ApiResponse<object>(new ErrorInfo(context.Exception.HResult, context.Exception.Message)));
            }
            else if (context.Exception.GetType() == typeof(FriendlyException))
            {
                context.Result = new OkObjectResult(new ApiResponse<object>(new ErrorInfo(context.Exception.HResult, context.Exception.Message)));
            }
            else
            {
                context.Result = new ObjectResult(new ApiResponse<object>(new ErrorInfo(context.Exception.HResult, context.Exception.Message)))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            context.ExceptionHandled = true;
        }
    }

}

