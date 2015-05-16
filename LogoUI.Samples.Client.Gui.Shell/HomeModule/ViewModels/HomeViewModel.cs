using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using LogoFX.Practices.IoC;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Modularity.Contracts;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;

namespace LogoUI.Samples.Client.Gui.Shell.HomeModule.ViewModels
{    
    [Singleton]
    [NavigationViewModel(ConductorType = typeof(IMainViewModel), IsSingleton = true)]
    public sealed class HomeViewModel : Conductor<IScreen>.Collection.AllActive, INavigationViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly List<ILogoUiModule> _modules;

        public HomeViewModel(
            INavigationService navigationService,
            IEnumerable<ILogoUiModule> modules)
        {
            _navigationService = navigationService;

            _modules = new List<ILogoUiModule>(modules.Where(t => t.IsGroup == false).OrderBy(t => t.Order));            
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var module in _modules.OrderBy(x => x.Order))
            {
                var module1 = module;
                var tile = new TileViewModel(
                    module.Name,
                    _navigationService,
                    () => module1.RootViewModel,
                    new TypeNavigationParameter(module.RootModelType, _navigationService));
                Items.Add(tile);                
            }

            while (Items.Count < 5)
            {
                var tile = new TileViewModel("Unknown",_navigationService, () => null, 
                    _navigationService.CreateParameter<HomeViewModel>());
                Items.Add(tile);
            }
            foreach (var item in Items)
            {
                ActivateItem(item);
            }
        }

        public void OnNavigated(NavigationDirection direction, object argument)
        {

        }        
    }
}