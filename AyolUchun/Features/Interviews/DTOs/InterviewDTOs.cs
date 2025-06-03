namespace AyolUchun.Features.Interviews.DTOs;

public record InterviewListDto
{
  public required int Id { get; set; }
  public required string User { get; set; }
  public required string Title { get; set; }
  public required string Image { get; set; }
  public required int Duration { get; set; }
}