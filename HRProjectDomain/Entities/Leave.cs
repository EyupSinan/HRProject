using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectDomain.Entities
{
    public class Leave : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public LeaveType LeaveType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
        public RequestStatus RequestStatus { get; set; }

        
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
