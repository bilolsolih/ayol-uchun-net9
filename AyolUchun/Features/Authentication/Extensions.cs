﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AyolUchun.Features.Authentication.Repositories;
using AyolUchun.Features.Authentication.Services;

namespace AyolUchun.Features.Authentication;

public static class AuthenticationExtensions
{
  public static void RegisterAuthenticationFeature(this IServiceCollection services, ConfigurationManager config)
  {
    services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }
    ).AddJwtBearer(options =>
      {
        var jwtSettings = config.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtSettings["Issuer"],
          ValidAudience = jwtSettings["Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)),
          ClockSkew = TimeSpan.Zero
        };
      }
    );
    services.AddScoped<UserRepository>();
    services.AddScoped<OtpRepository>();
    services.AddScoped<TokenService>();
    services.AddScoped<UserService>();
    services.AddTransient<IEmailSender, MailKitEmailSender>();
  }
}