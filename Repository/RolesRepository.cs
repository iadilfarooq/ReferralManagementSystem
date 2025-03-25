using Microsoft.Data.SqlClient;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly string _connectionString;
        public RolesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }
        public List<Roles> GetRoles()
        {
            var roles = new List<Roles>();
            using (var connection = new SqlConnection(_connectionString))
            {

                connection.Open();
                string query = "SELECT * FROM Roles";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Roles
                            {
                                Id = reader.GetInt32(0),
                                RoleName = reader.GetString(1)
                            });
                        }
                    };


                    return roles;
                }
            }
        }
    }
}