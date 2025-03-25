using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly string _connectionString;
        public DoctorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }

        public List<DoctorDetails> GetDoctorDetails()
        {
            var doctorDetails = new List<DoctorDetails>();

            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "SELECT Id, Name, Designation, Department, PhoneNumber FROM DoctorDetails;";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctorDetails.Add(new DoctorDetails
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Designation = reader.GetString(2),
                                Department = reader.IsDBNull(3) ? null : reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                            });
                        }
                    }
                }
            }
            return doctorDetails;
        }

        public void CreateDoctorDetail(DoctorDetails doctorDetails)
        {
            var doctorDetail = new List<DoctorDetails>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO DoctorDetails (Name, Designation, Department, PhoneNumber) VALUES (@Name, @Designation, @Department, @PhoneNumber);";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", doctorDetails.Name);
                command.Parameters.AddWithValue("@Designation", doctorDetails.Designation);
                command.Parameters.AddWithValue("@Department", doctorDetails.Department ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", doctorDetails.PhoneNumber);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateDoctorDetail(DoctorDetails doctorDetails)
        {
            var doctorDetail = new List<DoctorDetails>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE DoctorDetails SET Name = @Name, Designation = @Designation, Department = @Department, PhoneNumber = @PhoneNumber WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", doctorDetails.Id);
                command.Parameters.AddWithValue("@Name", doctorDetails.Name);
                command.Parameters.AddWithValue("@Designation", doctorDetails.Designation);
                command.Parameters.AddWithValue("@Department", doctorDetails.Department ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", doctorDetails.PhoneNumber);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void DeleteDoctorDetail(DoctorDetails doctorDetails)
        {
            var doctorDetail = new List<DoctorDetails>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM DoctorDetails WHERE Id = @Id;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", doctorDetails.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
