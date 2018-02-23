using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSysCore.Models;

namespace UserWeb.Models
{
    public class UserListViewModel
    {
        public IList<UserInfo> UserList { get; set; }

        public Func<int, IList<RoleInfo>> GetUserRolesFunc;

        public string GetUserRoleStr(int userId)
        {
            var list = GetUserRolesFunc(userId);
            if (list.Count == 0)
            {
                return "无角色";
            }
          return  string.Join(",", list.Select(p => p.RoleName));
        }
    }
}
