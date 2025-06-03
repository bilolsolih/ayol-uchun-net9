using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AyolUchun.Features.Authentication.Models;

namespace AyolUchun.Features.Authentication.Repositories;

public class UserRepository(MyDbContext context, IMapper mapper)
{
  public async Task<User> AddAsync(User user)
  {
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return user;
  }

  public async Task<User?> GetByIdAsync(int id)
  {
    return await context.Users.FindAsync(id);
  }

  public async Task<User> UpdateAsync(User user)
  {
    user.Updated = DateTime.UtcNow;
    context.Users.Update(user);
    await context.SaveChangesAsync();
    return user;
  }

  public async Task<bool> ExistsByIdAsync(int id)
  {
    var exists = await context.Users.AnyAsync(u => u.Id == id);
    return exists;
  }

  public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
  {
    return await context.Users.SingleOrDefaultAsync(u => u.PhoneNumber.ToLower() == phoneNumber.ToLower());
  }

  public async Task<bool> ExistsByEmailAsync(string email)
  {
    return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
  }
}