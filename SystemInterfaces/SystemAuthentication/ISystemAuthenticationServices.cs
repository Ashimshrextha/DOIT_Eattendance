using System;
using System.Threading.Tasks;
using static SystemStores.ENUMData.EnumGlobal;

namespace SystemInterfaces.SystemAuthentication
{
    public interface ISystemAuthenticationServices
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password);

        Task<SignInStatus> ChangePasswordAsync(string userName, string passwordMD5, string passwordSHA);

        Task<SignInStatus> RegisterUserAsync(string userName, string password, string email, string mobile);
    }
}
