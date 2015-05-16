using System;
using LogoFX.UI.Navigation;

namespace LogoUI.Samples.Client.Gui.Shared.ViewModels
{
    public sealed class TypeNavigationParameter : NavigationParameter
    {
        private readonly Type _type;
        private readonly INavigationService _navigationService;
        private readonly object _argument;

        public TypeNavigationParameter(Type type, INavigationService navigationService, object argument = null)
        {
            _type = type;
            _navigationService = navigationService;
            _argument = argument;
        }

        public override void Navigate()
        {
            _navigationService.Navigate(_type, _argument);
        }
    }
}