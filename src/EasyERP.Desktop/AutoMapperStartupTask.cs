namespace EasyERP.Desktop
{
    using AutoMapper;
    using EasyERP.Desktop.Product;
    using Infrastructure.Desktop;
    using System;

    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<ProductViewModel, Domain.Model.Product>();
        }

        public int Order
        {
            get { throw new NotImplementedException(); }
        }
    }
}