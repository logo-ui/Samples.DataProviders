using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using LogoFX.UI.Bootstrapping.SimpleContainer;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Modularity.Contracts;
using LogoUI.Samples.Client.Gui.Shared.UiServices;
using LogoUI.Samples.Client.Gui.Shell.ViewModels;
using LogoUI.Samples.Client.Model.Contracts;
using LogoUI.Samples.Client.Model.Shared;

namespace LogoUI.Samples.Client.Gui.Shell
{
	public sealed class AppBootstrapper : NavigationBootstrapper<ShellViewModel,ExtendedSimpleIocContainer>
	{
        protected override void OnConfigure(ExtendedSimpleIocContainer container)
        {
            base.OnConfigure(container);

            container.RegisterHandler(
                typeof(IEnumerable<ILogoUiModule>),
                null,
                c =>
                    new List<ILogoUiModule>(
                        container.GetAllInstances(typeof(ILogoUiModule)).Cast<ILogoUiModule>()));
            
            container.RegisterSingleton<IShellCloseService, ShellViewModel>();
            container.RegisterSingleton<IWindowManager, ShellViewModel>("LikeRT");
            container.RegisterSingleton<ILoginService, LoginService>();
            container.RegisterSingleton<IDataService, DataService>();
        }

        protected override void OnPrepareNavigation(INavigationService navigationService)
        {
            base.OnPrepareNavigation(navigationService);
            foreach (var module in Modules.OfType<ILogoUiModule>())
            {
                module.NavigationService = navigationService;
            }
        }    
	}
}