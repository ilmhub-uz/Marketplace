using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Services.Products.Entities;

public class Product
{
	[BsonId]
	public Guid Id { get; set; } = Guid.NewGuid();
	public required string Name { get; set; }
	public string? Description { get; set; }

	public decimal Price { get; set; }

	public int CategoryId { get; set; }

	public List<ProductImage>? Images { get; set; }
}

public class ProductImage
{
	public int Order { get; set; }
	public required string Path { get; set; }
}