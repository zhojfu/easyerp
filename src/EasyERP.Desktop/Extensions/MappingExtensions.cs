namespace EasyERP.Desktop.Extensions
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Desktop.ViewModels;

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
            return entity.MapTo<Product, ProductViewModel>();
        }
    }
}