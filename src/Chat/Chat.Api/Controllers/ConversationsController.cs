using Chat.Core.Managers;
using Chat.Core.Models;
using Identity.Core.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConversationsController : ControllerBase
{
    private readonly ConversationManager _conversationManager;
    private readonly UserProvider _userProvider;

    public ConversationsController(ConversationManager conversationManager, UserProvider userProvider)
    {
        _conversationManager = conversationManager;
        _userProvider = userProvider;
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
    }
}