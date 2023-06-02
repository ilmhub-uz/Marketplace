using Chat.Core.Context;
using Chat.Core.Entities;
using Chat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Core.Managers;

public class ConversationManager
{
    private readonly ChatDbContext _dbContext;

    public ConversationManager(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
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

        return messages.Select(message => new MessageModel()
        {
            FromUserId = message.FromUserId,
            Date = message.Date,
            Id = message.Id,
            Text = message.Text
        }).ToList();
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
    }
}