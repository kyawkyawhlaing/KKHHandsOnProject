using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHHandsOnProject.Domain.Features.Blogs
{
    public class BlogsActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<BlogsActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;
        public BlogsActionFilter(ILogger<BlogsActionFilter> logger, string key, string value)
        {
            _logger = logger;
            _key    = key;
            _value  = value;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Before
            _logger.LogInformation("Before : {FileName}.{MethodName} - Before", nameof(BlogsActionFilter), nameof(OnActionExecutionAsync));
            if (context.ActionArguments.ContainsKey("fname"))
            {
                context.HttpContext.Items["arguments"] = context.ActionArguments;
                string? fname = Convert.ToString(context.ActionArguments["fname"]);
                context.ActionArguments["fname"] = "Method argument was changed by Custom Filter";
            }
            await next();
            // After
            _logger.LogInformation("After : {FileName}.{MethodName} - After", nameof(BlogsActionFilter), nameof(OnActionExecutionAsync));
            IDictionary<string, object?>? parameters = (IDictionary<string, object>)context.HttpContext.Items["arguments"];
            context.HttpContext.Response.Headers[_key] = _value;

        }
    }
}
