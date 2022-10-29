using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Must to type UserName")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Must to type Password")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must to type ConfirmPassword")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must to type Email")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime CreateDate => DateTime.Now;

        public Status Status => Status.Active;

        public Gender Gender { get; set; }
    }
}

