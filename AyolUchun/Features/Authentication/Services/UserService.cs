﻿using System.Text;
using AutoMapper;
using AyolUchun.Core;
using AyolUchun.Core.Exceptions;
using AyolUchun.Features.Authentication.DTOs;
using AyolUchun.Features.Authentication.Models;
using AyolUchun.Features.Authentication.Repositories;

namespace AyolUchun.Features.Authentication.Services;

public class UserService(
  UserRepository userRepo,
  OtpRepository otpRepo,
  IEmailSender emailSender,
  IMapper mapper,
  IWebHostEnvironment webEnv,
  IHttpContextAccessor httpContextAccessor
) : ServiceBase("profiles", webEnv, httpContextAccessor)
{
  public async Task<User> CreateUserAsync(UserCreateDto payload)
  {
    var user = mapper.Map<User>(payload);

    return await userRepo.AddAsync(user);
  }


  public async Task<UserDetailDto> GetUserByIdAsync(int id)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");

    return mapper.Map<UserDetailDto>(user);
  }

  public async Task<User?> GetUserByLoginAsync(string value)
  {
    var user = await userRepo.GetByPhoneNumberAsync(value);
    return user;
  }

  public async Task<UserDetailDto> UpdateUserAsync(int id, UserUpdateDto payload)
  {
    var user = await userRepo.GetByIdAsync(id);
    DoesNotExistException.ThrowIfNull(user, $"User with id: {id} does not exist");
    mapper.Map(payload, user);
    user = await userRepo.UpdateAsync(user);

    return mapper.Map<UserDetailDto>(user);
  }

  public async Task SendOtpToEmailAsync(SendOtpDto payload)
  {
    var user = await userRepo.GetByPhoneNumberAsync(payload.Email);
    if (user == null) return;

    var oldOtps = await otpRepo.GetAllByUserEmailAsync(user.Email);
    await otpRepo.DeleteAllAsync(oldOtps);

    var code = Random.Shared.Next(minValue: 0, maxValue: 9999).ToString();
    var otpCode = new StringBuilder();
    for (int i = 0; i < 4 - code.Length; i++)
    {
      otpCode.Append('0');
    }

    otpCode.Append(code);

    var newOtp = new Otp
    {
      UserId = user.Id,
      User = user,
      Code = otpCode.ToString(),
      ExpiryDate = DateTime.UtcNow.AddMinutes(10)
    };
    await otpRepo.AddOtpAsync(newOtp);

    await emailSender.SendEmailAsync(user.Email, "Store App verification code", $"Your code: {newOtp.Code}");
  }

  public async Task<bool> VerifyOtpAsync(VerifyOtpDto payload)
  {
    var otpCode = await otpRepo.GetByEmailAndCodeAsync(payload.Email, payload.Code);
    DoesNotExistException.ThrowIfNull(otpCode, payload.ToString());

    if (otpCode.ExpiryDate >= DateTime.UtcNow)
    {
      return true;
    }

    return false;
  }

  public async Task<User> ResetPasswordAsync(ResetPasswordDto payload)
  {
    var user = await userRepo.GetByPhoneNumberAsync(payload.Email);
    DoesNotExistException.ThrowIfNull(user, payload.ToString());

    var otp = await otpRepo.GetByEmailAndCodeAsync(payload.Email, payload.Code);
    DoesNotExistException.ThrowIfNull(otp, payload.ToString());

    if (otp.ExpiryDate < DateTime.UtcNow)
    {
      throw new OtpCodeExpiredException();
    }

    user.Password = payload.Password;
    await userRepo.UpdateAsync(user);
    return user;
  }
}