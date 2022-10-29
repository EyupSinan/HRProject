using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.AppUserService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Mvc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRProjectApplication.Services.CompanyService;
using Microsoft.AspNetCore.Http;
using HRProjectApplication.Services.EmailService;

namespace HRProjectUI.Controllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly IEmailService _emailService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public AppUserController(UserManager<AppUser> userManager, IAppUserService appUserService, IMapper mapper, ICompanyService companyService, IEmailService emailService)
        {
            _userManager = userManager;
            _appUserService = appUserService;
            _mapper = mapper;
            _companyService = companyService;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {

            AppUser manager = await _userManager.GetUserAsync(User);
            //var users =  await _appUserService.GetAppUserEmployees(manager.CompanyId); users.ToPagedList(1,5);

            return View((await _appUserService.GetAppUserEmployees(manager.CompanyId)).ToPagedList(page, 5));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserEmployeeDTO model)
        {
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            AppUser manager = await _userManager.GetUserAsync(User);

            AppUser user = new AppUser();

            string password = Guid.NewGuid().ToString().Split('-')[1] + Guid.NewGuid().ToString().Split('-')[1];

            user.PasswordHash = passwordHasher.HashPassword(user, password);

            user.UserName = model.Username;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.CompanyId = manager.CompanyId;
            user.BirthDate = model.BirthDate;

            user.Status = HRProjectDomain.Enums.Status.Active;
            user.CreateDate = DateTime.Now;

            IdentityResult result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Employee");
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
                return View(model);
            }
            else
            {
                TempData["Success"] = "Employee has been created!";

                _emailService.SendUserInfoEmail(user, manager.Email, password);
                return RedirectToAction("Index", "AppUser");
            }
        }

        public IActionResult CreateManager()
        {
            var companies = _companyService.GetCompanies().Result;
            var appUserManagerDTO = new AppUserManagerDTO()
            {
                Companies = companies
            };
            return View(appUserManagerDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(AppUserManagerDTO model)
        {
            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            AppUser admin = await _userManager.GetUserAsync(User);

            AppUser user = new AppUser();
            string password = Guid.NewGuid().ToString().Split('-')[1] + Guid.NewGuid().ToString().Split('-')[1];
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            user.UserName = model.Username;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.CompanyId = model.CompanyId;
            user.Status = HRProjectDomain.Enums.Status.Active;
            user.CreateDate = DateTime.Now;

            IdentityResult result = await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Manager");
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                return View(model);
            }
            else
            {
                _emailService.SendUserInfoEmail(user, admin.Email, password);
                return RedirectToAction("Companies", "Company");
            }
        }

        public async Task<IActionResult> EmployeeDetails(string id)
        {

            AppUser employee = await _userManager.FindByIdAsync(id);

            return View(_mapper.Map<UpdateAppUserEmployeeDTO>(employee));
        }

        public async Task<IActionResult> AccountDetails()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            var userVM = _mapper.Map<AppUserEmployeeVM>(appUser);
            return View(userVM);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _appUserService.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAppUserEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await _userManager.FindByIdAsync(model.Id);
                appUser.Email = model.Email;
                appUser.PhoneNumber = model.PhoneNumber;
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(appUser);
            }
            return RedirectToAction("AccountDetails", "AppUser");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(string id)
        {
            UpdateAppUserEmployeeDTO model = await _appUserService.GetEmployeeById(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateAppUserEmployeeDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);

                user.UserName = model.Username;
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.CompanyId = model.CompanyId;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.UpdateDate = DateTime.Now;
                user.Status = HRProjectDomain.Enums.Status.Modified;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
            }
            return RedirectToAction("Index", "AppUser");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _appUserService.Delete(id);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            return View(await _appUserService.GetByIdForPassword(id));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UpdatePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);

                PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
                user.PasswordHash = ph.HashPassword(user, model.Password);
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                return RedirectToAction("AccountDetails", "AppUser");
            }
            return View(model);
        }
    }
}
