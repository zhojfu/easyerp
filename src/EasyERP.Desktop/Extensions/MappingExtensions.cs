namespace EasyERP.Desktop.Extensions
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Desktop.Price;
    using EasyERP.Desktop.Product;
    using EasyERP.Desktop.Stock;

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

        public static ProductModel ToModel(this Product entity)
        {
            return entity.MapTo<Product, ProductModel>();
        }

        public static Product ToEntity(this ProductModel model)
        {
            return model.MapTo<ProductModel, Product>();
        }

        public static StockItemViewModel ToModel(this RepositoryStock entity)
        {
            return entity.MapTo<RepositoryStock, StockItemViewModel>();
        }

        public static RepositoryStock ToEntity(this StockItemViewModel model)
        {
            return model.MapTo<StockItemViewModel, RepositoryStock>();
        }

        public static PriceModel ToModel(this Price entity)
        {
            return entity.MapTo<Price, PriceModel>();
        }

        public static Price ToEntity(this PriceModel model)
        {
            return model.MapTo<PriceModel, Price>();
        }
    }
}