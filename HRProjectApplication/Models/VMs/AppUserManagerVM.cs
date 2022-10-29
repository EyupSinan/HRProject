using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.VMs
{
    public class AppUserManagerVM
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName => Name + " " + Surname;

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
