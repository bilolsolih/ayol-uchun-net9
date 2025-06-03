using AutoMapper;
using AyolUchun.Features.Interviews.DTOs;
using AyolUchun.Features.Interviews.Models;

namespace AyolUchun.Features.Interviews.Profiles;

public class InterviewsProfiles : Profile
{
  public InterviewsProfiles()
  {
    CreateMap<Interview, InterviewListDto>()
      .ForMember(dest => dest.User, opts => opts.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"));
  }
}