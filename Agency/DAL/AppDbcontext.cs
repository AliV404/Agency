using System;
using Agency.Models;
using Microsoft.EntityFrameworkCore;

namespace Agency.DAL
{
    public class AppDbcontext:DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options):base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
