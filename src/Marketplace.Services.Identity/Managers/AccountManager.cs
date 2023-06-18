using Marketplace.Services.Identity.Context;
using Marketplace.Services.Identity.Entities;
using Marketplace.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Marketplace.Services.Identity.Managers;

public class AccountManager
{
    private readonly IdentityDbContext _dbContext;
    private ILogger<AccountManager> _logger;
    private readonly JwtTokenManager _tokenManager;

    public AccountManager(
        JwtTokenManager tokenManager, 
        ILogger<AccountManager> logger, 
        IdentityDbContext dbContext)
    {
        _tokenManager = tokenManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<User> Register(CreateUserModel createUserModel)
    {
        if (await _dbContext.Users.AnyAsync(u => u.UserName == createUserModel.UserName))
        {
            throw new Exception("UserName already exists.");
        }

        var user = new User()
        {
            UserName = createUserModel.UserName,
            Name = createUserModel.Name
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, createUserModel.Password);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
    
    public async Task<string> Login(LoginUserModel loginUserModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.UserName);
        if (user == null)
        {
            throw new Exception("UserName or Password is incorrect");
        }

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, loginUserModel.Password);

        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception("UserName or Password is incorrect");
        }

        var token = _tokenManager.GenerateToken(user);

        return token;
    }

 
}