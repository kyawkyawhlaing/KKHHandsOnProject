using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHHandsOnProject.Domain.Features.Blogs
{
    public class BlogsActionFilter : IActionFilter
    {
        private readonly ILogger<BlogsActionFilter> _logger;
        public BlogsActionFilter(ILogger<BlogsActionFilter> logger)
        {
            _logger = logger;
        }
        // Before
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Before : {FileName}.{MethodName}", nameof(BlogsActionFilter), nameof(OnActionExecuting));
           if (context.ActionArguments.ContainsKey("fname"))
            {
                context.HttpContext.Items["arguments"] = context.ActionArguments;
                string? fname = Convert.ToString(context.ActionArguments["fname"]);
                context.ActionArguments["fname"] = "Static Value";
            }
        }

        // After
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("After : {FileName}.{MethodName}", nameof(BlogsActionFilter), nameof(OnActionExecuted));
            IDictionary<string, object?>? parameters = (IDictionary<string, object>) context.HttpContext.Items["arguments"];
   
        }

    }
}
