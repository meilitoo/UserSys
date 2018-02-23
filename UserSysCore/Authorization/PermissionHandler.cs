using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using UserSysCore.Service;
using UserSysCore.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserSysCore.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        IUserInfoService _UserInfoService;
        public PermissionHandler(IUserInfoService userInfoService)
        {
            _UserInfoService = userInfoService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            Claim claim = context.User.FindFirst(ClaimTypes.UserData);
            IList<Menu> menuList = new List<Menu>();
            if (claim != null)
            {
                string roleId = claim.Value;
                menuList = _UserInfoService.GetUserMenus(roleId);

            }


            var mvcContext = context.Resource as AuthorizationFilterContext;
            var actionDescriptor = mvcContext.ActionDescriptor as ControllerActionDescriptor;
            string controllerName = actionDescriptor.ControllerName;
            string actionName = actionDescriptor.ActionName;
           if( menuList.Any(p=>p.ControllerName.Equals(controllerName,StringComparison.CurrentCultureIgnoreCase)&& p.ActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
          
            return Task.CompletedTask;
        }
    }
}
