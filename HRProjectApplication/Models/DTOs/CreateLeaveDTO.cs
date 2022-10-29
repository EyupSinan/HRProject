using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Models.DTOs
{
    public class CreateLeaveDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsApproved => false;
        public LeaveType LeaveType { get; set; }
        public int NumberOfDays => (EndDate - StartDate ).Days;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public RequestStatus RequestStatus => RequestStatus.Pending;
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
