using API.Models;
using System.Data;
using System.Data.SqlClient;

namespace API.Data
{
    public class LicenseDataAccessLayer
    {
      
        private readonly IConfiguration _configuration;

        public LicenseDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<LicenseModel> GetAllLicenses()
        {
            string connectionString = _configuration.GetConnectionString("conn");
            List<LicenseModel> licenseList = new List<LicenseModel>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllLicense", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    LicenseModel license = new LicenseModel();
                    license.Id = Convert.ToInt32(rdr["Id"]);
                    license.UserName = rdr["UserName"].ToString();
                    license.ApplicationName = rdr["ApplicationName"].ToString();
                    license.LicenseKey = rdr["LicenseKey"].ToString();
                    license.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                    license.CreatedBy = rdr["CreatedBy"].ToString();
                    license.CreatedAt = Convert.ToDateTime(rdr["CreatedAt"]);

                    licenseList.Add(license);
                }
                con.Close();
            }
            return licenseList;
        }
    }
}
