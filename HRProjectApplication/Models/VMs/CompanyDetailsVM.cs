using HRProjectDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.VMs
{
    public class CompanyDetailsVM
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string TaxOffice { get; set; }

        public int TaxNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public int NumberOfEmployees { get; set; }

        public string WebSite { get; set; }

        //public List<AppUser> Users { get; set; }
    }
}
