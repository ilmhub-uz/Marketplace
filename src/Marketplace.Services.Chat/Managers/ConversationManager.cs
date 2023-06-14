using Marketplace.Services.Chat.Context;
using Marketplace.Services.Chat.Entities;
using Marketplace.Services.Chat.Hubs;
using Marketplace.Services.Chat.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.Managers;

public class ConversationManager
{
	private readonly ChatDbContext _dbContext;
	private readonly IHubContext<ConversationHub> _conversationHubContext;

	public ConversationManager(
		ChatDbContext dbContext,
		IHubContext<ConversationHub> conversationHubContext)
	{
		_dbContext = dbContext;
		_conversationHubContext = conversationHubContext;
	}

	public async Task<List<ConversationModel>> GetConversations(Guid userId)
	{
		var conversations = await _dbContext.Conversations
			.Where(conversation => conversation.UserIds.Contains(userId))
			.ToListAsync();

		return conversations.Select(conversation => new ConversationModel()
		{
			FromUserId = conversation.UserIds.First(u => u != userId),
			Id = conversation.Id
		}).ToList();
	}

	public async Task<List<MessageModel>> GetConversationMessages(Guid conversationId)
	{
		var messages = await _dbContext.Messages
			.Where(m => m.ConversationId == conversationId)
			.ToListAsync();

		return messages.Select(message => new MessageModel(message)).ToList();
	}

	public async Task SaveMessage(Guid userId, NewMessageModel messageModel)
	{
		var conversation = await _dbContext.Conversations
			.Where(c =>
				c.UserIds.Contains(userId)
				&& c.UserIds.Contains(messageModel.ToUserId))
			.FirstOrDefaultAsync();

		if (conversation == null)
		{
			conversation = new Conversation()
			{
				UserIds = new List<Guid>() { userId, messageModel.ToUserId }
			};

			_dbContext.Conversations.Add(conversation);
			await _dbContext.SaveChangesAsync();
		}

		var message = new Message()
		{
			ConversationId = conversation.Id,
			Date = DateTime.Now,
			FromUserId = userId,
			Text = messageModel.Text
		};

		//send to hub

		_dbContext.Messages.Add(message);
		await _dbContext.SaveChangesAsync();

		await SendMessagesToHub(conversation.UserIds, new MessageModel(message));
	}

	private async Task SendMessagesToHub(
		List<Guid> userIds,
		MessageModel messageModel)
	{
		await _conversationHubContext.Clients
			.Users(userIds.Select(u => u.ToString()).ToList())
			.SendAsync("NewMessage", messageModel);
	}
}