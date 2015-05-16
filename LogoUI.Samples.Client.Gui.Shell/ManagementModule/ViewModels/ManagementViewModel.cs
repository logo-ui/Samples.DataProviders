using Caliburn.Micro;
using LogoFX.Practices.IoC;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;

namespace LogoUI.Samples.Client.Gui.Shell.ManagementModule.ViewModels
{
    [Singleton]
    [NavigationViewModel(ConductorType = typeof(IMainViewModel), IsSingleton = true)]
    public sealed class ManagementViewModel : Screen, INavigationViewModel
    {
        public void OnNavigated(NavigationDirection direction, object argument)
        {

        }
    }
}