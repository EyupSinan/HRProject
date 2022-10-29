using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class AppUserDTO
    {
        [Required(ErrorMessage = "Must to type First Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must to type Last Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 2")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Must to type Valid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public Gender Gender { get; set; }

        public int CompanyId { get; set; }
    }
}
