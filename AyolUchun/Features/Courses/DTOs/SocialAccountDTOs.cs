namespace AyolUchun.Features.Courses.DTOs;

public record SocialAccountListDto
{
  public required int Id { get; set; }
  public required string Title { get; set; }
  public required string Link { get; set; }
  public required string Icon { get; set; }
}