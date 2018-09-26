using ManyToMany.ConsoleDemo.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManyToMany.ConsoleDemo
{
    public class TestContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("testDB");
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=test;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostTag>()
                .HasKey(bc => new { bc.PostId, bc.TagId });//book = user, category = permission

            modelBuilder.Entity<PostTag>()
                .HasOne(bc => bc.Post)
                .WithMany(b => b.Tags)
                .HasForeignKey(bc => bc.PostId);

            //modelBuilder.Entity<UserPermission>()
            //    .HasOne(bc => bc.Permission)
            //    .WithMany(c => c.Users)
            //    .HasForeignKey(bc => bc.PermissionId);
        }
    }
}
