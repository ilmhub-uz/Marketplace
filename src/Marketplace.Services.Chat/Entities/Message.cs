﻿namespace Marketplace.Services.Chat.Entities;

public class Message
{
	public Guid Id { get; set; }

	public Guid ConversationId { get; set; }
	public Conversation? Conversation { get; set; }

	public Guid FromUserId { get; set; }

	public required string Text { get; set; }
	public DateTime Date { get; set; }
}