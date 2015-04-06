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
            Mapper.CreateMap<Employee, EmployeeModel>();
            Mapper.CreateMap<Employee, EmployeeListModel>();
        }
    }
}