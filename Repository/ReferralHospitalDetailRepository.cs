using Microsoft.Data.SqlClient;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository.IRepository;
using System.Data;

namespace ReferralManagementSystem.Repository
{
    public class ReferralHospitalDetailRepository : IReferralHospitalDetailRepository
    {
        private readonly string _connectionString;
        public ReferralHospitalDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }

        public async Task<List<ReferralHospitalDetail>> GetHospitalAsync()
        {
            var hospitals = new List<ReferralHospitalDetail>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT * FROM ReferralHospitalDetail;", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                hospitals.Add(new ReferralHospitalDetail
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    HeadName = reader["HeadName"].ToString(),
                    HeadContact = reader["HeadContact"].ToString(),
                    Address = reader["Address"].ToString()
                });
            }
            return hospitals;
        }

        public async Task CreateHospitalAsync(ReferralHospitalDetail referralHospitalDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("INSERT INTO ReferralHospitalDetail ([Name], HeadName, HeadContact, [Address])\r\nVALUES (@Name, @HeadName, @HeadContact, @Address);", connection);
            command.Parameters.AddWithValue("@Name", referralHospitalDetail.Name);
            command.Parameters.AddWithValue("@HeadName", referralHospitalDetail.HeadName);
            command.Parameters.AddWithValue("@HeadContact", referralHospitalDetail.HeadContact);
            command.Parameters.AddWithValue("@Address", referralHospitalDetail.Address);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateHospitalAsync(ReferralHospitalDetail referralHospitalDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = @"
                            UPDATE ReferralHospitalDetail 
                            SET Name = @Name, 
                                HeadName = @HeadName,
                                HeadContact = @HeadContact,
                                Address = @Address
                            WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", referralHospitalDetail.Id);
            command.Parameters.AddWithValue("@Name", referralHospitalDetail.Name);
            command.Parameters.AddWithValue("@HeadName", referralHospitalDetail.HeadName);
            command.Parameters.AddWithValue("@HeadContact", referralHospitalDetail.HeadContact);
            command.Parameters.AddWithValue("@Address", referralHospitalDetail.Address);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteHospitalAsync(ReferralHospitalDetail referralHospitalDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "DELETE FROM ReferralHospitalDetail WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", referralHospitalDetail.Id);

            await command.ExecuteNonQueryAsync();
        }
    }
}
