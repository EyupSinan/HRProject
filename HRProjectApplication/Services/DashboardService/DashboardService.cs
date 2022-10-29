using AutoMapper;
using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.AppUserService;
using HRProjectApplication.Services.CompanyService;
using HRProjectDomain.Entities;
using HRProjectDomain.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.DashboardService
{
    public class DashboardService : IDashboardService
    {
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public DashboardService(IAppUserService appUserService, IMapper mapper, ICompanyService companyService)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _companyService = companyService;
        }

        public async Task<DashboardVM> GetDashboardManager(string id)
        {
            var dashboardVM = new DashboardVM()
            {
                Employees = await _appUserService.GetAppUserEmployeesTake(5, id)
            };
            return dashboardVM;
        }

        public async Task<DashboardVM> GetDashboardAdmin()
        {
            var dashboardVM = new DashboardVM()
            {
                Companies = await _companyService.GetCompanies(),
                Managers = await _appUserService.GetAppUserManagersTake(5)
            };
            return dashboardVM;
        }
    }
}



