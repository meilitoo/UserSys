using System;
using System.Collections.Generic;
using System.Text;
using UserSysCore.Models;

namespace UserSysCore.Service
{
   public interface IMenuService
    {
        bool AddMenu(Menu menuInfo, out string msg);
        bool DelMenu(int menuId);
        bool UpdateMenu(Menu menuInfo, out string msg);
        IList<Menu> GetMenusByRoleId(int roleId);
        IList<Menu> GetMenusByRoleIds(params int[] roleId);
        IList<Menu> GetParentMenuList();
        IList<Menu> GetMenuList();
        IList<Menu> GetMenuList(Pagination pageInfo);
        Menu GetMenu(int menuId);
    }
}
