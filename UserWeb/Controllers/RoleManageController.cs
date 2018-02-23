using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Service;
using UserSysCore.Models;
using UserWeb.Models;

namespace UserWeb.Controllers
{
    public class RoleManageController : BaseController
    {
        IMenuService _MenuService;
        IRoleInfoService _RoleInfoService;
        public RoleManageController(IRoleInfoService roleInfoService, IMenuService menuService, IApplicationContext applicationContext) : base(applicationContext)
        {
            _MenuService = menuService;
            _RoleInfoService = roleInfoService;
        }

        public IActionResult AddRole()
        {
            var vm = new AddRoleViewModel();
            var allMenu = _MenuService.GetMenuList();
            IList<TreeMenu> treeMenuList = new List<TreeMenu>();
            var pMenus = allMenu.Where(p => p.MenuPId == 0);
            foreach (var item in pMenus)
            {
                TreeMenu treeMenu = new TreeMenu {
                    ParentMenu = item, SubMenus = allMenu.Where(p => p.MenuPId == item.MenuId).ToList() };
                treeMenuList.Add(treeMenu);
            }
            vm.MenuList = treeMenuList;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRole(RoleInfo role,int[] subMenu)
        {
            role.CreateTime = DateTime.Now;
            _RoleInfoService.AddRole(role, out string msg, subMenu); 
            return RedirectToAction("RoleList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRole(RoleInfo role, int[] subMenu)
        {
            _RoleInfoService.UpdateRole(role, out string msg, subMenu);
            return RedirectToAction("RoleList");
        }
        public IActionResult EditRole(int roleId)
        {
            var roleModel = _RoleInfoService.GetRole(roleId);
            if (roleModel == null)
                return NotFound();
            var vm = new AddRoleViewModel
            {
                IsEdit = true,
                RoleInfo = roleModel
            };
            var allMenu = _MenuService.GetMenuList();
            IList<TreeMenu> treeMenuList = new List<TreeMenu>();
            var pMenus = allMenu.Where(p => p.MenuPId == 0);
            foreach (var item in pMenus)
            {
                TreeMenu treeMenu = new TreeMenu
                {
                    ParentMenu = item,
                    SubMenus = allMenu.Where(p => p.MenuPId == item.MenuId).ToList()
                };
                treeMenuList.Add(treeMenu);
            }
            vm.MenuList = treeMenuList;
            vm.RoleMenus = _MenuService.GetMenusByRoleIds(roleId);
            return View("AddRole", vm);
           
        }
        public IActionResult Delete(int roleId)
        {
            _RoleInfoService.DelRole(roleId);
            return RedirectToAction("RoleList");
        }

        public IActionResult RoleList()
        {
            var vm = new RoleListViewModel
            {
                RoleInfoList = _RoleInfoService.GetRoleList(), GetRoleMenuFunc=_MenuService.GetMenusByRoleId
            };
            return View(vm);
        }
    }
}