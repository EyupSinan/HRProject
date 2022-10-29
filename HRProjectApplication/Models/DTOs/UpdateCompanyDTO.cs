using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class UpdateCompanyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must to type First Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string CompanyName { get; set; }

        public string Address { get; set; }

        public int NumberOfEmployees { get; set; }

        public string TaxOffice { get; set; }

        public int TaxNumber { get; set; }

        [PhoneAttribute]
        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public string WebSite { get; set; }

        public DateTime? UpdateDate = DateTime.Now;
        public DateTime? CreateDate { get; set; }

        public Status Status { get; set; }

        //Nav.Property
        public List<AppUser> Users { get; set; }
    }
}
