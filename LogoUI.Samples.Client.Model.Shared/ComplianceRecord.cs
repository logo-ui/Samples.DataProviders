using System;
using System.Net;
using LogoUI.Samples.Client.Model.Contracts.Compliance;
using LogoUI.Samples.Client.Model.Contracts.UserManagement;

namespace LogoUI.Samples.Client.Model.Shared
{
    sealed class ComplianceRecord : IComplianceRecord
    {
        public DateTime LastDate { get; set; }
        public string Host { get; set; }
        public IPAddress IpAddress { get; set; }
        public string Object { get; set; }
        public IUser LoggedOnUser { get; set; }
        public ComplianceRecordStatus Status { get; set; }
        public string Information { get; set; }
    }
}