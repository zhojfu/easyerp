namespace Doamin.Service.Products
{
    using System;
    using System.Linq;
    using Domain.Model.Products;
    using Domain.Model.Security;
    using EasyErp.Core;
    using Infrastructure.Domain;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<AclRecord> aclRepository;

        private readonly IRepository<Category> categoryRepository;

        private readonly IRepository<Product> productRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly IWorkContext workContext;

        public CategoryService(
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IRepository<AclRecord> aclRepository,
            IWorkContext workContext,
            IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.aclRepository = aclRepository;
            this.workContext = workContext;
            this.unitOfWork = unitOfWork;
        }

        public virtual void DeleteCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            category.Deleted = true;
            UpdateCategory(category);
        }

        public virtual IPagedList<Category> GetAllCategories(
            string categoryName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue)
        {
            var categories = categoryRepository.FindAll(c => true);
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                categories = categories.Where(c => c.Name.Contains(categoryName));
            }

            categories = categories.Where(c => !c.Deleted).OrderBy(c => c.Id);

            return new PagedList<Category>(categories, pageIndex, pageSize);
        }

        public virtual Category GetCategoryById(int categoryId)
        {
            return categoryId == 0 ? null : categoryRepository.GetByKey(categoryId);
        }

        public virtual void InsertCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            categoryRepository.Add(category);
            unitOfWork.Commit();
        }

        public virtual void UpdateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            categoryRepository.Update(category);
            unitOfWork.Commit();
        }
    }
}