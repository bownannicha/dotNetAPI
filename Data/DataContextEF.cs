using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class DataContextEF : DbContext
    {
        private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        // Access to each Models
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSalary> UserSalary { get; set; }
        public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

        // Setup EFContext ready to go
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Check if the the builder is configureD?
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                        optionsBuilder => optionsBuilder.EnableRetryOnFailure());
                // whether or not we want to RETRY to configure if it fail.
                // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
                // this package will tell EF to connect to SqlServer
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            // has to bring EntityFramework Relational 
            // dotnet add package Microsoft.EntityFrameworkCore.Relational
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            // EF connect to database schema, Mapping table with primary key
            modelBuilder.Entity<User>()
                .ToTable("Users", "TutorialAppSchema")
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserSalary>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<UserJobInfo>()
                .HasKey(u => u.UserId);
        }

    }
}