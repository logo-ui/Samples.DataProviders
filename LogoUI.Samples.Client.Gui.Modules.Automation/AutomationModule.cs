using System.ComponentModel.Composition;
using LogoUI.Samples.Client.Gui.Modularity;
using LogoUI.Samples.Client.Gui.Modules.Automation.ViewModels;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Gui.Modules.Automation
{
    [Export(typeof(ICompositionModule))]
    public sealed class AutomationModule : LogoUiModule<AutomationRootViewModel>
    {
        protected override string GetName()
        {
            return "Automation";
        }

        protected override int GetOrder()
        {
            return 2;
        }
    }
}