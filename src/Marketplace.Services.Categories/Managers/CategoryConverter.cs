using Categories.Entites;
using Categories.Models;

public static class CategoryConverter
{
   public static List<CategoryModel> ConvertToCategoryModels(List<Category> categories)
   {
      var categoryModels = new List<CategoryModel>();
      var parentCategories = categories.Where(c => c.ParentId == null);

      foreach (var category in parentCategories)
      {
         var categoryModel = CreateCategoryModel(category, categories);
         categoryModels.Add(categoryModel);
      }

      return categoryModels;
   }

   private static CategoryModel CreateCategoryModel(Category category, List<Category> categories)
   {
      var categoryModel = new CategoryModel
      {
         Id = category.Id,
         Name = category.Name,
         Key = category.Key,
         SubCategories = new List<CategoryModel>()
      };

      var subCategories = categories.Where(c => c.ParentId == category.Id).ToList();
      
      foreach (var subCategory in subCategories)
      {
         var subCategoryModel = CreateCategoryModel(subCategory, categories);
         categoryModel.SubCategories.Add(subCategoryModel);
      }

      return categoryModel;
   }
}