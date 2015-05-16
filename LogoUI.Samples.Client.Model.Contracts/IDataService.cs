using System.Collections.Generic;
using System.Threading.Tasks;
using LogoUI.Samples.Client.Model.Contracts.Compliance;

namespace LogoUI.Samples.Client.Model.Contracts
{
    public interface IDataService
    {                
        Task<IEnumerable<IComplianceRecord>> GetComplianceRecordsAsync(IComplianceRecordsFilter filter);
    }
}
