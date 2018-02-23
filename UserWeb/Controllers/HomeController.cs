﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace UserWeb.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IApplicationContext applicationContext) : base(applicationContext)
        {

        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult MainPage()
        {
            ViewBag.Name= AppContext.CurrentUserName;
            return View();
        }


        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
