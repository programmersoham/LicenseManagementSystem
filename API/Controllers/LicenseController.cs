using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public LicenseController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddLicense([FromBody] LicenseRequestModel request)
        {
            // Validate the request model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            


            List<string> generatedLicenses = GenerateLicenses(request.TotalLicense, request.LicensePrefix, request.LicenseStartValue,request.UserName,request.ApplicationName,request.CreatedBy);
            // Save license to database using bulkinsert


            var licenseEntities = generatedLicenses.Select(licenseKey => new LicenseModel
            {
                UserName = request.UserName,
                ApplicationName = request.ApplicationName,
                LicenseKey = licenseKey,
                IsActive = true, 
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy
            }).ToList();

            _context.BulkInsert(licenseEntities);

            return Ok(new { Message = " Saved to Database." });
            
            

        }
        [HttpGet]
        public IActionResult GetAllLicense()
        {
            try
            {
                var alllicense = _context.Licenses.ToList();
                return Ok(alllicense);
            }
            catch (Exception ex) { return StatusCode(500,new { Message = $"Internal Server Error: {ex.Message}" });}
        }
        
        [HttpPut("update-license/{licenseKey}")]
        public IActionResult UpdateLicenseState(string licenseKey, [FromBody] bool isActive)
        {
            try
            {
                // Find the license by key
                var license = _context.Licenses.SingleOrDefault(l => l.LicenseKey == licenseKey);

                if (license == null)
                {
                    return NotFound(new { Message = "License not found." });
                }

                // Update the IsActive state
                license.IsActive = !license.IsActive;


                // Save changes to the database
                _context.SaveChanges();

                return Ok(new { Message = "License state updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Internal Server Error: {ex.Message}" });
            }
        }



        [ApiExplorerSettings(IgnoreApi = true)] // Exclude from Swagger
        public List<string> GenerateLicenses( int total, string licensePrefix, string licenseStartValue, string UserName ,string ApplicationName, string createdBy)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            List<string> generatedLicenses = new List<string>();

            for (int i = 0; i < total; i++)
            {
                string randomString = new string(Enumerable.Repeat(chars, 8)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                string license = $"{licensePrefix.ToUpper()}{licenseStartValue}{randomString}";
                
                Console.WriteLine(license); 
                generatedLicenses.Add(license);
                 
                var licenseEntity = new LicenseModel
                {
                    UserName = UserName,
                    ApplicationName = ApplicationName,
                    LicenseKey = license,
                    IsActive = true, 
                    CreatedAt = DateTime.UtcNow,
                     CreatedBy = createdBy
                };
                licenseEntity.CreatedAt = licenseEntity.CreatedDateOnly;


            }
            return generatedLicenses;
        }
    }

    

}