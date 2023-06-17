using Marketplace.Services.Chat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.Context;

public class ChatDbContext : DbContext
{
	public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
	{

	}

	public DbSet<Conversation> Conversations => Set<Conversation>();
	public DbSet<Message> Messages => Set<Message>();
}