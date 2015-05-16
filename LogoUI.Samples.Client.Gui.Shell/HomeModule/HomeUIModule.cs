using System.ComponentModel.Composition;
using LogoUI.Samples.Client.Gui.Modularity;
using LogoUI.Samples.Client.Gui.Shell.HomeModule.ViewModels;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Gui.Shell.HomeModule
{
    [Export(typeof(ICompositionModule))]
    public sealed class HomeUiModule : LogoUiModule<HomeViewModel>
    {
        protected override string GetName()
        {
            return "Home";
        }

        protected override int GetOrder()
        {
            return -1;
        }

        protected override bool GetIsGroup()
        {
            return true;
        }
    }
}