using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AyolUchun.Core.Exceptions;
using AyolUchun.Features.Courses.DTOs;
using AyolUchun.Features.Courses.Filters;

namespace AyolUchun.Features.Courses.Controllers;

[ApiController, Route("api/v1/courses"), Authorize]
public class CourseController(MyDbContext context) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<CourseListDto>>> ListCourses([FromQuery] CourseFilters filters)
  {
    var userId = int.Parse(User.FindFirstValue("userid")!);
    var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
    DoesNotExistException.ThrowIfNull(user, $"userId: {userId}");

    var courses = context.Courses.AsQueryable();

    if (filters is { CategoryId: not null })
    {
      courses = courses.Where(p => p.CategoryId == filters.CategoryId);
    }

    if (filters is { Title: not null })
    {
      courses = courses.Where(p => p.Title.ToLower().Contains(filters.Title.ToLower()));
    }

    if (filters is { MinPrice: not null, MaxPrice: not null })
    {
      courses = courses.Where(p => p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice);
    }

    if (filters is { OrderBy: not null })
    {
      courses = filters.OrderBy.ToLower() switch
      {
        "-price" => courses.OrderByDescending(p => p.Price),
        "price" => courses.OrderBy(p => p.Price),
        _ => courses.OrderBy(p => p.Created)
      };
    }

    return await courses.Select(p => new CourseListDto
      {
        Id = p.Id,
        User = $"{p.User.FirstName} {p.User.LastName}",
        Category = p.Category.Title,
        Title = p.Title,
        Image = HttpContext.GetUploadsBaseUrl() + '/' + p.Image,
        Price = p.Price,
        Rating = p.Rating,
        Status = p.Status,
      }
    ).ToListAsync();
  }
}