using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs;

public class ConversationHub : Hub
{
	private readonly UserConnectionIdService _connectionIdService;

	public ConversationHub(UserConnectionIdService connectionIdService)
	{
		_connectionIdService = connectionIdService;
	}

	[Authorize]
	public override async Task OnConnectedAsync()
	{
		var connectionId = Context.ConnectionId;
		var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

		_connectionIdService.ConnectionIds.Add(new(Guid.Parse(userId), connectionId));
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var connectionId = Context.ConnectionId;
		var item = _connectionIdService.ConnectionIds.First(c => c.Item2 == connectionId);
		_connectionIdService.ConnectionIds.Remove(item);
	}
}

public class UserConnectionIdService
{
	public List<Tuple<Guid, string>> ConnectionIds = new ();
}