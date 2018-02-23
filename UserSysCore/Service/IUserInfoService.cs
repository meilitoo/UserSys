using System;
using System.Collections.Generic;
using System.Text;
using UserSysCore.Models;
using System.Threading.Tasks;

namespace UserSysCore.Service
{
   public interface IUserInfoService
    {
        UserInfo GetUserInfo(string userName, string md5Pwd);
        UserInfo GetUserInfo(int userId);
        IList<UserInfo> GetUserList();
        IList<RoleInfo> GetUserRoles(int userId);
        IList<Menu> GetUserMenus(string roleIdStr);
        bool AddUserInfo(UserInfo userInfo, out string msg);
        bool AddUserInfo(UserInfo userInfo, out string msg,params int[] roleId);
        bool CheckLoginName(string loginName);
        bool DelUser(int userId);
        bool UpdateUser(UserInfo userInfo, out string msg);
        bool UpdateUser(UserInfo userInfo, out string msg, params int[] roleId);

        Task<UserInfo> GetUserInfoAsync(string userName, string md5Pwd);


    }
}
