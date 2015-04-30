namespace EasyERP.Web
{
    using AutoMapper;
    using Domain.Model;
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using EasyERP.Web.Models;
    using EasyERP.Web.Models.Products;
    using EasyERP.Web.Models.Stores;

    public static class AutoMapperBootstraper
    {
        public static void RegisterModelMapper()
        {
            Mapper.CreateMap<EmployeeModel, Employee>();
            Mapper.CreateMap<Product, ProductModel>();
            Mapper.CreateMap<ProductModel, Product>();
            Mapper.CreateMap<Inventory, InventoryModel>();
            Mapper.CreateMap<InventoryModel, Inventory>();
            Mapper.CreateMap<Employee, EmployeeModel>();
            Mapper.CreateMap<Employee, EmployeeListModel>();
            Mapper.CreateMap<Store, StoreModel>();
            Mapper.CreateMap<StoreModel, Store>();
            Mapper.CreateMap<PriceModel, ProductPrice>();
        }
    }
}