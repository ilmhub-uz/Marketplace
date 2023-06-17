namespace Marketplace.Services.Chat.Models;

public class NewMessageModel
{
	public Guid ToUserId { get; set; }

	public required string Text { get; set; }
}