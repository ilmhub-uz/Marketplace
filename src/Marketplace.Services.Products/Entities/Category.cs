namespace Marketplace.Services.Products.Entities;

public class Category
{
	public int Id { get; set; }
	public required string Name { get; set; }

	public int? ParentId { get; set; }
	public Category? Parent { get; set; }

	public List<Category>? ChildCategories { get; set; }

	public List<Product>? Products { get; set; }

}