using AyolUchun.Core;

namespace AyolUchun.Features.Authentication.Models;

public enum Gender
{
  Male,
  Female
}

public class User : BaseModel
{
  public required string FirstName { get; set; }
  public required string LastName { get; set; }
  public required string Email { get; set; }
  public required string PhoneNumber { get; set; }
  public required string Password { get; set; }

  public DateOnly? BirthDate { get; set; }
  public Gender? Gender { get; set; }

  public ICollection<Otp> Otps { get; set; } = [];
}