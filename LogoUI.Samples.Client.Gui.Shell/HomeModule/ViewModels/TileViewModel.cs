using System;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;

namespace LogoUI.Samples.Client.Gui.Shell.HomeModule.ViewModels
{
    public sealed class TileViewModel : TileViewModelBase
    {
        public TileViewModel(
            string title, 
            INavigationService navigationService, 
            Func<object> loadFunc, 
            NavigationParameter parameter = null)
            : base(title,navigationService,  loadFunc, parameter)
        {
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
            {
                Parent = null;
            }
            base.OnDeactivate(close);
        }
    }
}