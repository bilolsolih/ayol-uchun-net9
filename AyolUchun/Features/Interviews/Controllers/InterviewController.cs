using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AyolUchun.Core.Exceptions;
using AyolUchun.Features.Interviews.DTOs;
using AyolUchun.Features.Interviews.Filters;

namespace AyolUchun.Features.Interviews.Controllers;

[ApiController, Route("api/v1/interviews"), Authorize]
public class InterviewController(MyDbContext context, IMapper mapper) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<InterviewListDto>>> ListInterviews([FromQuery] InterviewFilters filters)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var query = context.Interviews.AsQueryable();
    if (filters is { Limit: not null, Page: not null })
    {
      query = query
        .Skip((int)(filters.Limit * (filters.Page - 1)))
        .Take((int)filters.Limit);
    }

    var interviews = await query.ProjectTo<InterviewListDto>(mapper.ConfigurationProvider).ToListAsync();
    var baseUrl = HttpContext.GetUploadsBaseUrl();
    interviews.ForEach(i => { i.Image = $"{baseUrl}/{i.Image}"; });
    return interviews;
  }
}