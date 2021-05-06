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
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View();
        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
          await  _accountRepository.LogoutAsync();
          return  RedirectToAction("SignIn");
        }
    }
}
