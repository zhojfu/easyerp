using Domain.Model;
using Infrastructure.Domain;

namespace Doamin.Service
{
    public class ProductService
    {
        private IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
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
