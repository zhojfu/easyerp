namespace EasyERP.Desktop
{
    using AutoMapper;
    using EasyERP.Desktop.Product;
    using EasyERP.Desktop.Stock;
    using Infrastructure.Desktop;
    using System;

    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<ProductViewModel, Domain.Model.Product>();
            Mapper.CreateMap<StockItemViewModel, Domain.Model.RepositoryStock>();
            Mapper.CreateMap<Domain.Model.RepositoryStock, StockItemViewModel>();
        }

        public int Order
        {
            get { throw new NotImplementedException(); }
        }
    }
}