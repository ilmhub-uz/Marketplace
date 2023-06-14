using Marketplace.Services.Chat.Entities;

namespace Marketplace.Services.Chat.Models;

public class ConversationModel
{
	public Guid Id { get; set; }

	public Guid FromUserId { get; set; }
}

public class MessageModel
{
	public Guid Id { get; set; }
	public Guid FromUserId { get; set; }
	public string Text { get; set; }
	public DateTime Date { get; set; }

	public MessageModel(Message message)
	{
		FromUserId = message.FromUserId;
		Date = message.Date;
		Id = message.Id;
		Text = message.Text;
	}
}