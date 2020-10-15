using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Security.DAL.Entities;

namespace Security.DAL
{
    public class WindesheimDbContext : DbContext
    {
        public WindesheimDbContext(DbContextOptions<WindesheimDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Email = "student@windesheim.nl", Password = User.hash("password"), Name = "Student", Created = DateTime.Now });
        }
    }
}