namespace Marketplace.Services.Products.Models;

public class CategoryModel
{
	public int Id { get; set; }
	public required string Name { get; set; }

	public List<CategoryModel> ChildCategories { get; set; } = new List<CategoryModel>();
}