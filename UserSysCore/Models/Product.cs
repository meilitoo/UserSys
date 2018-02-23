using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserSysCore.Models
{
   public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(16)]
        public string ProductName { get; set; }

        public string ProductDes { get; set; }

        public string ProductUrl { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public UserInfo Creator { get; set; }
    }
}
