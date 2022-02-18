using CoreWebApiBoilerPlate.Entity;
using CoreWebApiBoilerPlate.Infrastructure.Data.MockData;
using Microsoft.EntityFrameworkCore;
using NETCore.Encrypt.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelpDeskBBQN.Infrastructure.Data
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options): base(options)
        {
        
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                 .Property(p => p.IsActive)
                 .HasDefaultValue(false);

            modelBuilder.Entity<Post>()
                .HasOne(m=>m.User)
                .WithMany(m => m.Posts)
                .HasForeignKey(f => f.CreatedBy);
        }

    }
}
