using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Identity.Core.Providers;

public class UserProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    protected HttpContext? Context => _contextAccessor.HttpContext;

    public string UserName => Context.User.FindFirstValue(ClaimTypes.Name);
    public Guid UserId => Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
}