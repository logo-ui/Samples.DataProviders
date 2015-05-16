using System.Security;
using LogoUI.Samples.Client.Data.Providers.Contracts;

namespace LogoUI.Samples.Client.Data.Providers.Fake
{
    class FakeLoginProvider : ILoginProvider
    {
        public void Login(string userName, string password)
        {
            if (userName == "e")
            {
                throw new SecurityException("Unauthorized credentials");
            }
        }

        public void Logout(string userName)
        {
            
        }
    }
}
