using System;
using LogoUI.Samples.Client.Model.Contracts.Compliance;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.Models
{
    public sealed class ComplianceRecordsFilter : IComplianceRecordsFilter
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}