using BlazorProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Data
{
    public class PHContext : DbContext
    {
        public PHContext(DbContextOptions<PHContext> options) : base(options)
        {
        }

        public DbSet<PHItem> PHItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PHItem>().ToTable("PHItem");
        }
    }
}
