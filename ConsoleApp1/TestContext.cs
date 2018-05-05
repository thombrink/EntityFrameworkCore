using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class TestContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("testDB");
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=test;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPermission>()
                .HasKey(bc => new { bc.UserId, bc.PermissionId });//book = user, category = permission

            modelBuilder.Entity<UserPermission>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Permissions)
                .HasForeignKey(bc => bc.UserId);

            //modelBuilder.Entity<UserPermission>()
            //    .HasOne(bc => bc.Permission)
            //    .WithMany(c => c.Users)
            //    .HasForeignKey(bc => bc.PermissionId);
        }
    }
}
