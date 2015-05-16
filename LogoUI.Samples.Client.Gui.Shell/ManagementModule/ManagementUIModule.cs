using System.ComponentModel.Composition;
using LogoUI.Samples.Client.Gui.Modularity;
using LogoUI.Samples.Client.Gui.Shell.ManagementModule.ViewModels;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Gui.Shell.ManagementModule
{
    [Export(typeof(ICompositionModule))]
    public sealed class ManagementUiModule : LogoUiModule<ManagementViewModel>
    {
        protected override string GetName()
        {
            return "Management";
        }

        protected override int GetOrder()
        {
            return 0;
        }

        protected override bool GetIsGroup()
        {
            return false;
        }
    }
}