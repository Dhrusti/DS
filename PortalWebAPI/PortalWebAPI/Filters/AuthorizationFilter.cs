using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PortalWebAPI.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                var data = context.RouteData.Values["action"] as string;    // To get method name of current request.
            }
            catch (Exception)
            {
                // new CommonHelper(_configuration,IHostingEnvironment ).AddLog("Exception :: " + ex.ToString());
            }
        }
    }
}
