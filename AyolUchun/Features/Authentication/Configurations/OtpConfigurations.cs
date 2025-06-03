using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Authentication.Configurations;

public class OtpConfigurations : IEntityTypeConfiguration<Otp>
{
  public void Configure(EntityTypeBuilder<Otp> builder)
  {
    builder.ToTable("otp_codes");
    builder.HasKey(c => c.Id);

    builder.HasOne(c => c.User)
      .WithMany(u => u.Otps)
      .HasForeignKey(c => c.UserId);

    builder.HasIndex(c => new { c.UserId, c.Code })
      .IsUnique();

    builder.Property(c => c.Id)
      .HasColumnName("id");


    builder.Property(c => c.UserId)
      .HasColumnName("user_id")
      .IsRequired();

    builder.Property(c => c.Code)
      .HasColumnName("code")
      .IsRequired()
      .HasMaxLength(4);


    builder.Property(c => c.ExpiryDate)
      .HasColumnName("expiry_date")
      .IsRequired();

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