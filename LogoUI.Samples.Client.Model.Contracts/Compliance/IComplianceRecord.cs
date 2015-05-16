using System;
using System.ComponentModel;
using System.Net;
using LogoUI.Samples.Client.Model.Contracts.UserManagement;

namespace LogoUI.Samples.Client.Model.Contracts.Compliance
{
    public interface IComplianceRecord
    {
        DateTime LastDate { get; }

        string Host { get; }

        IPAddress IpAddress { get; }

        string Object { get; }

        IUser LoggedOnUser { get; }

        ComplianceRecordStatus Status { get; }

        string Information { get; }
    }

    public enum ComplianceRecordStatus
    {
        [Description("Not Installed")]
        NotInstalled,
        [Description("Installed")]
        Installed,
    }
}
