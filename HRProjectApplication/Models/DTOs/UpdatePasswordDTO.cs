using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
   public class UpdatePasswordDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Must to type Password")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Must to type Password")]
        [MinLength(3, ErrorMessage = "Minimum length is 3")]
        [Compare("Password", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set; }
    }
}
