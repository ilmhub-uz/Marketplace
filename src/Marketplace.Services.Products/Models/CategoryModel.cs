using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Models;

public class CategoryModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? ParentId { get; set; }
    public List<CategoryModel>? ChildCategories { get; set; }
    public List<ProductModel> Products { get; set; }
}