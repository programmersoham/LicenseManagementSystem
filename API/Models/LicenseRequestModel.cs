// No need to add in database. This is just a model to get the data from the user and pass it to the controller.
namespace API.Models
{
    public class LicenseRequestModel
    {
        public string UserName { get; set; }


        public string ApplicationName { get; set; }


        public int TotalLicense { get; set; }


        public string LicensePrefix { get; set; }


        public string LicenseStartValue { get; set; }
        public string CreatedBy { get; set; }
    }
}
