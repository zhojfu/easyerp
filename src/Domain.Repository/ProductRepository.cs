using Domain.Model;
using Infrastructure.Domain;
using Infrastructure.Domain.EntityFramework;

namespace Domain.Repository
{
    public class ProductRepository : EntityFrameworkRepository<Product>, IProductRepository
    {
        public ProductRepository(IEntityFrameworkDbContext dbcontext, IUnitOfWork unitOfWork)
            : base(dbcontext, unitOfWork)
        {
        }
     
        public void AddNewProduct(Product product)
        {
            RegisterAdd(product);
        }

        public void RemoveProduct(Product product)
        {
            RegisterRemoved(product);
        }

        public void UpdateProduct(Product product)
        {
            RegisterUpdate(product);
        }
    }
}
