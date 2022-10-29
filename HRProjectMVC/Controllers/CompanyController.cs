using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.CompanyService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProjectUI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;

        public CompanyController(UserManager<AppUser> userManager, ICompanyService companyService)
        {
            _companyService = companyService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Details()
        {
            AppUser manager = await _userManager.GetUserAsync(User);
            return View(await _companyService.GetCompanyDetails(manager.CompanyId));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetById(id);
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCompanyDTO model)
        {
            if (ModelState.IsValid)
            {
                await _companyService.UpdateCompany(model);
                TempData["Success"] = "Company has been updated!";
                return RedirectToAction("Companies", "Company");
            }
            else
            {
                TempData["Success"] = "Post has not been updated!";
                return RedirectToAction("Companies");
            }
        }
        public async Task<IActionResult> Companies()
        {
            var companies = await _companyService.GetCompanies();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCompanyDTO model)
        {
            if (ModelState.IsValid)
            {
                _companyService.Create(model);
            }

            return RedirectToAction("Companies");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.Delete(id);
            return RedirectToAction("Companies");
        }
    }
}
