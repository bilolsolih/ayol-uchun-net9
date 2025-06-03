using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AyolUchun.Features.Courses.Models;

namespace AyolUchun.Features.Courses.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.ToTable("courses");
    builder.HasKey(p => p.Id);

    builder.HasOne(c => c.User)
      .WithMany()
      .HasForeignKey(c => c.UserId)
      .OnDelete(DeleteBehavior.Restrict);
    
    builder.HasOne(p => p.Category)
      .WithMany(c => c.Courses)
      .HasForeignKey(p => p.CategoryId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.Id)
      .HasColumnName("id");

    builder.Property(p => p.UserId)
      .HasColumnName("user_id")
      .IsRequired();
    
    builder.Property(p => p.CategoryId)
      .HasColumnName("category_id")
      .IsRequired();

    builder.Property(p => p.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(64);

    builder.Property(p => p.Image)
      .HasColumnName("image")
      .IsRequired()
      .HasMaxLength(128);

    builder.Property(p => p.Price)
      .HasColumnName("price")
      .IsRequired();
    
    builder.Property(p => p.Rating)
      .HasColumnName("rating")
      .IsRequired();
    
    builder.Property(p => p.Status)
      .HasColumnName("status")
      .IsRequired(false);

    builder.Property(p => p.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(p => p.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}