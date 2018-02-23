using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;
using UserSysCore;

namespace UserWeb.Models
{
    public class MenuListViewModel
    {
        public Func<int, Menu> GetMenuFunc;
       
        public IList<Menu> MenuList { get; set; }
        public Pagination PageInfo { get; set; }
        public string GetMenuNameByPId(int pId)
        {
            if(pId>0)
            {
                var menu = GetMenuFunc(pId);
                string menuName = pId.ToString();
                if (menu != null)
                    menuName = menu.MenuName;
                return menuName;
            }
            else
            {
                return "一级菜单";
            }
        }
    }
}

