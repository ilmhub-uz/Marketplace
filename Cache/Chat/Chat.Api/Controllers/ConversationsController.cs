using Chat.Api.Hubs;
using Chat.Core.Managers;
using Chat.Core.Models;
using Identity.Core.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly ConversationManager _conversationManager;
    private readonly UserProvider _userProvider;
    private readonly IHubContext<ConversationHub> _conversationHubContext;

    public ConversationsController(
	    ConversationManager conversationManager,
	    UserProvider userProvider,
	    IHubContext<ConversationHub> conversationHubContext)
    {
        _conversationManager = conversationManager;
        _userProvider = userProvider;
        _conversationHubContext = conversationHubContext;
    }

    [HttpGet]
    public async Task<List<ConversationModel>> GetConversations()
    {
        return await _conversationManager.GetConversations(_userProvider.UserId);
    }

	[HttpGet("{conversationId}")]
    public async Task<List<MessageModel>> GetConversationMessages(Guid conversationId)
    {
        return await _conversationManager.GetConversationMessages(conversationId);
    }

    [HttpPost]
    public async Task SaveMessage(NewMessageModel messageModel)
    {
        await _conversationManager.SaveMessage(_userProvider.UserId, messageModel);

        var connectionId = UserConnectionIdService.ConnectionIds
	        .FirstOrDefault(c => c.Item1 == messageModel.ToUserId)?.Item2;
        if (connectionId != null)
        {
	        await _conversationHubContext.Clients.Client(connectionId)
		        .SendAsync("NewMessage", messageModel);
        }


        var connectionId1 = UserConnectionIdService.ConnectionIds
	        .FirstOrDefault(c => c.Item1 == _userProvider.UserId)?.Item2;

        if (connectionId1 != null)
        {
	        await _conversationHubContext.Clients.Client(connectionId1)
		        .SendAsync("NewMessage", new MessageModel()
		        {
			        Text = messageModel.Text,
			        FromUserId = _userProvider.UserId,
                    Date = DateTime.Now
		        });
        }
	}
}