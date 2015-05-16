using System.Collections;
using System.Threading.Tasks;
using Caliburn.Micro;
using LogoFX.UI.ViewModels;
using LogoUI.Samples.Client.Model.Contracts;
using LogoUI.Samples.Client.Model.Contracts.Compliance;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels
{
    public sealed class ComplianceListViewModel : Screen
    {
        private readonly IDataService _dataService;        

        public ComplianceListViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _items = new WrappingCollection
            {
                FactoryMethod = o => new ComplianceRecordViewModel((IComplianceRecord) o)
            };
        }

        private readonly WrappingCollection _items;
        public IEnumerable Items
        {
            get { return _items; }
        }

        public async Task Update(IComplianceRecordsFilter filter)
        {
            _items.ClearSources();
            var dataItems = await _dataService.GetComplianceRecordsAsync(filter);
            _items.AddSource(dataItems);
        }
    }
}