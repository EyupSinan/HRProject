using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.AppUserService
{
    public interface IAppUserService
    {
        Task<IdentityResult> Register(RegisterDTO model);
        Task<SignInResult> Login(LoginDTO model);
        Task LogOut();
        Task Create(AppUserEmployeeDTO model);
        Task<UpdateProfileDTO> GetByUserName(string username);
        Task<AppUserVM> GetUserByUserName(string username);
        Task<List<AppUserEmployeeVM>> GetAppUserEmployees(int id);
        Task UpdateUser(UpdateProfileDTO model);
        Task<UpdateAppUserEmployeeDTO> GetEmployeeById(string id);
        Task Delete(string id);
        Task UpdateAppUserEmployee(AppUser model);
        Task<List<AppUserEmployeeVM>> GetAppUserEmployeesTake(int take, string id);
        Task<List<AppUserManagerVM>> GetAppUserManagersTake(int take);
        Task<UpdateAppUserEmployeeDTO> GetById(string id);
        Task<UpdatePasswordDTO> GetByIdForPassword(string id);      
    }
}
