using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSysCore.Models
{
   public class UserInfo
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [StringLength(16)]
        [Required(ErrorMessage = "登录名不能为空")]
        public string LoginName { get; set; }
        [StringLength(32)]
        [Required(ErrorMessage ="密码不能为空")]
        public string LoginPwd { get; set; }
        [StringLength(16)]
        public string UserName { get; set; }

        public DateTime CreateTime { get; set; }

    }

    public class UserToRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserToRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

    }
}
