using AutoMapper;
using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Models.VMs;
using HRProjectApplication.Services.LeaveService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRProjectUI.Controllers
{
    public class LeaveController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILeaveService _leaveService;
        private readonly IMapper _mapper;

        public LeaveController(UserManager<AppUser> userManager, ILeaveService leaveService, IMapper mapper)
        {
            _userManager = userManager;
            _leaveService = leaveService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<LeaveVM> model = null;
            if (User.IsInRole("Manager"))
            {
                model = await _leaveService.GetLeaves();
            }
            else if (User.IsInRole("Employee"))
            {
                var user = await _userManager.GetUserAsync(User);
                model = await _leaveService.GetLeavesById(user.Id);
            }
            return View(model);
        }
        public async Task<IActionResult> Pending()
        {
            var model = await _leaveService.GetPendingLeaves();
            return View(model);
        }
        public async Task<IActionResult> Approved()
        {
            var model = await _leaveService.GetApprovedLeaves();
            return View(model);
        }
        public async Task<IActionResult> Denied()
        {
            var model = await _leaveService.GetDeniedLeaves();
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            CreateLeaveDTO model = new CreateLeaveDTO();
            AppUser appUser = await _userManager.GetUserAsync(User);
            model.User = appUser;
            model.UserId = appUser.Id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveDTO model)
        {
            if (ModelState.IsValid)
            {

                AppUser appUser = await _userManager.GetUserAsync(User);
                model.User = appUser;
                model.UserId = appUser.Id;
                await _leaveService.Create(model);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ApproveRequest(int id)
        {
            await _leaveService.ApproveLeave(id);
            return RedirectToAction("Approved");
        }
        public async Task<IActionResult> DenyRequest(int id)
        {
            await _leaveService.DenyLeave(id);
            return RedirectToAction("Denied");
        }
        public async Task<IActionResult> Details(int id)
        {
            var leave = await _leaveService.GetById(id);
            var model = _mapper.Map<LeaveVM>(leave);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leave = await _leaveService.GetById(id);
            leave.User = await _userManager.FindByIdAsync(leave.UserId);
            var leaveVM = _mapper.Map<LeaveVM>(leave);

            return View(leaveVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LeaveVM model)
        {
            if (ModelState.IsValid)
            {
                await _leaveService.Update(model);
                TempData["Success"] = "Leave has been updated!";
                return RedirectToAction("Index", "Leave");
            }
            else
            {
                TempData["Success"] = "Leave has not been updated!";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _leaveService.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
