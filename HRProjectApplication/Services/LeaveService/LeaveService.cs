using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectDomain.Entities;
using HRProjectDomain.Enums;
using HRProjectDomain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProjectApplication.Services.LeaveService
{
    public class LeaveService : ILeaveService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAppUserRepository _appUserRepository;
        

        public LeaveService(IMapper mapper, UserManager<AppUser> userManager, ILeaveRepository leaveRepository, IAppUserRepository appUserRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _leaveRepository = leaveRepository;
            _appUserRepository = appUserRepository;
        }
        public async Task Create(CreateLeaveDTO model)
        {           
            var leave = _mapper.Map<Leave>(model);
            await _leaveRepository.Create(leave);
        }
        public async Task<Leave> GetById(int id)
        {
            var leave = await _leaveRepository.GetDefault(x=>x.Id ==id);
            return leave;
        }
        public async Task<List<LeaveVM>> GetLeavesById(string id)
        {
            var leaves = await _leaveRepository.GetFilteredList(
                select: x => new LeaveVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    IsApproved = x.IsApproved,
                    LeaveType = x.LeaveType,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    RequestStatus = x.RequestStatus,
                    UserId = id,
                    User = _userManager.FindByIdAsync(id).Result,
                    Status = x.Status,
                    CreateDate = x.CreateDate
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderByDescending(x => x.CreateDate));
            return leaves;
        }
        public async Task Update(LeaveVM model)
        {
            var leave = _mapper.Map<Leave>(model);
            await _leaveRepository.Update(leave);
        }
        public async Task ApproveLeave(int id)
        {
            var leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.RequestStatus = RequestStatus.Approved;
            await _leaveRepository.Update(leave);
        }
        public async Task DenyLeave(int id)
        {
            var leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.RequestStatus = RequestStatus.Denied;
            await _leaveRepository.Update(leave);
        }
        public async Task<List<LeaveVM>> GetLeaves()
        {
            var leaves = await _leaveRepository.GetFilteredList(
                select: x => new LeaveVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreateDate = x.CreateDate,
                    IsApproved = x.IsApproved,
                    LeaveType = x.LeaveType,
                    Status = x.Status,
                    User = x.User,
                    UserId = x.User.Id,
                    RequestStatus = x.RequestStatus
                },
                include: x => x.Include(x => x.User),
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.RequestStatus));


            return leaves;
        }
        public async Task<List<LeaveVM>> GetLeaves(RequestStatus requestStatus)
        {

            var leaves = await _leaveRepository.GetFilteredList(
                select: x => new LeaveVM
                {

                    Id = x.Id,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    CreateDate = x.CreateDate,
                    IsApproved = x.IsApproved,
                    LeaveType = x.LeaveType,
                    Status = x.Status,
                    User = x.User,
                    UserId = x.User.Id,
                    RequestStatus = x.RequestStatus
                },
                include: x => x.Include(x => x.User),
                where: x => x.Status != Status.Passive && x.RequestStatus == requestStatus,
                orderBy: x => x.OrderByDescending(x => x.CreateDate));

            return leaves;
        }
        public async Task<List<LeaveVM>> GetPendingLeaves()
        {
            return await GetLeaves(RequestStatus.Pending);            
        }
        public async Task<List<LeaveVM>> GetApprovedLeaves()
        {
            return await GetLeaves(RequestStatus.Approved);
        }
        public async Task<List<LeaveVM>> GetDeniedLeaves()
        {
            return await GetLeaves(RequestStatus.Denied);
        }
        public async Task Delete(int id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.Status = Status.Passive;
            leave.DeleteDate = DateTime.Now;
            await _leaveRepository.Delete(leave);

        }
    }
}
