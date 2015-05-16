using System.Linq;
using Caliburn.Micro;
using LogoUI.Samples.Client.Gui.Modules.Compliance.Models;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels
{
    public sealed class ComplianceRecordsFiltersViewModel :
        Conductor<ComplianceRecordsFilterViewModel>.Collection.OneActive        
    {
        protected override void OnActivate()
        {
            base.OnActivate();

            ActivateItem(new ComplianceRecordsFilterViewModel("Default", new ComplianceRecordsFilter())
            {
                EnabledDateFiltering = true,
                LastDays = true,
            });

            ActivateItem(new ComplianceRecordsFilterViewModel("Not default", new ComplianceRecordsFilter())
            {
                EnabledDateFiltering = false,
                DateRange = true,
            });

            ActivateItem(Items.First());
        }        
    }
}