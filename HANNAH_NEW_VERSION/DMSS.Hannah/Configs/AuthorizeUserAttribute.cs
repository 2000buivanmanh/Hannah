using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HANNAH_NEW_VERSION.Configs
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private readonly int[] allowedroles;
        public AuthorizeUserAttribute(params int[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var staffService = DependencyResolver.Current.GetService<IAuthenticationService>();
            var user = staffService.GetAuthenticatedUser();
            if (user != null)
            {
                foreach (var role in allowedroles)
                {
                    if (role == user.VaiTro) return true;
                }
            }
            var userId = Convert.ToString(httpContext.Session["UserId"]);
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "action", "UnAuthorized" }
               });
        }
    }
}