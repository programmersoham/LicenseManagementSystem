namespace BlazorApp.Models
{
    public class FetchLicense
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ApplicationName { get; set; }
        public string LicenseKey { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
