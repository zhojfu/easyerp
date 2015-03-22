namespace EasyERP.Desktop
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Desktop.ViewModels;
    using Infrastructure.Desktop;
    using System;

    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<ProductViewModel, Product>();
        }

        public int Order
        {
            get { throw new NotImplementedException(); }
        }
    }
}