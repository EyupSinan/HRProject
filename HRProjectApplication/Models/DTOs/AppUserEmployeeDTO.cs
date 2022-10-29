using HRProjectDomain.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HRProjectApplication.Models.DTOs.MinimumAgeAttribute;

namespace HRProjectApplication.Models.DTOs
{
   public class AppUserEmployeeDTO
    { 
        [Required]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(13)]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public string PhoneNumber { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MinimumAgeAttribute(18, ErrorMessage ="Age must be above 18")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime BirthDate { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public Gender Gender { get; set; }
    }
}
