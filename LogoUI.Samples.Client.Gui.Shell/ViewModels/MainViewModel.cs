using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using LogoFX.Practices.IoC;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Modularity.Contracts;
using LogoUI.Samples.Client.Gui.Modularity.ViewModels;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;

namespace LogoUI.Samples.Client.Gui.Shell.ViewModels
{
    [Singleton]
    [NavigationViewModel(ConductorType = typeof(ShellViewModel), IsSingleton = true)]
    [NavigationSynonym(typeof(IMainViewModel))]
    public sealed class MainViewModel : Conductor<IScreen>, INavigationViewModel, IMainViewModel
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(
            INavigationService navigationService,
            IEnumerable<ILogoUiModule> modules)
        {
            _navigationService = navigationService;

            _modules.AddRange(modules.Select(t => new ModuleViewModel(t)));

            var collectionView = CollectionViewSource.GetDefaultView(Modules);
            collectionView.SortDescriptions.Add(new SortDescription("Order", ListSortDirection.Ascending));
        }

        private readonly List<ModuleViewModel> _modules = new List<ModuleViewModel>();
        public IEnumerable Modules
        {
            get { return _modules; }
        }        

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            NavigationHelper.RegisterNavigationCommands(typeof(MainViewModel), view, _navigationService);
        }

        void INavigationConductor.NavigateTo(object viewModel, object argument)
        {
            ActivateItemImpl(viewModel);
        }

        void INavigationViewModel.OnNavigated(NavigationDirection direction, object argument)
        {
            ActivateItemImpl(Modules.OfType<ModuleViewModel>().First(t => t.Name == "Home").RootViewModel);
        }

        private void ActivateItemImpl(object viewModel)
        {
            ActivateItem((IScreen)viewModel);
        }
    }
}