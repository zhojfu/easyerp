using Domain.Model;
using Domain.Repository;

namespace Doamin.Service
{
    public class ProductService
    {
        private IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void AddNewProduct( /*ViewProduct viewModel*/)
        {
            
        }

        private Product ConvertViewModelToModel()
        {
            return null;
        }
    }
}
