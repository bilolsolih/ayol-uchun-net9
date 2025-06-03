using AyolUchun.Core;

namespace AyolUchun.Features.Courses.Models;

public class Category : BaseModel
{
  public required string Title { get; set; }
  public required string Icon { get; set; }

  public ICollection<Course> Courses { get; set; } = [];
}