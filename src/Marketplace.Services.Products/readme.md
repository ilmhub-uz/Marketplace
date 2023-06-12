Entities
	Product
	Category

Models:
	CreateProductModel
	CreateCategoryModel
	ProductModel
	CategoryModel

Repositories
	IProductRepository
	ICategoryRepository

Managers
	ProductManager
	CategoryManager

Controllers
	ProductController
		GetProducts, GetProductById, Create, Update, Delete
	CategoryController
		GetCategories, GetCategoryById, Create, Update, Delete