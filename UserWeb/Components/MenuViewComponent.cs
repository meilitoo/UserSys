using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserSysCore;
using UserSysCore.Models;
using UserSysCore.Service;
using UserWeb.Models;

namespace UserWeb.Components
{
    [ViewComponent(Name ="Menu")]
    public class MenuViewComponent:ViewComponent
    {
        IMenuService _MenuService;
        IApplicationContext _ApplicationContext;
        public MenuViewComponent(IMenuService menuService,IApplicationContext applicationContext)
        {
            _MenuService = menuService;
            _ApplicationContext = applicationContext;
        }
        public IViewComponentResult Invoke()
        {
            string ctlName = RouteData.Values["controller"].ToString();
            string actName= RouteData.Values["action"].ToString();

             MenuSidebarViewModel vm = new MenuSidebarViewModel();
            if (string.IsNullOrWhiteSpace(_ApplicationContext.CurrentUserRoles))
            {
                vm.UserMenuTree= new List<TreeMenu>();
                return View(vm);
            }
                
            var allMenu = _MenuService.GetMenusByRoleIds(_ApplicationContext.CurrentUserRoles.Split(',').Select(p=>int.Parse(p)).ToArray()).Where(p=>p.IsDisplay);
            IList<TreeMenu> treeMenuList = new List<TreeMenu>();
            Dictionary<int, TreeMenu> dicPMenu = new Dictionary<int, TreeMenu>();
            foreach(var item in allMenu)
            {
                if (!dicPMenu.ContainsKey(item.MenuPId))
                {
                    TreeMenu treeMenu = new TreeMenu();
                    treeMenu.ParentMenu = _MenuService.GetMenu(item.MenuPId);
                    treeMenu.SubMenus = allMenu.Where(p => p.MenuPId == item.MenuPId).ToList();
                    treeMenuList.Add(treeMenu);

                    dicPMenu.Add(item.MenuPId, treeMenu);
                }
                if (string.Equals(item.ControllerName, ctlName, StringComparison.CurrentCultureIgnoreCase)&& string.Equals(item.ActionName, actName, StringComparison.CurrentCultureIgnoreCase))
                {
                    vm.SelectedMenuId = item.MenuId;
                }
            }
            vm.UserMenuTree = treeMenuList;

            return View(vm);
        }
    }
}
