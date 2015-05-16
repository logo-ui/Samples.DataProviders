using System;
using System.Collections.Generic;
using LogoUI.Samples.Client.Data.Contracts;

namespace LogoUI.Samples.Client.Data.Providers.Contracts
{
    public interface IComplianceProvider
    {
        IEnumerable<ComplianceRecordDto> GetComplianceRecords(DateTime? startTime, DateTime? endTime);
    }
}
