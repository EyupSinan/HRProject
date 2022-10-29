using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.VMs
{
    public class AppUserVM
    {        
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string FullName => Name + " " + SurName;
        
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
