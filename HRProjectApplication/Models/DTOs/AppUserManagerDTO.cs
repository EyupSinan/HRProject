using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class AppUserManagerDTO
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }

        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public Gender Gender { get; set; }

        public List<CompanyVM> Companies { get; set; }
    }
}
