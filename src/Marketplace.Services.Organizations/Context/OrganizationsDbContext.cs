using Marketplace.Services.Organizations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organizations.Context;

public class OrganizationsDbContext : DbContext
{
	public DbSet<Organization> Organizations => Set<Organization>();
	public DbSet<OrganizationUser> OrganizationsUser => Set<OrganizationUser>();
	public DbSet<OrganizationAddress> OrganizationsAddress => Set<OrganizationAddress>();

	public OrganizationsDbContext(DbContextOptions<OrganizationsDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<OrganizationUser>()
			.HasKey(o => new { o.UserId, o.OrganizationId });
	}
}