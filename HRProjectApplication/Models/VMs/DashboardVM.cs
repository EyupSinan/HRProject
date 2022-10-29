using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.VMs
{
   public class DashboardVM
    {
        public List<AppUserEmployeeVM> Employees { get; set; }

        public List<AppUserManagerVM> Managers { get; set; }

        public List<CompanyVM> Companies { get; set; }
    }
}
