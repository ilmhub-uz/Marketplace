using Marketplace.Services.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Identity.Context;

public class IdentityDbContext : DbContext
{
	public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
	{

	}

	public DbSet<User> Users => Set<User>();
}