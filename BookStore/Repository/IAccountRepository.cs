using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task LogoutAsync();
    }
}