using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Marketplace.Services.Chat.Hubs;

public class ConversationHub : Hub
{
	[Authorize]
	public override async Task OnConnectedAsync()
	{

	}


}