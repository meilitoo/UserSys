using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using UserWeb.Models;
using UserSysCore.Service;
using UserSysCore;

namespace UserWeb.Controllers
{
    public class AccountController : BaseController
    {
        IUserInfoService _UserInfoService;
        IdentityService _IdentityService;
        public AccountController(IUserInfoService userInfoService, IdentityService identityService, IApplicationContext applicationContext) : base(applicationContext)
        {
            _UserInfoService = userInfoService;
            _IdentityService = identityService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var pageModel = new LoginViewModel { ReturnUrl="/" };
            return View("~/Views/Home/Index.cshtml",pageModel);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl)
        {
            return View(); 
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            var pageModel = new LoginViewModel { ReturnUrl = ReturnUrl };
            return View("~/Views/Home/Index.cshtml", pageModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null)
            {
                model = new LoginViewModel();
                model.ErrorInfo = "请输入用户名";
            }
            var userPrincipal= await _IdentityService.CheckUserAsync(model.UserName, model.Password);
            if (userPrincipal!=null)
            {
                await HttpContext.SignInAsync(IdentityService.AuthenticationScheme, userPrincipal);
                if (string.IsNullOrEmpty(model.ReturnUrl))
                    model.ReturnUrl = "/Home/MainPage";
                return Redirect(model.ReturnUrl);
            }
            else
                model.ErrorInfo = "用户名或密码错误";
            return View("~/Views/Home/Index.cshtml", model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(IdentityService.AuthenticationScheme);
            return Redirect("/");
        }
    }
}