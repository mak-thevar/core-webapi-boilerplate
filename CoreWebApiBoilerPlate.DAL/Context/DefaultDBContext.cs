using CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApiBoilerPlate.WebApi.DataAccessLayer.Context
{
    //dotnet ef database update --project .\CoreWebApiBoilerPlate.DAL.csproj --startup-project ..\CoreWebApiBoilerPlate\CoreWebApiBoilerPlate.WebApi.csproj
    //dotnet ef migrations add InitialCreate --project .\CoreWebApiBoilerPlate.DAL.csproj --startup-project ..\CoreWebApiBoilerPlate\CoreWebApiBoilerPlate.WebApi.csproj --output-dir Migrations
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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
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




            modelBuilder.Entity<Role>()
                .HasData(SeedingData.GetRoles());
            modelBuilder.Entity<User>()
                .HasData(SeedingData.GetUsers());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
