using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Models;

public class ProductModel
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int CategoryId { get; set; }
    public required string Photo_Path { get; set; }
}