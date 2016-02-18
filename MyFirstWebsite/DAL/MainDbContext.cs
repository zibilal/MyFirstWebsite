using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyFirstWebsite.Models;

namespace MyFirstWebsite.DAL
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("name=DefaultConnection")
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<List> List { get; set; }
    }
}