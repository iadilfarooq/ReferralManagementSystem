using System.Data;
using Microsoft.Data.SqlClient;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;
        public UsersRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }

        public async Task<List<Users>> GetUsersDetailsAsync()
        {
            var usersDetails = new List<Users>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetUsers", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                usersDetails.Add(new Users
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    Email = reader.GetString(3),     // Email
                    PicturePath = reader.GetString(4),
              
                    RoleId = 0, // Not needed, but keeping it for reference
                    Role = new Roles
                    {
                        RoleName = reader.GetString(5) // Mapping RoleName correctly
                    }
                });
            }
            return usersDetails;
        }

        

        public async Task<Users> GetUserByIdAsync(int id)
        {
            Users user = null;

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Use raw SQL query instead of stored procedure
            string query = @"
                        SELECT u.Id, u.Username, u.PasswordHash, u.Email,u.PicturePath, u.RoleId, r.RoleName 
                        FROM Users u
                        INNER JOIN Roles r ON u.RoleId = r.Id
                        WHERE u.Id = @UserId";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) // Fetching only one user
            {
                user = new Users
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    Email = reader.GetString(3),
                    PicturePath = reader.GetString(4),
                  
                    RoleId = reader.GetInt32(5), // Fetch RoleId
                    Role = new Roles
                    {
                        RoleName = reader.GetString(6) // Fetch RoleName
                    }
                };
            }
            return user;
        }

        public async Task CreateUserAsync(Users users, IFormFile picture)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string uniqueFileName = null;

            if (picture != null && picture.Length > 0)
            {
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");
                Directory.CreateDirectory(uploadFolder); // Ensure the folder exists
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }
            }

            using var command = new SqlCommand("sp_CreateUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", users.Username);
            command.Parameters.AddWithValue("@PasswordHash", users.PasswordHash);
            command.Parameters.AddWithValue("@Email", users.Email);
            command.Parameters.AddWithValue("@RoleId", users.RoleId);
            command.Parameters.AddWithValue("@PicturePath", uniqueFileName ?? (object)DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateUserAsync(Users users, IFormFile? picture)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string uniqueFileName = null;

            if (picture != null && picture.Length > 0)
            {
                // Get old picture path from DB
                string getOldImageQuery = "SELECT PicturePath FROM Users WHERE Id = @UserId";
                using var getOldImageCommand = new SqlCommand(getOldImageQuery, connection);
                getOldImageCommand.Parameters.AddWithValue("@UserId", users.Id);
                string? oldFileName = (await getOldImageCommand.ExecuteScalarAsync())?.ToString();

                // Delete old image from folder
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users", oldFileName);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                // Save new image
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");
                Directory.CreateDirectory(uploadFolder);
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }
            }

            using var command = new SqlCommand("sp_UpdateUser", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", users.Id);
            command.Parameters.AddWithValue("@Username", users.Username);
            command.Parameters.AddWithValue("@PasswordHash", users.PasswordHash);
            command.Parameters.AddWithValue("@Email", users.Email);
            command.Parameters.AddWithValue("@RoleId", users.RoleId);
            command.Parameters.AddWithValue("@PicturePath", uniqueFileName ?? (object)DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }



        public async Task DeleteUserAsync(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Retrieve old picture path before deleting the user
            string getOldImageQuery = "SELECT PicturePath FROM Users WHERE Id = @UserId";
            using var getOldImageCommand = new SqlCommand(getOldImageQuery, connection);
            getOldImageCommand.Parameters.AddWithValue("@UserId", userId);

            string? oldFileName = (await getOldImageCommand.ExecuteScalarAsync())?.ToString();

            // Delete the old image if it exists
            if (!string.IsNullOrEmpty(oldFileName))
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users", oldFileName);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            // Now delete the user
            string query = "DELETE FROM Users WHERE Id = @UserId";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserId", userId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<Users?> AuthenticateAsync(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT Id, Username, Email, RoleId FROM Users WHERE Username = @Username AND PasswordHash = @Password";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);  // Hash in real-world apps!

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Users
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    RoleId = reader.GetInt32(3)
                };
            }

            return null; // User not found
        }
    }
}
