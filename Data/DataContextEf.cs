using DotNetApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Data
{
  public class DataContextEf : DbContext
  {
    private readonly IConfiguration _config;

    public DataContextEf(IConfiguration config)
    {
      _config = config;
    }

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<UserSalary> UserSalary { get; set; }
    public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"), x => x.EnableRetryOnFailure());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasDefaultSchema("TutorialAppSchema");

      // modelBuilder.Entity<User>().ToTable("Users", "TutorialAppSchema").HasKey(e => e.UserId);
      modelBuilder.Entity<User>(entity =>
      {
        entity.ToTable("Users", "TutorialAppSchema");
        entity.HasKey(e => e.UserId);
        entity.Property(e => e.FirstName).IsRequired();
        entity.Property(e => e.LastName).IsRequired();
        entity.Property(e => e.Email).IsRequired();
        entity.Property(e => e.Gender).IsRequired();
        entity.Property(e => e.Active).IsRequired();
      });

      modelBuilder.Entity<UserSalary>(entity =>
      {
        entity.HasKey(e => e.UserId);
        // entity.Property(e => e.UserId).IsRequired();
        // entity.Property(e => e.Salary).IsRequired();
      });

      modelBuilder.Entity<UserJobInfo>(entity =>
      {
        entity.HasKey(e => e.UserId);
        // entity.Property(e => e.UserId).IsRequired();
        // entity.Property(e => e.JobTitle).IsRequired();
        // entity.Property(e => e.Department).IsRequired();
      });
    }
  }
}