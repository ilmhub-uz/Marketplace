using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Core.Context;

public class IdentityDbContext : DbContext
{
	public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
	{

	}

	public DbSet<User> Users => Set<User>();
}