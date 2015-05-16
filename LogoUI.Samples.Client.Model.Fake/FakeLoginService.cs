using System.Security;
using System.Threading;
using System.Threading.Tasks;
using LogoUI.Samples.Client.Model.Contracts;
using LogoUI.Samples.Client.Model.Shared;
using LogoUI.Samples.Client.Model.Shared.UserManagement;

namespace LogoUI.Samples.Client.Model.Fake
{
    public class FakeLoginService : ILoginService
    {        
        public Task<bool> Login(string loginName, string password, bool persist = false)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);

                if (loginName == "e")
                {
                    throw new SecurityException("Wrong user name or password.");
                }

                UserContext.Current = new LocalUser
                {
                    Name = loginName,
                    LoginName = loginName,
                };

                return true;
            });
        }

        public Task<bool> LogOut()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);                
                UserContext.Current = null;
                return true;
            });
        }
    }
}
