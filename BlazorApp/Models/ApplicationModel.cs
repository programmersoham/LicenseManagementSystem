using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Application Name is required.")]
        public  string ApplicationName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public  string Description { get; set; }
    }
}
