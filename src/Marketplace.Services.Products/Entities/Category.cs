namespace Marketplace.Services.Products.Entities;

public class Category
{
	public Guid Id { get; set; }
	public required string Name { get; set; }

	public List<Category> ChildCategories { get; set; } = new List<Category>();
}