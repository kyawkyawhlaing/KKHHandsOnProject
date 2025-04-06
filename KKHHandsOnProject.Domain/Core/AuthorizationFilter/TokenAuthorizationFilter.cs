//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KKHHandsOnProject.Domain.Core.AuthorizationFilter
//{
//    public class TokenAuthorizationFilter : IAsyncAuthorizationFilter
//    {
//        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
//        {

//            if (context.HttpContext.Request.Cookies.ContainsKey("Auth-Key") == false)
//            {
//                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
//            }
//            if (context.HttpContext.Request.Cookies["Auth-Key"]!= "K001")
//            {
//                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
//            }

//        }
//    }
//}
