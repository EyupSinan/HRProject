using HRProjectDomain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectDomain.Entities
{
    public class AppUser : IdentityUser, IBaseEntity 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public Gender Gender { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Leave> Leaves { get; set; }
    }
}
