using AyolUchun.Core;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Interviews.Models;

public class Interview : BaseModel
{
  public required int UserId { get; set; }
  public User User { get; set; }

  public required string Title { get; set; }
  public required string Image { get; set; }
  public required int Duration { get; set; }
}