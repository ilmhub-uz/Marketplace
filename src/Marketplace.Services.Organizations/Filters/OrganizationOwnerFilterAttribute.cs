using Marketplace.Services.Organizations.Context;
using Marketplace.Services.Organizations.Entities;
using Marketplace.Services.Organizations.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organizations.Filters;

public class OrganizationOwnerFilterAttribute : ActionFilterAttribute
{
	private readonly UserProvider _userProvider;
	private readonly OrganizationsDbContext _organizationsDbContext;

	public OrganizationOwnerFilterAttribute(UserProvider userProvider, OrganizationsDbContext organizationsDbContext)
	{
		_userProvider = userProvider;
		_organizationsDbContext = organizationsDbContext;
	}

	public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var organizationId = context.ActionArguments["organizationId"];

		var organization = await _organizationsDbContext.Organizations
			.Include(o => o.Users)
			.Where(o => o.Id == Guid.Parse((string)organizationId))
			.FirstOrDefaultAsync();

		var userId = _userProvider.UserId;

		var isUserOwner = organization.Users.Any(u => u.UserId == userId
													  && u.UserRole == OrganizationUserRole.Owner);

		if (!isUserOwner)
		{
			context.Result = new ForbidResult();
		}
	}

	public class OrganizationOwner : TypeFilterAttribute
	{
		public OrganizationOwner() : base(typeof(OrganizationOwnerFilterAttribute))
		{
		}
	}
}