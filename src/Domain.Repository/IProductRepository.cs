using Domain.Model;

namespace Domain.Repository
{
    public interface IProductRepository
    {
        void AddNewProduct(Product product);
        void RemoveProduct(Product product);
        void UpdateProduct(Product product);
    }
}
