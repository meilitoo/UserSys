using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UserSysCore.Models;
using System.Threading.Tasks;

namespace UserSysCore.Service
{
    public class UserInfoService :BaseService<UserInfo>, IUserInfoService
    {
        public UserInfoService(IApplicationContext applicationContext, UserContext context):base(applicationContext,context)
        {
           
        }

        public bool AddUserInfo(UserInfo userInfo, out string msg)
        {
            string loginName = userInfo.LoginName;
            if(string.IsNullOrWhiteSpace(loginName))
            {
                msg = "登录名不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(userInfo.LoginPwd))
            {
                msg = "登录密码不能为空";
                return false;
            }
            if(CheckLoginName(loginName))
            {
                msg = "此登录名已存在";
                return false;
            }
            userInfo.LoginPwd = Helper.GetMD5Hash(userInfo.LoginPwd);
            userInfo.CreateTime = DateTime.Now;
            var newUser= Add(userInfo);
            if(newUser.UserId>0)
            {
                msg = newUser.UserId.ToString();
                return true;
            }
            else
            {
                msg = "保存失败";
                return false;
            }
            
        }

        public bool AddUserInfo(UserInfo userInfo, out string msg, params int[] roleId)
        {
           bool res=AddUserInfo(userInfo,out msg);
            if (res)
            {
                int userId = Convert.ToInt32(msg);

                BeginTransaction(() =>
                {
                    List<UserToRole> list = new List<UserToRole>();
                    foreach (int id in roleId)
                    {
                        list.Add(new UserToRole { UserId = userId, RoleId = id });
                    }
                    DbContext.UserToRoles.AddRange(list);
                    DbContext.SaveChanges();
                });
            }
            msg = "ok";
            return res;
        }

        public bool CheckLoginName(string loginName)
        {
          int n=  Count(p => p.LoginName == loginName);
            return n > 0; 
        }

        public bool DelUser(int userId)
        {
            Remove(userId);
            return true;
        }

        public UserInfo GetUserInfo(string userName, string md5Pwd)
        {
            var user=DbContext.UserInfos.FirstOrDefault(p => p.LoginName == userName && p.LoginPwd.Equals(md5Pwd, StringComparison.CurrentCultureIgnoreCase));
            return user;
        }

        public UserInfo GetUserInfo(int userId)
        {
            return Get(userId);
        }

        public async Task<UserInfo> GetUserInfoAsync(string userName, string md5Pwd)
        {
            var user = DbContext.UserInfos.FirstOrDefault(p => p.LoginName == userName && p.LoginPwd.Equals(md5Pwd, StringComparison.CurrentCultureIgnoreCase));
            return await Task<UserInfo>.Run(() => { return user; });
        }

        public IList<UserInfo> GetUserList()
        {
            return Get().ToList();
        }

        public IList<Menu> GetUserMenus(string roleIdStr)
        {
            var roleIdArr = roleIdStr.Split(',');
           var menuIds= DbContext.RoleToMenus.Where(p => roleIdArr.Contains(p.RoleId.ToString())).Select(p => p.MenuId);
            return DbContext.Menus.Where(p => menuIds.Contains(p.MenuId)).ToList();
        }

        public IList<RoleInfo> GetUserRoles(int userId)
        {
          var roleIds=  DbContext.UserToRoles.Where(p=>p.UserId==userId).Select(p=>p.RoleId);
          return  DbContext.RoleInfos.Where(p => roleIds.Contains(p.RoleId)).ToList();
        }

        public bool UpdateUser(UserInfo userInfo, out string msg)
        {
            var newUser = Get(userInfo.UserId);
            if (newUser == null)
            {
                msg = "未找到用户";
                return false;
            }
            newUser.UserName = userInfo.UserName;
            Update(newUser);
            msg = "ok";
            return true;
        }

        public bool UpdateUser(UserInfo userInfo, out string msg, params int[] roleId)
        {
            bool res = UpdateUser(userInfo, out msg);
            if (res)
            {
                int userId = userInfo.UserId;

                BeginTransaction(() =>
                {
                    DbContext.UserToRoles.RemoveRange(DbContext.UserToRoles.Where(p => p.UserId == userId));
                    List<UserToRole> list = new List<UserToRole>();
                    foreach (int id in roleId)
                    {
                        list.Add(new UserToRole { UserId = userId, RoleId = id });
                    }
                    DbContext.UserToRoles.AddRange(list);
                    DbContext.SaveChanges();
                });
            }
            return res;
        }
    }
}
