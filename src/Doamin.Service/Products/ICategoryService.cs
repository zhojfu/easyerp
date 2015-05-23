namespace Doamin.Service.Products
{
    using Domain.Model.Products;
    using EasyErp.Core;

    public interface ICategoryService
    {
        void DeleteCategory(Category category);

        IPagedList<Category> GetAllCategories(
            string categoryName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue);

        Category GetCategoryById(int categoryId);
        void InsertCategory(Category category);
        void UpdateCategory(Category category);
    }
}