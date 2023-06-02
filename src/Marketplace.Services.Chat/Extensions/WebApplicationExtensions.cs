using Marketplace.Services.Chat.Context;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.Extensions;

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