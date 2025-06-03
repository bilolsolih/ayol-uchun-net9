using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Authentication.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");
    builder.HasKey(u => u.Id);

    builder.HasIndex(u => u.Email)
      .IsUnique();

    builder.HasIndex(u => u.PhoneNumber)
      .IsUnique();

    builder.Property(u => u.Id)
      .HasColumnName("id");

    builder.Property(u => u.FirstName)
      .HasColumnName("first_name")
      .IsRequired()
      .HasMaxLength(64);
    
    builder.Property(u => u.LastName)
      .HasColumnName("last_name")
      .IsRequired()
      .HasMaxLength(64);

    builder.Property(u => u.Email)
      .HasColumnName("email")
      .IsRequired()
      .HasMaxLength(64);


    builder.Property(u => u.PhoneNumber)
      .HasColumnName("phone_number")
      .IsRequired()
      .HasMaxLength(16);

    builder.Property(u => u.BirthDate)
      .HasColumnName("birth_date")
      .IsRequired(false);

    builder.Property(u => u.Password)
      .HasColumnName("password")
      .IsRequired()
      .HasMaxLength(32);

    builder.Property(u => u.Gender)
      .HasColumnName("gender")
      .IsRequired(false);


    builder.Property(u => u.Created)
      .HasColumnName("created")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();

    builder.Property(u => u.Updated)
      .HasColumnName("updated")
      .IsRequired()
      .HasDefaultValueSql("CURRENT_TIMESTAMP")
      .ValueGeneratedOnAdd();
  }
}