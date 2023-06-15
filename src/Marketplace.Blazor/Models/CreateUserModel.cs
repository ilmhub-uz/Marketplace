using System.ComponentModel.DataAnnotations;

namespace Marketplace.Blazor.Models;

public class CreateUserModel
{
	public string Name { get; set; }
	public string Password { get; set; }
	[Compare("Password",
		ErrorMessage = "Passwords must be same")]
	public string ConfirmPassword { get; set; }
	public string UserName { get; set; }
}