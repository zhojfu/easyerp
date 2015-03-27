namespace EasyERP.Desktop
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Desktop.Price;
    using EasyERP.Desktop.Product;
    using EasyERP.Desktop.Stock;
    using Infrastructure.Desktop;
    using System;

    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<ProductModel, Domain.Model.Product>();
            Mapper.CreateMap<Domain.Model.Product, ProductModel>();
            Mapper.CreateMap<StockItemViewModel, RepositoryStock>();
            Mapper.CreateMap<RepositoryStock, StockItemViewModel>();
            Mapper.CreateMap<PriceModel, Domain.Model.Price>();
            Mapper.CreateMap<Domain.Model.Price, PriceModel>();
        }

        public int Order
        {
            get { throw new NotImplementedException(); }
        }
    }
}