using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class UpdateAppUserDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Must to type First Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must to type First Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must to type Last Name")]
        [MinLength(3, ErrorMessage = "Minimun lenght is 3")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Must type Valid Email")]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime UpdateDate => DateTime.Now;

        public Status Status => Status.Modified;

        public Gender Gender { get; set; }
    }
}
