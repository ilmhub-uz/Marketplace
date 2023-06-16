namespace Marketplace.Services.Products.Models;

public class CreateCategoryModel
{
	public required string Name { get; set; }
	public int? ParentId { get; set; }
}