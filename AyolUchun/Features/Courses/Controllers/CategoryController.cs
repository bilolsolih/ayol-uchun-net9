using AyolUchun.Features.Courses.DTOs;
using AyolUchun.Features.Courses.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyolUchun.Features.Courses.Controllers;

[ApiController, Route("api/v1/categories"), Authorize]
public class CategoryController(MyDbContext context) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<CategoryListDto>> ListCategories([FromQuery] CategoryFilters filters)
  {
    var query = context.Categories
      .Include(c => c.Courses)
      .AsQueryable();

    if (filters is { Limit: not null, Page: not null })
    {
      query = query
        .Skip((int)(filters.Limit * (filters.Page - 1)))
        .Take((int)filters.Limit);
    }

    var baseUrl = HttpContext.GetUploadsBaseUrl();

    var categories = await query
      .Select(c => new CategoryListDto
        {
          Id = c.Id,
          Title = c.Title,
          Icon = $"{baseUrl}/{c.Icon}",
          TotalCourses = c.Courses.Count,
        }
      )
      .ToListAsync();

    return Ok(categories);
  }
}