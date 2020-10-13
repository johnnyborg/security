using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.DAL
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}