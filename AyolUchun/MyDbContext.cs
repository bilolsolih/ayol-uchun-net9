using AyolUchun.Features.Authentication.Configurations;
using AyolUchun.Features.Authentication.Models;
using AyolUchun.Features.Courses.Configurations;
using AyolUchun.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace AyolUchun;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Otp> Otps { get; set; }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<SocialAccount> SocialAccounts { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.ApplyConfiguration(new UserConfigurations());
    builder.ApplyConfiguration(new OtpConfigurations());

    builder.ApplyConfiguration(new CategoryConfigurations());
    builder.ApplyConfiguration(new CourseConfigurations());
    builder.ApplyConfiguration(new SocialAccountConfigurations());
  }
}