using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LogoUI.Samples.Client.Data.Contracts;
using LogoUI.Samples.Client.Data.Providers.Contracts;
using LogoUI.Samples.Client.Model.Contracts;
using LogoUI.Samples.Client.Model.Contracts.Compliance;

namespace LogoUI.Samples.Client.Model.Shared
{
    public class DataService : IDataService
    {
        private readonly IComplianceProvider _complianceProvider;

        public DataService(IComplianceProvider complianceProvider)
        {
            _complianceProvider = complianceProvider;
        }

        public Task<IEnumerable<IComplianceRecord>> GetComplianceRecordsAsync(IComplianceRecordsFilter filter)
        {
            return
                Task.Factory.StartNew(
                    () => _complianceProvider.GetComplianceRecords(filter.StartTime, filter.EndTime).Select(
                        ComplianceRecordMapper.ToComplianceRecord));
        }
    }

    class ComplianceRecordMapper
    {
        internal static IComplianceRecord ToComplianceRecord(ComplianceRecordDto complianceRecordDto)
        {
            var addressParts = complianceRecordDto.IpAddress.Split(new[] {'.'}).Select(byte.Parse).ToArray();
            return new ComplianceRecord()
            {
                Host = complianceRecordDto.Host,
                Information = complianceRecordDto.Information,
                IpAddress = new IPAddress(new byte[]
                {
                    addressParts[0], addressParts[1], addressParts[2], addressParts[3]
                }),
                LastDate = complianceRecordDto.LastDate,
                LoggedOnUser = UserContext.Current,
                Object = complianceRecordDto.Object,
                Status =
                    (ComplianceRecordStatus) Enum.Parse(typeof (ComplianceRecordStatus), complianceRecordDto.Status)
            };
        }
    }
}
