using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.ManagerService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProjectUI.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        public async Task<IActionResult> Index()
        {
            var managers = await _managerService.GetManagers();
            return View(managers);
        }
    }
}
