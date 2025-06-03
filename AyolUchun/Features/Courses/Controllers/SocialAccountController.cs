using AyolUchun.Features.Courses.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AyolUchun.Features.Courses.Controllers;

[ApiController, Route("api/v1/social-accounts"), Authorize]
public class SocialAccountController(MyDbContext context) : ControllerBase
{
  [HttpGet("list")]
  public async Task<ActionResult<SocialAccountListDto>> ListSocialAccounts()
  {
    var baseUrl = HttpContext.GetUploadsBaseUrl();

    var socialAccounts = await context.SocialAccounts
      .Select(c => new SocialAccountListDto
        {
          Id = c.Id,
          Title = c.Title,
          Link = c.Link,
          Icon = $"{baseUrl}/{c.Icon}",
        }
      )
      .ToListAsync();

    return Ok(socialAccounts);
  }
}