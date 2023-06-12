using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models;
using Marketplace.Services.Products.Repositories;

namespace Marketplace.Services.Products.Managers;

public class CategoryManager
{
   private readonly ICategoryRepository _categoryRepository;
   public CategoryManager(ICategoryRepository categoryRepository)
   {
       _categoryRepository = categoryRepository;
   }

   public async Task<CategoryModel> AddCategory(CreateCategoryModel? model)
   {
       if (model == null) return null!;
       var category = new Category()
       {
           Name = model.Name,
           ParentId = model.ParentId
       };

       await _categoryRepository.AddCategory(category);
       return ParseCategoryModel(category);
   }


   public async Task<CategoryModel> GetById(int categoryId)
   {
       var category = await _categoryRepository.GetCategoryById(categoryId);
       if (category == null) return null!;
       return ParseCategoryModel(category);
   }

   public async Task<IEnumerable<CategoryModel>> GetCategories()
   {
       var categoryModels = new List<CategoryModel>();
       foreach (var category in _categoryRepository.Categories)
       {
           categoryModels.Add(ParseCategoryModel(category));
       }
       return categoryModels;
   }
   public async Task<CategoryModel> UpdateCategory(CreateCategoryModel? model, int categoryId)
   {
       var category = await _categoryRepository.GetCategoryById(categoryId);
       if (model == null) return null!;

       category.Name = model.Name;
       category.ParentId = model.ParentId;
       await _categoryRepository.UpdateCategory(category);
       return ParseCategoryModel(category);
   }


   public async Task<string> DeleteCategory(int categoryId)
   {
       var category = _categoryRepository.GetCategoryById(categoryId);
       if(category == null!) return "Not found";
       await _categoryRepository.DeleteCategory(await category);
       return "This category was deleted";
   }










    private CategoryModel ParseCategoryModel(Category category)
   {
        if(category == null!) return null!;
        return new CategoryModel()
        {
            Id = category.Id,
            Name = category.Name,
            ParentId = category.ParentId,
            ChildCategories = category.ChildCategories,
            Products = category.Products,
            
        };
   }


}