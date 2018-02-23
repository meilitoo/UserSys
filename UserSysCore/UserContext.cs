using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserSysCore.Models;

namespace UserSysCore
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }
        public DbSet<RoleToMenu> RoleToMenus { get; set; }
        public DbSet<UserToRole> UserToRoles { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<RoleInfo>().ToTable("RoleInfo");
            modelBuilder.Entity<RoleToMenu>().ToTable("RoleToMenu");
            modelBuilder.Entity<UserToRole>().ToTable("UserToRole");
            modelBuilder.Entity<Product>().ToTable("Product");
        }
    }
}
