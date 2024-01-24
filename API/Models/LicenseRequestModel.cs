namespace API.Models
{
    public class LicenseRequestModel
    {
        public string UserName { get; set; }


        public string ApplicationName { get; set; }


        public int TotalLicense { get; set; }


        public string LicensePrefix { get; set; }


        public string LicenseStartValue { get; set; }
    }
}
