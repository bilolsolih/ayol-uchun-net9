namespace AyolUchun.Features.Courses.DTOs;

public record CategoryListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Icon { get; set; }
  public required int TotalCourses { get; set; }
}