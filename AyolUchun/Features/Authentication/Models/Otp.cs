using AyolUchun.Core;

namespace AyolUchun.Features.Authentication.Models;

public class Otp : BaseModel
{
  public required int UserId { get; set; }
  public required string Code { get; set; }
  public DateTime ExpiryDate { get; set; }

  public required User User { get; set; }
}