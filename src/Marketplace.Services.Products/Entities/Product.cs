namespace Marketplace.Services.Products.Entities;

public class Product
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public string? Description { get; set; }

	public decimal Price { get; set; }

	public int CategoryId { get; set; }
	public Category? Category { get; set; }

	public List<ProductImage>? Images { get; set; }
}

public class ProductImage
{
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public required string Path { get; set; }
}