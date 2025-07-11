﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AyolUchun.Features.Authentication.Services;

public class TokenService(IConfiguration config)
{
  public async Task<string> GenerateTokenAsync(string phoneNumber, int id)
  {
    return await Task.Run(() =>
      {
        var jwtSettings = config.GetSection("JwtSettings");
        var secret = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);
        var claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.PhoneNumber, phoneNumber),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim("userid", id.ToString()),
        };

        var key = new SymmetricSecurityKey(secret);
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
          issuer: jwtSettings["Issuer"],
          audience: jwtSettings["Audience"],
          claims: claims,
          expires: DateTime.Now.AddYears(3),
          signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
      }
    );
  }
}