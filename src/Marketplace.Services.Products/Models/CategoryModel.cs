namespace Marketplace.Services.Products.Models;

public class CategoryModel
{
	public Guid Id { get; set; }
	public required string Name { get; set; }

	public List<CategoryModel> ChildCategories { get; set; } = new List<CategoryModel>();
}