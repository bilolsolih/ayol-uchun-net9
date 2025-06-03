using AutoMapper;
using AyolUchun.Features.Authentication.DTOs;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Authentication.Profiles;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<UserCreateDto, User>();
    CreateMap<User, UserDetailDto>()
      .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.BirthDate));
    CreateMap<UserUpdateDto, User>()
      .ForMember(dest => dest.Gender, opts => opts.MapFrom((src, dest) => src.Gender ?? dest.Gender))
      .ForMember(
        dest => dest.BirthDate,
        opts => opts.MapFrom((src, dest) => src.BirthDate != null ? DateOnly.Parse(src.BirthDate) : dest.BirthDate)
      )
      .ForAllMembers(opts => opts.Condition((dto, user, dtoMember) =>
          {
            if (dtoMember is string str)
              return !string.IsNullOrEmpty(str);

            return dtoMember != null;
          }
        )
      );
    ;
  }
}