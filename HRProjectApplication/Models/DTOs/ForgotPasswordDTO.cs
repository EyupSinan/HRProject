using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
   public class ForgotPasswordDTO
    {
        [Required, EmailAddress, Display(Name = "Registered Email Address")]
        public int Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
