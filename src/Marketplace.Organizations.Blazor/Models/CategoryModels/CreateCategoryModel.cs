using System.ComponentModel.DataAnnotations;

namespace Marketplace.Organizations.Blazor.Models.CategoryModels;

public class CreateCategoryModel
{
    [Required]
    public  string Name { get; set; }

    public Guid? ParentId
    {
        get
        {
            if (ForGuid != null)
            {
                return Guid.Parse(ForGuid);
            }
            return null;
        }
    }


    public string? ForGuid { get; set; }
}