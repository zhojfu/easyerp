using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Web.Extensions
{
    using AutoMapper;
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using EasyERP.Web.Models.Products;
    using EasyERP.Web.Models.Stores;

    public static class MapperExtensions
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

        public static Product ToEntity(this ProductModel model, Product destination)
        {
            return model.MapTo(destination);
        }

        public static InventoryModel ToModel(this Inventory entity)
        {
            return entity.MapTo<Inventory, InventoryModel>();
        }

        public static Inventory ToEntity(this  InventoryModel model)
        {
            return model.MapTo<InventoryModel, Inventory>();
        }

        public static Store ToEntity(this  StoreModel model)
        {
            return model.MapTo<StoreModel, Store>();
        }

        public static StoreModel ToModel(this Store entity)
        {
            return entity.MapTo<Store, StoreModel>();
        }
    }
}