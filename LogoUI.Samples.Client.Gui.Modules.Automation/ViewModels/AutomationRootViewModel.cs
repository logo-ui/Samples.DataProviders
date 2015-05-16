using Caliburn.Micro;
using LogoFX.Practices.IoC;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;

namespace LogoUI.Samples.Client.Gui.Modules.Automation.ViewModels
{
    [Singleton]
    [NavigationViewModel(ConductorType = typeof(IMainViewModel), IsSingleton = true)]
    public sealed class AutomationRootViewModel : Screen, INavigationViewModel
    {
        public void OnNavigated(NavigationDirection direction, object argument)
        {
            
        }
    }
}