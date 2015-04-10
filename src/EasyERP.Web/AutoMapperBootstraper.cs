namespace EasyERP.Web
{
    using AutoMapper;
    using Domain.Model;
    using Domain.Model.Products;
    using EasyERP.Web.Models;
    using EasyERP.Web.Models.Products;

    public class AutoMapperBootstraper
    {
        public static void RegisterModelMapper()
        {
            Mapper.CreateMap<EmployeeModel, Employee>();
            Mapper.CreateMap<Product, ProductModel>();
            Mapper.CreateMap<ProductModel, Product>();
        }
    }
}