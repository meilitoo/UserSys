using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Service;
using UserWeb.Models;

namespace UserWeb.Controllers
{
    public class UserManageController : BaseController
    {
        IUserInfoService _UserInfoService;
        IRoleInfoService _RoleInfoService;
        public UserManageController(IUserInfoService userInfoService, IRoleInfoService roleInfoService,IApplicationContext applicationContext) : base(applicationContext)
        {
            _UserInfoService = userInfoService;
            _RoleInfoService = roleInfoService;
        }
        public IActionResult UserList()
        {
            UserListViewModel pageModel = new UserListViewModel
            {
                UserList = _UserInfoService.GetUserList(),
                 GetUserRolesFunc=_UserInfoService.GetUserRoles
            };
            return View(pageModel);
        }
        public IActionResult AddUser()
        {
            var vm = new AddUserViewModel
            {
                AllRoleList = _RoleInfoService.GetRoleList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(UserSysCore.Models.UserInfo model,int[] roleId)
        {
            string msg="";
            if (ModelState.IsValid)
            {
                bool res = _UserInfoService.AddUserInfo(model, out msg,roleId);
                if (res)
                    return RedirectToAction("UserList");
            }
           
            var resModel = new AddUserViewModel
            {
                ErrorMsg = msg,
                LoginName = model.LoginName,
                LoginPwd = model.LoginPwd,
                UserName = model.UserName
            };
            ModelState.AddModelError("LoginName", msg);
            return View(resModel);
        }

        public IActionResult Delete(int userId)
        {
            _UserInfoService.DelUser(userId);
            return RedirectToAction("UserList");
        }

        public IActionResult Edit(int userId)
        {
            var userInfo = _UserInfoService.GetUserInfo(userId);
            var model = new AddUserViewModel
            {
                LoginName = userInfo.LoginName,
                UserName = userInfo.UserName,
                UserId = userInfo.UserId,
               AllRoleList= _RoleInfoService.GetRoleList(),
               UserRoleList=_UserInfoService.GetUserRoles(userId),
                IsEdit = true
            };
            return View("AddUser",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserSysCore.Models.UserInfo model, int[] roleId)
        {

            bool res = _UserInfoService.UpdateUser(model, out string msg, roleId);
            if (res)
                    return RedirectToAction("UserList");
            

            var resModel = new AddUserViewModel
            {
                ErrorMsg = msg,
                LoginName = model.LoginName,
               
                UserName = model.UserName,
                IsEdit=true
            };
            ModelState.AddModelError("LoginName", msg);
            return View(resModel);
        }
    }
}