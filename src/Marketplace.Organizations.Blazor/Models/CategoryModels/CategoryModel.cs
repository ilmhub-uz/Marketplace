using System.ComponentModel.DataAnnotations;

namespace Marketplace.Organizations.Blazor.Models.CategoryModels;

public class CategoryModel
{
    public Guid Id { get; set; }
    [Required]
    public  string Name { get; set; }

    public List<CategoryModel> ChildCategories { get; set; } = new List<CategoryModel>();
}