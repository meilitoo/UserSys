using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;
using UserSysCore.Service;

namespace UserWeb.Models
{
    public class RoleListViewModel
    {
        public Func<int, IList<Menu>> GetRoleMenuFunc;
        
        public IList<RoleInfo> RoleInfoList { get; set; }

        public string GetRoleMenuStr(int roleId)
        {
            var list = GetRoleMenuFunc(roleId);
            if(list.Count==0)
                return "无菜单权限";
            return string.Join(" ", list.Select(p => p.MenuName));
        }
    }
}
