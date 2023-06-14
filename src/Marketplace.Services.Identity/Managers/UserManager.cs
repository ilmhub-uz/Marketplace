using Marketplace.Services.Identity.Context;
using Marketplace.Services.Identity.Entities;
using Marketplace.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Identity.Managers;

public class UserManager
{
    private readonly IdentityDbContext _dbContext;

    public UserManager(
        JwtTokenManager tokenManager, 
        ILogger<AccountManager> logger,
        IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> UpdateProfileAsync(Guid userId, UpdateUserModel model)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return null;
        }

        user.Name = model.Name ?? user.Name;
        user.UserName = model.UserName ?? user.UserName;


        if (model.Password is not null)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        }

        await _dbContext.SaveChangesAsync();

        return user;
    }


    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();

        return true;
    }


    public async Task<User?> GetUser(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUser(string userName)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<List<UserModel>> GetAllUserAsync()
    {
        var users = await _dbContext.Users.ToListAsync();

        var userModels = new List<UserModel>();

        foreach (var user in users)
        {
            userModels.Add(MapToUserModel(user));
        }

        return userModels;
    }

    public UserModel MapToUserModel(User user)
    {
        var userModel = new UserModel(user);

        return userModel;
    }


}