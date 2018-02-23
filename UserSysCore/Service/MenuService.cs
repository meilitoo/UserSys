using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UserSysCore.Models;

namespace UserSysCore.Service
{
   public class MenuService:BaseService<Menu>, IMenuService
    {
        public MenuService(IApplicationContext applicationContext, UserContext context) : base(applicationContext, context)
        {

        }

        public bool AddMenu(Menu menuInfo, out string msg)
        {
            var newMenu = Add(menuInfo);
            if (newMenu.MenuId > 0)
            {
                msg = newMenu.MenuId.ToString();
                return true;
            }
            else
            {
                msg = "添加失败";
                return false;
            }
        }

        public bool DelMenu(int menuId)
        {
            Remove(menuId);
            return true;
        }

        public Menu GetMenu(int menuId)
        {
            return Get(menuId);
        }

        public IList<Menu> GetMenuList()
        {
            return Get().ToList();
        }

        public IList<Menu> GetMenuList(Pagination pageInfo)
        {
            return Get(null, pageInfo);
        }

        public IList<Menu> GetMenusByRoleId(int roleId)
        {
            return GetMenusByRoleIds(roleId);
        }
        public IList<Menu> GetMenusByRoleIds(params int[] roleId)
        {
            string paraStr = ""; 
            if(roleId.Length==1)
            {
                paraStr = "=" + roleId[0];
            }
            else
            {
                paraStr = "in(";
                foreach (int id in roleId)
                {
                    paraStr += id + ",";
                }
                paraStr = paraStr.TrimEnd(',') + ")";
            }

            return GetFromSql(@"select * from menu where menuid in(select menuid from RoleToMenu where roleid " + paraStr + ")");
          
           
        }

        public IList<Menu> GetParentMenuList()
        {
          return  Get(p => p.MenuPId == 0);
        }

        public bool UpdateMenu(Menu menuInfo, out string msg)
        {
            var newMenu = Get(menuInfo.MenuId);
            if (newMenu == null)
            {
                msg = "未找到菜单";
                return false;
            }
            newMenu.MenuName = menuInfo.MenuName;
            newMenu.MenuMemo = menuInfo.MenuMemo;
            newMenu.ActionName = menuInfo.ActionName;
            newMenu.ControllerName = menuInfo.ControllerName;
            newMenu.MenuPId = menuInfo.MenuPId;
            newMenu.OrderId = menuInfo.OrderId;
            newMenu.IsDisplay = menuInfo.IsDisplay;
            Update(newMenu);
            msg = "ok";
            return true;
        }
    }
}
