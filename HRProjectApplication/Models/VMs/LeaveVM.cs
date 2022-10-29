using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.VMs
{
    public class LeaveVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public LeaveType LeaveType { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndDate { get; set; }
        public int NumberOfDays => (EndDate - StartDate ).Days;
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        
    }
}
