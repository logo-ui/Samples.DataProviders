using System;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Modularity.Contracts;

namespace LogoUI.Samples.Client.Gui.Modularity.ViewModels
{
    public class ModuleViewModel
    {
        public ModuleViewModel(ILogoUiModule model)
        {
            Model = model;
        }

        private ILogoUiModule Model { get; set; }

        public object RootViewModel
        {
            get { return Model.RootViewModel; }
        }

        public Type RootModelType
        {
            get { return Model.RootModelType; }
        }

        public string Name
        {
            get { return Model.Name; }
        }

        public INavigationService NavigationService
        {
            get { return Model.NavigationService; }
        }

        public int Order
        {
            get { return Model.Order; }
        }
    }
}
