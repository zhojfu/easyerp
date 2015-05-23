namespace EasyERP.Web
{
    using AutoMapper;
    using Domain.Model;
    using Domain.Model.Customer;
    using Domain.Model.Factory;
    using Domain.Model.Products;
    using Domain.Model.Stores;
    using EasyERP.Web.Models.Customer;
    using EasyERP.Web.Models.Employee;
    using EasyERP.Web.Models.Factory;
    using EasyERP.Web.Models.Products;
    using EasyERP.Web.Models.Stores;

    public class AutoMapperBootstraper
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

            Mapper.CreateMap<Consumption, ConsumptionModel>();
            Mapper.CreateMap<Customer, CustomerModel>();
            Mapper.CreateMap<CustomerModel, Customer>();
            Mapper.CreateMap<Customer, CustomerListModel>();
            Mapper.CreateMap<ConsumptionModel, Consumption>();
            Mapper.CreateMap<Timesheet, TimesheetModel>();
            Mapper.CreateMap<TimesheetModel, Timesheet>();
        }
    }
}