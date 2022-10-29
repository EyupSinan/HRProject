using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.LeaveService
{
    public interface ILeaveService
    {
        Task Create(CreateLeaveDTO model);
        Task<Leave> GetById(int id);
        Task ApproveLeave(int id);
        Task DenyLeave(int id);
        Task<List<LeaveVM>> GetLeaves();
        Task<List<LeaveVM>> GetLeavesById(string id);
        Task<List<LeaveVM>> GetLeaves(RequestStatus requestStatus);
        Task<List<LeaveVM>> GetPendingLeaves();
        Task<List<LeaveVM>> GetDeniedLeaves();
        Task<List<LeaveVM>> GetApprovedLeaves();
        Task Delete(int id);
        Task Update(LeaveVM model);
    }
}
