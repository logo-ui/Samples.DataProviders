using System;
using System.Collections.Generic;
using System.Threading;
using LogoUI.Samples.Client.Data.Contracts;
using LogoUI.Samples.Client.Data.Providers.Contracts;

namespace LogoUI.Samples.Client.Data.Providers.Fake
{
    class FakeComplianceProvider : IComplianceProvider
    {
        private const int ComplianceRecordCount = 100;

        private static readonly string[] AppNames =
        {
            "Security Update for Windows",
            "Skype",
            "TeamViewer",
            "USB Removable Storage",
            "WebEx",
            "Windows Live Messenger",
            "Windows Messenger",
            "WinPcap",
            "WinSCP",
            "Wireshark",
            "Google Talk",
        };

        public IEnumerable<ComplianceRecordDto> GetComplianceRecords(DateTime? startTime, DateTime? endTime)
        {
            Random rnd = new Random();
            var result = new List<ComplianceRecordDto>();

            for (int i = 0; i < ComplianceRecordCount; ++i)
            {
                result.Add(GenerateComplianceRecordDto(rnd, i));
                Thread.Sleep(5);
            }

            return result;
        }

        private ComplianceRecordDto GenerateComplianceRecordDto(Random rnd, int index)
        {
            byte hostIndex = (byte)rnd.Next(1, 4);

            var result = new ComplianceRecordDto
            {
                LastDate = new DateTime(2012, 1, 1) + new TimeSpan(rnd.Next(0, 100000000) * 1000),
                Host = "HOST" + hostIndex,
                IpAddress = "192.168.0." + hostIndex,
                Object = AppNames[rnd.Next(AppNames.Length)],                
                Status = rnd.Next(2) == 0 ? "Installed" : "NotInstalled",
                Information = "Record N " + (index + 1)
            };

            return result;
        }
    }
}
