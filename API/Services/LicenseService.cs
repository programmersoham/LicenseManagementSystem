
using API.Data;
using API.Models;
using System.Data;
using System.Data.SqlClient;

namespace API.Services
{
    public class LicenseService
    {
        private readonly IConfiguration _configuration;

        public LicenseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<LicenseModel> GetAllLicenses()
        {
            string connectionString = _configuration.GetConnectionString("conn");
            List<LicenseModel> licenseList = new List<LicenseModel>();

            // Using Dataaccess.GetDataTable method to get data
            DataTable dt = Dataaccess.GetDataTable("spGetAllLicense", CommandType.StoredProcedure, null, connectionString);

            foreach (DataRow row in dt.Rows)
            {
                LicenseModel license = new LicenseModel();
                license.Id = Convert.ToInt32(row["Id"]);
                license.UserName = row["UserName"].ToString();
                license.ApplicationName = row["ApplicationName"].ToString();
                license.LicenseKey = row["LicenseKey"].ToString();
                license.IsActive = Convert.ToBoolean(row["IsActive"]);
                license.CreatedBy = row["CreatedBy"].ToString();
                license.CreatedAt = Convert.ToDateTime(row["CreatedAt"]);

                licenseList.Add(license);
            }

            return licenseList;
        }
    }
}
