using Chat.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Api.Extensions;

public static class WebApplicationExtensions
{
	public static void MigrateChatDbContext(this WebApplication app)
	{
		if (app.Services.GetService<ChatDbContext>() != null)
		{
			var chatDb = app.Services.GetRequiredService<ChatDbContext>();
			chatDb.Database.Migrate();
		}
	}
}