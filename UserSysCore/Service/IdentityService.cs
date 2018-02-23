using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using UserSysCore.Models;


namespace UserSysCore.Service
{
   public class IdentityService
    {
        public const string AuthenticationScheme = "WebAuthCookies";
        IUserInfoService _UserInfoService;
        public IdentityService(IUserInfoService userService)
        {
            _UserInfoService = userService;
        }
        public async Task<ClaimsPrincipal> CheckUserAsync(string username, string password)
        {
            string md5Password =Helper.GetMD5Hash(password);
            var user =await _UserInfoService.GetUserInfoAsync(username, md5Password);
            if (user == null) return null;

            var ci = CreateClaimsIdentity(user);
          
            return new ClaimsPrincipal(ci);
        }

        #region helper
       

        private ClaimsIdentity CreateClaimsIdentity(UserInfo user)
        {
            //用当前用户信息创建一个ClaimsIdentity
            //AuthenticationScheme需要和Cookie中间件中AuthenticationScheme一致
            //如果添加的角色时使用的类型不是ClaimTypes.Role，则需要在此处指定类型
            //var result = new ClaimsIdentity(AuthenticationScheme,NameType,RoleType);
            var result = new ClaimsIdentity(AuthenticationScheme);
            //NameType使用自带的ClaimTypes.Name
            result.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            result.AddClaim(new Claim(ClaimTypes.PrimarySid, user.UserId.ToString()));

            var roleList = _UserInfoService.GetUserRoles(user.UserId);
            string roleIdStr = string.Join(',', roleList.Select(p => p.RoleId));
            result.AddClaim(new Claim(ClaimTypes.UserData, roleIdStr));
            return result;
        }
        #endregion
    }
}
