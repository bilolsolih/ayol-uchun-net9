using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AyolUchun.Features.Courses.Models;

namespace AyolUchun.Features.Courses.Configurations;

public class SocialAccountConfigurations : IEntityTypeConfiguration<SocialAccount>
{
  public void Configure(EntityTypeBuilder<SocialAccount> builder)
  {
    builder.ToTable("social_accounts");
    builder.HasKey(c => c.Id);

    builder.HasIndex(c => c.Title)
      .IsUnique();

    builder.Property(c => c.Id)
      .HasColumnName("id");

    builder.Property(c => c.Title)
      .HasColumnName("title")
      .IsRequired()
      .HasMaxLength(32);

    builder.Property(c => c.Link)
      .HasColumnName("link")
      .IsRequired()
      .HasMaxLength(128);

    builder.Property(c => c.Icon)
      .HasColumnName("icon")
      .IsRequired()
      .HasMaxLength(128);

    builder.Property(c => c.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(c => c.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}