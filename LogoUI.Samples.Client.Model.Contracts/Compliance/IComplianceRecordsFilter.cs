using System;

namespace LogoUI.Samples.Client.Model.Contracts.Compliance
{
    public interface IComplianceRecordsFilter
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
    }
}