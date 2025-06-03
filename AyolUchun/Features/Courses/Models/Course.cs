using AyolUchun.Core;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Courses.Models;

public enum CourseStatus
{
  Bestseller,
  Recommended,
}

public class Course : BaseModel
{
  public required int UserId { get; set; }
  public User User { get; set; }
  
  public required int CategoryId { get; set; }
  public Category Category { get; set; }

  public required string Title { get; set; }
  public required string Image { get; set; }
  public required double Price { get; set; }
  public required double Rating { get; set; }

  public CourseStatus? Status { get; set; }
}