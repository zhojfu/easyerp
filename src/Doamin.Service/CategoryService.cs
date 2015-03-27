namespace Doamin.Service
{
    using Domain.Model;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService
    {
        private readonly IRepository<Category> categoryRepository;

        private readonly IRepository<ProductCategory> productCategoryRepository;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IRepository<ProductCategory> productCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productCategoryRepository = productCategoryRepository;
        }

        public IList<Category> GetAllCategories()
        {
            return this.categoryRepository.FindAll(c => true).ToList();
        }

        public virtual IList<ProductCategory> GetProductCategoriesByProductId(
            Guid productId,
            int storeId,
            bool showHidden = false)
        {
            var query = from pc in this.productCategoryRepository.FindAll(i => true)
                        join c in this.categoryRepository.FindAll(t => true) on pc.CategoryId equals c.Id
                        where pc.ProductId == productId
                        orderby pc.DisplayOrder
                        select pc;
            return query.ToList();
        }

        public virtual void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            this.categoryRepository.Add(category);
        }

        public virtual void InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
            {
                throw new ArgumentNullException("productCategory");
            }

            this.productCategoryRepository.Add(productCategory);
        }
    }
}