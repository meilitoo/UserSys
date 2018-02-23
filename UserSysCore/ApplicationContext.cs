using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace UserSysCore
{
    public class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
            HttpContextAccessor = httpContextAccessor;
        }
        public IHostingEnvironment HostingEnvironment
        {
            get;
        }
        public IHttpContextAccessor HttpContextAccessor
        {
            get;
            set;
        }

        public bool IsAuthenticated
        {
            get
            {
                var httpContext = HttpContextAccessor.HttpContext;
                if (httpContext == null)
                    return false;
                return HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public string CurrentUserName {
            get {
                var httpContext = HttpContextAccessor.HttpContext;
                if (IsAuthenticated)
                    return httpContext.User.Identity.Name; 
                return "";
            }
        }

        public int CurrentUserId {
            get
            {
                var httpContext = HttpContextAccessor.HttpContext;
                if (IsAuthenticated)
                    if( httpContext.User.HasClaim(p=>p.Type== ClaimTypes.PrimarySid))
                    {
                        var cl = httpContext.User.FindFirst(ClaimTypes.PrimarySid);
                        return int.Parse(cl.Value);
                    }
                return 0;
            }
        }

        public string CurrentUserRoles
        {
            get
            {
                var httpContext = HttpContextAccessor.HttpContext;
                if (IsAuthenticated)
                    if (httpContext.User.HasClaim(p => p.Type == ClaimTypes.UserData))
                    {
                        var cl = httpContext.User.FindFirst(ClaimTypes.UserData);
                        return cl.Value;
                    }
                return "";
            }
        }
    }
}
