using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSysCore.Models
{
   public class RoleInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [StringLength(16)]
        public string RoleName { get; set; }

        public string RoleMemo { get; set; }

        public DateTime CreateTime { get; set; }

    }

    public class RoleToMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleToMenuId { get; set; }

        public int RoleId { get; set; }

        public int MenuId { get; set; }

   }
}
