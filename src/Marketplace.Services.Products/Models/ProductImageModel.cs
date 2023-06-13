namespace Marketplace.Services.Products.Models;

public class ProductImageModel
{
    public Guid ProductId { get; set; }
    public IFormFile Image;
}