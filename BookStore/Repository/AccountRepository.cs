using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpUserModel.FirstName,
                LastNme = signUpUserModel.LastName,
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email
            };
            return await _userManager.CreateAsync(user, signUpUserModel.Password);
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
        }
        public async Task LogoutAsync()
        {
           await _signInManager.SignOutAsync();
        }
    }
}
