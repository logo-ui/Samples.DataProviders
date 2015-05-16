namespace LogoUI.Samples.Client.Data.Providers.Contracts
{
    public interface ILoginProvider
    {
        void Login(string userName, string password);
        void Logout(string userName);
    }
}
