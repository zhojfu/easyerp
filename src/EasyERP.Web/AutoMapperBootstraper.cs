namespace EasyERP.Web
{
    using AutoMapper;
    using Domain.Model;
    using EasyERP.Web.Models;

    public class AutoMapperBootstraper
    {
        public static void RegisterModelMapper()
        {
            Mapper.CreateMap<EmployeeModel, Employee>();
        }
    }
}