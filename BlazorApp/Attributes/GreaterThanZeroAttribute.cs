using System.ComponentModel.DataAnnotations;
namespace BlazorApp.Attributes;
public class GreaterThanZeroAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int intValue)
        {
            if (intValue > 0)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("The field must be a number greater than 0.");
    }
}
