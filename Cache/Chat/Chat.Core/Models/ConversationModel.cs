namespace Chat.Core.Models;

public class ConversationModel
{
    public Guid Id { get; set; }

    public Guid FromUserId { get; set; }
}

public class MessageModel
{
    public Guid Id { get; set; }
    public Guid FromUserId { get; set; }

    public required string Text { get; set; }
    public DateTime Date { get; set; }
}