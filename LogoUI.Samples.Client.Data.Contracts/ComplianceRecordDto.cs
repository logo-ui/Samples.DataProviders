using System;

namespace LogoUI.Samples.Client.Data.Contracts
{
    public class ComplianceRecordDto
    {
        public DateTime LastDate { get; set; }

        public string Host { get; set; }

        public string IpAddress { get; set; }

        public string Object { get; set; }

        public string Status { get; set; }

        public string Information { get; set; }
    }
}
