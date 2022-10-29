using HRProjectApplication.Models.DTOs;
using HRProjectApplication.Services.AppUserService;
using HRProjectDomain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using HRProjectApplication.Models.VMs;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using HRProjectApplication.Services.EmailService;

namespace HRProjectUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAppUserService appUserService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _appUserService = appUserService;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null )//&& await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    // Log the password reset link
                    //_logger.Log(LogLevel.Warning, passwordResetLink);

                    //Send email to user
                    _emailService.SendPasswordResetEmail(user, passwordResetLink);

                    // Send the user to Forgot Password Confirmation view
                    return View("ForgotPasswordConfirmation");
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist or is not confirmed
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            // If password reset token or email is null, most likely the
            // user tried to tamper the password reset link
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // AppUserService.Create(registerDTO)            
            var result = await _appUserService.Register(registerDTO);
            if (result.Succeeded)
            {
               
                return RedirectToAction("Index", "Home");
            }
            //error
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
                TempData["Error"] = "Something went wrong";
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded) //SignInResult.Succeeded
                {
                    var loggedUser = await _userManager.FindByNameAsync(model.UserName);

                    var roles = await _userManager.GetRolesAsync(loggedUser);                 

                    //logged user cookiye atılcak, o da olmazsa session araştırıp deneriz

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }
    }
}
