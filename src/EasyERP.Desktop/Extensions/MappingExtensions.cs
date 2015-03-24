namespace EasyERP.Desktop.Extensions
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Desktop.Product;
    using EasyERP.Desktop.Stock;
    using Infrastructure;
    using System.Linq;

    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public static Product ToEntity(this ProductViewModel model)
        {
            return model.MapTo<ProductViewModel, Product>();
        }

        public static ProductViewModel ToViewModel(this Product entity)
        {
            //return entity.MapTo<Product, ProductViewModel>();

            var vm = new ProductViewModel
            {
                Name = entity.Name,
                Description = entity.Description,
                Upc = entity.Upc,
                Unit = entity.Unit,
                Origin = "China"
            };
            if (!entity.Prices.Any())
            {
                vm.Price = null;
            }
            vm.Price = entity.Prices.Aggregate(
                (latest, price) =>
                (latest == null || latest.UpdataTime > price.UpdataTime ? latest : price))
                             .IfNotNull(p => p.SalePrice);

            vm.StockQuantity = entity.RepositoryStocks.Sum(i => i.Quantity);

            return vm;
        }

        public static StockItemViewModel ToModel(this RepositoryStock entity)
        {
            return entity.MapTo<RepositoryStock, StockItemViewModel>();
        }

        public static RepositoryStock ToEntity(this StockItemViewModel model)
        {
            return model.MapTo<StockItemViewModel, RepositoryStock>();
        }
    }
}