using System;
using System.Collections.Generic;

namespace IPCU.Models
{
    // Classes to hold the report data
    public class DeviceMonitoringReportData
    {
        public string Area { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public List<DailyDeviceData> DailyData { get; set; }
    }

    public class DailyDeviceData
    {
        public DateTime Date { get; set; }

        // Device counts
        public int IUCNonKTCount { get; set; }
        public int IUCKTCount { get; set; }
        public int CLNonHDCount { get; set; }
        public int CLHDCount { get; set; }
        public int MVCount { get; set; }

        // Patient movement data
        public int AdmissionCount { get; set; }
        public int TransferInCount { get; set; }
        public int SentHomeCount { get; set; }
        public int MortalityCount { get; set; }
        public int TransferOutCount { get; set; }

        // Calculated properties
        public int TotalArrivals => AdmissionCount + TransferInCount;
        public int TotalDischarges => SentHomeCount + MortalityCount + TransferOutCount;
        public int NetChange => TotalArrivals - TotalDischarges;
    }
}