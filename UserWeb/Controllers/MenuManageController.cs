using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Service;
using System.Reflection;
using UserWeb.Models;

namespace UserWeb.Controllers
{
    public class MenuManageController : BaseController
    {
        IMenuService _MenuService;
        public MenuManageController(IMenuService menuService, IApplicationContext applicationContext) : base(applicationContext)
        {
            _MenuService = menuService;
        }
        public IActionResult AddMenu()
        {
            AddMenuViewModel vm = new AddMenuViewModel
            {
                IsParentMenu = false
            };
            vm.ParentMenuList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_MenuService.GetParentMenuList(), "MenuId", "MenuName");

            return View(vm);
        }

        public IActionResult AddParentMenu()
        {
            AddMenuViewModel vm = new AddMenuViewModel
            {
                IsParentMenu = true
            };
            return View("AddMenu", vm);
        }

        public IActionResult EditMenu(int menuId)
        {
            var menu = _MenuService.GetMenu(menuId);
            if (menu == null)
                return NotFound();
            AddMenuViewModel vm = new AddMenuViewModel
            {
                IsParentMenu = menu.MenuPId == 0,
                IsEdit = true,
                ActionName = menu.ActionName,
                ControllerName =menu.ControllerName,
                MenuId = menu.MenuId,
                MenuMemo = menu.MenuMemo,
                MenuPId = menu.MenuPId,
                MenuName = menu.MenuName,
                IsDisplay=menu.IsDisplay,
                OrderId = menu.OrderId
            };
            vm.ParentMenuList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_MenuService.GetParentMenuList(), "MenuId", "MenuName");

            return View("AddMenu", vm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMenu(UserSysCore.Models.Menu menu)
        {
            if (menu.MenuPId == 0)
            {
                menu.ActionName = "";
                menu.ControllerName = "";
            }
           
            bool res = _MenuService.AddMenu(menu, out string msg);
            if (res)
                return RedirectToAction("MenuList");
            return RedirectToAction("AddMenu");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenu(UserSysCore.Models.Menu menu)
        {
            if (menu.MenuPId == 0)
            {
                menu.ActionName = "";
                menu.ControllerName = "";
            }

            bool res = _MenuService.UpdateMenu(menu, out string msg);
            if (res)
                return RedirectToAction("MenuList");
            return RedirectToAction("EditMenu",menu.MenuId);
        }
        public IActionResult Delete(int menuId)
        {
            _MenuService.DelMenu(menuId);
            return RedirectToAction("MenuList");
        }
        public IActionResult GetActionList(string controllerName)
        {
            string fullName = "UserWeb.Controllers." + controllerName + "Controller";
            Assembly asm = Assembly.GetExecutingAssembly();
            var actionList = asm.GetType(fullName).GetMethods().Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)) && method.ReturnType == typeof(IActionResult)).Select(p => p.Name).ToList();
            return Json(actionList);
        }

        public IActionResult MenuList(int page = 1)
        {
            int pageIndex = page-1;
            if (pageIndex < 0)
                pageIndex = 0;

            Pagination pagination = new Pagination
            {
                PageIndex = pageIndex, PageSize=5
            };
            MenuListViewModel vm = new MenuListViewModel
            {
                MenuList = _MenuService.GetMenuList(pagination), GetMenuFunc=_MenuService.GetMenu, PageInfo=pagination
            };
            return View(vm);
        }
    }
}