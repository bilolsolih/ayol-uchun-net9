using AyolUchun.Features.Courses.Models;

namespace AyolUchun.Features.Courses.DTOs;

public record CourseListDto
{
  public required int Id { get; set; }
  public required string User { get; set; }
  public required string Category { get; set; }
  public required string Title { get; set; }
  public required string Image { get; set; }
  public required double Price { get; set; }
  public required double Rating { get; set; }
  public CourseStatus? Status { get; set; }
}