using AyolUchun.Features.Interviews.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AyolUchun.Features.Interviews.Configurations;

public class InterviewConfigurations : IEntityTypeConfiguration<Interview>
{
  public void Configure(EntityTypeBuilder<Interview> builder)
  {
    builder.ToTable("interviews");
    builder.HasKey(p => p.Id);

    builder.HasOne(c => c.User)
      .WithMany()
      .HasForeignKey(c => c.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.Property(p => p.Id)
      .HasColumnName("id");

    builder.Property(p => p.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(p => p.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(64);
    
    builder.Property(p => p.Image)
      .HasColumnName("image")
      .IsRequired()
      .HasMaxLength(128);
    
    builder.Property(p => p.Duration)
      .HasColumnName("duration")
      .IsRequired();
    
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