namespace API.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        public required string ApplicationName { get; set; }
        public required string Description { get; set; }
    }
}
