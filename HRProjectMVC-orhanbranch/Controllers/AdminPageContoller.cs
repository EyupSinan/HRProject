using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.EmployeeService;
using HRProjectDomain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProjectUI.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly IEmployeeService _employeeService;
        
      
        public AdminPageController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
           //deneme1
        }
        public async Task<IActionResult> Index()
        {
            List<EmployeeVM> model = await _employeeService.GetEmployees();
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDTO model)
        {
           await _employeeService.CreateEmployee(model);
            return RedirectToAction("Index");
        }
    }
}
