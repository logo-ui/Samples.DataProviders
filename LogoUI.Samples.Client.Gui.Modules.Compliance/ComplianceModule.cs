using System.ComponentModel.Composition;
using LogoUI.Samples.Client.Gui.Modularity;
using LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance
{
    [Export(typeof(ICompositionModule))]
    public sealed class ComplianceModule : LogoUiModule<ComplianceRootViewModel>
    {
        protected override string GetName()
        {
            return "Compliance";
        }

        protected override int GetOrder()
        {
            return 1;
        }
    }
}