namespace AyolUchun.Features.Courses.Filters;

public class CourseFilters
{
  public string? Title { get; set; }
  public int? CategoryId { get; set; }
  public double? MinPrice { get; set; }
  public double? MaxPrice { get; set; }
  public string? OrderBy { get; set; }
}