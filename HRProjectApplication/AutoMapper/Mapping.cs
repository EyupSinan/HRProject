using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.AutoMapper
{
    //DTO - VM ile Entity arasındaki bağlantıları yapan kısımdır.
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDTO>().ReverseMap();
            CreateMap<AppUser, LoginDTO>().ReverseMap();

            CreateMap<AppUser, AppUserVM>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserDTO>().ReverseMap();
            CreateMap<AppUser, AppUserManagerVM>().ReverseMap();

            CreateMap<AppUser, AppUserEmployeeVM>().ReverseMap();
            CreateMap<AppUser, AppUserEmployeeDTO>().ReverseMap();
            CreateMap<AppUser, UpdateAppUserEmployeeDTO>().ReverseMap();
            CreateMap<AppUser, UpdatePasswordDTO>().ReverseMap();
            CreateMap<AppUser, ForgotPasswordDTO>().ReverseMap();

            CreateMap<AppUser, DashboardVM>().ReverseMap();
            
            CreateMap<Company, CompanyVM>().ReverseMap();
            CreateMap<Company, CompanyDetailsVM>().ReverseMap();
            CreateMap<Company, UpdateCompanyDTO>().ReverseMap();
            CreateMap<Company, CreateCompanyDTO>().ReverseMap();

            CreateMap<Leave, CreateLeaveDTO>().ReverseMap();
            CreateMap<Leave, LeaveVM>().ReverseMap();

        }
    }
}
    