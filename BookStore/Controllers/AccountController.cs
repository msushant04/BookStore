using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }
        [Route("signup"), HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(signUpUserModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(signUpUserModel);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = signUpUserModel.Email });
            }
            return View(signUpUserModel);
        }
        [Route("signin")]
        public IActionResult SignIn()
        {
            return View();
        }
        [Route("signin"), HttpPost]
        public async Task<IActionResult> SignIn(SignInModel signUpUserModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signUpUserModel);
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not Allowed To Login");
                }
                else if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View();
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
          await  _accountRepository.LogoutAsync();
          return  RedirectToAction("SignIn");
        }
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
        [Route("change-password"),HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
               var result =  await _accountRepository.ChangePasswordAsync(changePasswordModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordModel);
                }
                ViewBag.isChangePasswordSuccess = true;
                ModelState.Clear();
            }
            return View(changePasswordModel);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token,string email)
        {
            EmailConfirmModel emailConfirmModel = new EmailConfirmModel()
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(uid))
            {
              token = token.Replace(' ', '+');
              var result =  await _accountRepository.ConfirmEmail(uid, token);
                if (result.Succeeded)
                {
                    emailConfirmModel.IsEmailVerified = true;
                }
                else
                {
                    emailConfirmModel.IsEmailVerified = false;
                }
            }
            return View(emailConfirmModel);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.IsEmailConfirmed = true;
                    return View(model); 
                }
                await _accountRepository.GenerateTokenAsync(user);
                model.IsEmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("","Something went wrong.");
            }

            return View(model);
        }
    }
}
