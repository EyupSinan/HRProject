using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using HRProjectDomain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICompanyRepository _companyRepository;

        public AppUserService(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserRepository appUserRepository, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
            _companyRepository = companyRepository;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            var result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new UpdateProfileDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email
                },
                where: x => x.UserName == userName);

            return result;
        }

        public async Task<AppUserVM> GetUserByUserName(string userName)
        {
            var result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Name = x.Name,
                    SurName = x.Surname
                },
                where: x => x.UserName == userName);

            return result;
        }

        public async Task Create(AppUserEmployeeDTO model)
        {
            AppUser appUser = _mapper.Map<AppUser>(model);
            await _appUserRepository.Create(appUser);
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }
        public async Task Delete(string id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);
            appUser.Status = Status.Passive;
            appUser.DeleteDate = DateTime.Now;
            await _appUserRepository.Delete(appUser);
        }

        public async Task UpdateUser(UpdateProfileDTO model) // Userın kendi bilgisini update etmesi
        {
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id); //DB'deki userı çektik

            if (model.Password != null) //
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password); //Password UI'dan gelenden alınıp DBden gelene eklendi
                await _userManager.UpdateAsync(user);
            }

            if (model.UserName != null)
            {
                var doesUserNameExist = await _userManager.FindByNameAsync(model.UserName);

                if (doesUserNameExist == null) //DB'de böyle bir username yok
                {
                    await _userManager.SetUserNameAsync(user, model.UserName); //yeni username DB usera eklendi
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            if (model.Email != null)
            {
                var isUserEmailExists = await _userManager.FindByNameAsync(model.UserName);

                if (isUserEmailExists == null) //DB'de böyle bir username yok
                {
                    await _userManager.SetUserNameAsync(user, model.Email); //yeni username DBdeki usera eklendi
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }
        }

        public async Task UpdateAppUserEmployee(AppUser model) //Manager'ın employee bilgisini update etmesi
        {
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            AppUser newUser = _mapper.Map<AppUser>(user);

            await _userManager.UpdateAsync(user);
        }

        public async Task<UpdateAppUserEmployeeDTO> GetEmployeeById(string id) //AppUser id Identityden string aldık
        {
            var appUserEmployee = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUser
                {
                    UserName = x.UserName,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    BirthDate = x.BirthDate,
                    CompanyId = x.CompanyId,
                    CreateDate = x.CreateDate,
                    Gender = x.Gender

                },
                where: x => x.Id == id);
            var model = _mapper.Map<UpdateAppUserEmployeeDTO>(appUserEmployee);

            return model;
        }

        public async Task<List<AppUserEmployeeVM>> GetAppUserEmployees(int id)
        {
            var employees = await _appUserRepository.GetFilteredList(
                select: x => new AppUserEmployeeVM
                {
                    Id = x.Id,
                    Username = x.UserName,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    BirthDate = x.BirthDate,
                    CreateDate = x.CreateDate,
                    Gender = x.Gender
                },
                where: x => x.Status != Status.Passive && x.CompanyId == id,
                orderBy: x => x.OrderBy(x => x.Id));

            return employees;
        }

        public async Task<List<AppUserEmployeeVM>> GetAppUserEmployeesTake(int take, string id)
        {
            var appUsers = await _appUserRepository.GetFilteredList(
                select: x => new AppUserEmployeeVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Status = x.Status,
                    CreateDate = x.CreateDate
                },
                where: x => x.Status != Status.Passive && x.CompanyId == Convert.ToInt32(id),
                orderBy: x => x.Take(take).OrderByDescending(x => x.Name));
            return appUsers;
        }

        //GetAppUserManagersTake
        public async Task<List<AppUserManagerVM>> GetAppUserManagersTake(int take)
        {
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            List<AppUserManagerVM> managersVM = new List<AppUserManagerVM>();
            foreach (var item in managers)
            {
                var mappedItem = _mapper.Map<AppUserManagerVM>(item);
                var company = _companyRepository.GetDefault(x => x.Id == item.CompanyId).Result;
                mappedItem.Company = company;
                managersVM.Add(mappedItem);
            }
            return managersVM;

            //var appUsers = await _appUserRepository.GetFilteredList(
            //    select: x => new AppUserManagerVM
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Surname = x.Surname,
            //        Company = x.Company,
            //        CompanyId = x.CompanyId,
            //    },
            //    where: x => x.Status != Status.Passive,
            //    orderBy: x => x.Take(take).OrderByDescending(x => x.Name),
            //    );
            //return appUsers;
        }

        public async Task<UpdateAppUserEmployeeDTO> GetById(string id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdateAppUserEmployeeDTO>(appUser);
            return (model);
        }

        public async Task<UpdatePasswordDTO> GetByIdForPassword(string id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<UpdatePasswordDTO>(appUser);
            return (model);
        }
    }
}
