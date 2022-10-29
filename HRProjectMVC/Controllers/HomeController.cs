using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.DashboardService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProjectUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(IDashboardService dashboardService, UserManager<AppUser> userManager)
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            //var loggedUser = JsonConvert.DeserializeObject(HttpContext.Session.GetString("UserSession"));
            if (User.IsInRole("Manager"))
            {
                AppUser appUser = await _userManager.GetUserAsync(User); //Signed user seçildi
                DashboardVM model = await _dashboardService.GetDashboardManager(appUser.CompanyId.ToString());
                //model.SignedUser = appUser; //Sign in olan userı DashboardVM'e yükledik
                return View(model);         //User + 5 employee view'e gönderildi               

            }
            else if (User.IsInRole("Admin"))
            {
                DashboardVM model = await _dashboardService.GetDashboardAdmin();
                return View(model);
            }
            else
            {
                return View();
            }
        }
    }
}
