using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.DashboardService
{
    public interface IDashboardService
    {
        Task<DashboardVM> GetDashboardManager(string id);

        Task<DashboardVM> GetDashboardAdmin();
    }
}
