using System.Web.Mvc;

namespace EasyERP.Web.Controllers
{
    using Antlr.Runtime.Misc;
    using AutoMapper;
    using Doamin.Service.Factory;
    using Domain.Model;
    using EasyERP.Web.Models;

    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: /Employee/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel employee)
        {
            Employee e = Mapper.Map<EmployeeModel, Employee>(employee);
            
            this.employeeService.AddEmployee(e);
            return View();

        }
	}
}