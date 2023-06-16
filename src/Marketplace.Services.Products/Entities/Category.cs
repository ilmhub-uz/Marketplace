namespace Marketplace.Services.Products.Entities;

public class Category
{
	//id(int => guid)
	
	public Guid Id { get; set; } = Guid.NewGuid();
	public required string Name { get; set; }

	public List<Category> ChildCategories { get; set; } = new List<Category>();
}