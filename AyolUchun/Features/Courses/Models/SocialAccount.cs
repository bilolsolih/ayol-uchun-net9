using AyolUchun.Core;

namespace AyolUchun.Features.Courses.Models;

public class SocialAccount : BaseModel
{
  public required string Title { get; set; }
  public required string Link { get; set; }
  public required string Icon { get; set; }
}