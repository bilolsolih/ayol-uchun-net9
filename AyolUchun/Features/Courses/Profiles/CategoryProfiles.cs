using AutoMapper;
using AyolUchun.Features.Courses.DTOs;
using AyolUchun.Features.Courses.Models;

namespace AyolUchun.Features.Courses.Profiles;

public class CategoryProfiles : Profile
{
  public CategoryProfiles()
  {
    CreateMap<Category, CategoryListDto>();
  }
}