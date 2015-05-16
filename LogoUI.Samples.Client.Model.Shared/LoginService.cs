using System;
using System.Threading.Tasks;
using LogoUI.Samples.Client.Data.Providers.Contracts;
using LogoUI.Samples.Client.Model.Contracts;
using LogoUI.Samples.Client.Model.Shared.UserManagement;

namespace LogoUI.Samples.Client.Model.Shared
{
    public class LoginService : ILoginService
    {
        private readonly ILoginProvider _loginProvider;

        public LoginService(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }

        public Task<bool> Login(string loginName, string password, bool persist = false)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _loginProvider.Login(loginName, password);
                    UserContext.Current = new LocalUser
                    {
                        Name = loginName,
                        LoginName = loginName
                    };
                    return true;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            });
        }

        public Task<bool> LogOut()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _loginProvider.Logout(UserContext.Current.LoginName);
                    UserContext.Current = null;
                    return true;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            });
        }
    }
}
