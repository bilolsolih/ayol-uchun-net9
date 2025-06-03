using AutoMapper;
using AyolUchun.Features.Courses.DTOs;
using AyolUchun.Features.Courses.Models;

namespace AyolUchun.Features.Courses.Profiles;

public class CourseProfiles : Profile
{
  public CourseProfiles()
  {
    CreateMap<Course, CourseListDto>();
  }
}