using LogoFX.UI.ViewModels;
using LogoUI.Samples.Client.Model.Contracts.Compliance;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels
{
    public sealed class ComplianceRecordViewModel : ObjectViewModel<IComplianceRecord>       
    {
        public ComplianceRecordViewModel(IComplianceRecord model)
            : base(model)
        {
            
        }
    }
}