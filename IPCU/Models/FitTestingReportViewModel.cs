namespace IPCU.Models
{
    public class FitTestingReportViewModel
    {
        public string HCW_Name { get; set; }
        public string DUO { get; set; }
        public string Professional_Category { get; set; }
        public string Fit_Test_Solution { get; set; }
        public string Respiratory_Type { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public string Test_Results { get; set; }
        public string Name_of_Fit_Tester { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime ExpiringAt { get; set; }
        public bool IsExpired => ExpiringAt < DateTime.Now;


    }

}
