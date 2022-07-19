using CoreWebApiBoilerPlate.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.DataLayer.Context
{
    public partial class DefaultDBContext : DbContext
    {
        public DefaultDBContext()
        {
            Database.EnsureCreated();
        }

        public DefaultDBContext(DbContextOptions<DefaultDBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Role> Roles { get; set; } = null!;

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Todo> Todos { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<TodoStatus> TodoStatus { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasOne(p => p.CreatedBy)
                .WithMany();
                entity.HasOne(p => p.ModifiedBy)
               .WithMany();
            });



            modelBuilder.Entity<User>(entity =>
            {
               

                entity.Property(e => e.EmailId)
                    .HasMaxLength(100)
                    .IsUnicode(false);
              

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasIndex(i => i.Username)
                .IsUnique();

                entity.HasIndex(i => i.EmailId)
                .IsUnique();
            });



            modelBuilder.Entity<TodoStatus>()
                .HasData(SeedingData.GetTodoStatus());
            modelBuilder.Entity<Role>()
                .HasData(SeedingData.GetRoles());
            modelBuilder.Entity<User>()
                .HasData(SeedingData.GetUsers());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
