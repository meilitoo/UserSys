using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSysCore.Models
{
   public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        [Required]
        [StringLength(16)]
        public string MenuName { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool IsDisplay { get; set; }

        [StringLength(64)]
        public string MenuMemo { get; set; }
        public int MenuPId { get; set; }
        public int OrderId { get; set; }




    }
}
