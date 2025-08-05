using System.Data;
using Microsoft.Data.SqlClient;
using ReferralManagementSystem.Models;
using ReferralManagementSystem.Repository.IRepository;

namespace ReferralManagementSystem.Repository
{
    public class PatientReferralFormRepository : IPatientReferralFormRepository
    {
        private readonly string _connectionString;
        public PatientReferralFormRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("dbcs");
        }
    //get all patient 
        public async Task<List<PatientReferralForm>> GetPatientRFDetailsAsync()
        {
            var patientRF = new List<PatientReferralForm>();

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetPatientDetail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                patientRF.Add(new PatientReferralForm
                {
                    ReferralID = (int)reader["ReferralID"],
                    ReferredFrom = reader["ReferredFrom"].ToString(),
                    NatureOfReferral = reader["NatureOfReferral"].ToString(),
                    Shift = reader["Shift"].ToString(),
                    PatientName = reader["PatientName"].ToString(),
                    SO_WO = reader["SO_WO"].ToString(),
                    MedicalRecordNo = reader["MedicalRecordNo"].ToString(),
                    IsActive = (bool)reader["IsActive"],
                    CNIC = reader["CNIC"].ToString(),
                    Age = (int)reader["Age"],
                    Gender = reader["Gender"].ToString(),
                    PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : reader["PhoneNumber"].ToString(),
                    Address = reader["Address"].ToString(),
                    Triage = reader["Triage"].ToString(),
                    ResponseToTreatment = reader["ResponseToTreatment"].ToString(),
                    PatientConditionAtExit = reader["PatientConditionAtExit"].ToString(),
                    Pulse = reader["Pulse"] == DBNull.Value ? (int?)null : (int)reader["Pulse"],
                    BP = reader["BP"] == DBNull.Value ? (int?)null : (int)reader["BP"],
                    TEMP = reader["TEMP"] == DBNull.Value ? (int?)null : (int)reader["TEMP"],
                    RR = reader["RR"] == DBNull.Value ? (int?)null : (int)reader["RR"],
                    SPO2 = reader["SPO2"] == DBNull.Value ? (int?)null : (int)reader["SPO2"],
                    Stability = reader["Stability"].ToString(),
                    Diagnosis = reader["Diagnosis"].ToString(),
                    Complications = reader["Complications"] == DBNull.Value ? null : reader["Complications"].ToString(),
                    AnyDrugAllergy = reader["AnyDrugAllergy"] == DBNull.Value ? null : reader["AnyDrugAllergy"].ToString(),
                    Instruction = reader["Instruction"] == DBNull.Value ? null : reader["Instruction"].ToString(),
                    ReferringMode = reader["ReferringMode"].ToString(),
                    ReasonOfRefer = reader["ReasonOfRefer"] == DBNull.Value ? null : reader["ReasonOfRefer"].ToString(),
                    DateIn = (DateTime)reader["DateIn"],
                    DateOut = (DateTime)reader["DateOut"],
                    ReferredFromDepartment = reader["ReferredFromDepartment"].ToString(),
                    HospitalID = (int)reader["HospitalID"],
                    HospitalName = reader["HospitalName"].ToString(),
                    DoctorId = (int)reader["DoctorId"],
                    DoctorName = reader["DoctorName"].ToString(),
                    UserId = (int)reader["UserId"],
                    UserName = reader["UserName"].ToString()
                });
            }

            return patientRF;
        }
        public async Task<PatientReferralForm> GetPatientRFIdAsync(int id)
        {
            PatientReferralForm patientRF = null;

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_GetPatientById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Pass the 'id' parameter to the stored procedure
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) // Expecting only one record
            {
                patientRF = new PatientReferralForm
                {
                    ReferralID = (int)reader["ReferralID"],
                    ReferredFrom = reader["ReferredFrom"].ToString(),
                    NatureOfReferral = reader["NatureOfReferral"].ToString(),
                    Shift = reader["Shift"].ToString(),
                    PatientName = reader["PatientName"].ToString(),
                    SO_WO = reader["SO_WO"].ToString(),
                    MedicalRecordNo = reader["MedicalRecordNo"].ToString(),
                    IsActive = (bool)reader["IsActive"],
                    CNIC = reader["CNIC"].ToString(),
                    Age = (int)reader["Age"],
                    Gender = reader["Gender"].ToString(),
                    PhoneNumber = reader["PhoneNumber"] == DBNull.Value ? null : reader["PhoneNumber"].ToString(),
                    Address = reader["Address"].ToString(),
                    Triage = reader["Triage"].ToString(),
                    ResponseToTreatment = reader["ResponseToTreatment"].ToString(),
                    PatientConditionAtExit = reader["PatientConditionAtExit"].ToString(),
                    Pulse = reader["Pulse"] == DBNull.Value ? (int?)null : (int)reader["Pulse"],
                    BP = reader["BP"] == DBNull.Value ? (int?)null : (int)reader["BP"],
                    TEMP = reader["TEMP"] == DBNull.Value ? (int?)null : (int)reader["TEMP"],
                    RR = reader["RR"] == DBNull.Value ? (int?)null : (int)reader["RR"],
                    SPO2 = reader["SPO2"] == DBNull.Value ? (int?)null : (int)reader["SPO2"],
                    Stability = reader["Stability"].ToString(),
                    Diagnosis = reader["Diagnosis"].ToString(),
                    Complications = reader["Complications"] == DBNull.Value ? null : reader["Complications"].ToString(),
                    AnyDrugAllergy = reader["AnyDrugAllergy"] == DBNull.Value ? null : reader["AnyDrugAllergy"].ToString(),
                    Instruction = reader["Instruction"] == DBNull.Value ? null : reader["Instruction"].ToString(),
                    ReferringMode = reader["ReferringMode"].ToString(),
                    ReasonOfRefer = reader["ReasonOfRefer"] == DBNull.Value ? null : reader["ReasonOfRefer"].ToString(),
                    DateIn = (DateTime)reader["DateIn"],
                    DateOut = (DateTime)reader["DateOut"],
                    ReferredFromDepartment = reader["ReferredFromDepartment"].ToString(),
                    HospitalID = (int)reader["HospitalID"],
                    HospitalName = reader["HospitalName"].ToString(),
                    DoctorId = (int)reader["DoctorId"],
                    DoctorName = reader["DoctorName"].ToString(),
                    UserId = (int)reader["UserId"],
                    UserName = reader["UserName"].ToString()
                };
            }

            return patientRF;
        }
        public async Task CreatePatientRFAsync(PatientReferralForm patientReferralForm)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_InsertPatientDetail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
          
            command.Parameters.AddWithValue("@ReferredFrom", patientReferralForm.ReferredFrom);
            command.Parameters.AddWithValue("@NatureOfReferral", patientReferralForm.NatureOfReferral);
            command.Parameters.AddWithValue("@Shift", patientReferralForm.Shift);
            command.Parameters.AddWithValue("@PatientName", patientReferralForm.PatientName);
            command.Parameters.AddWithValue("@SO_WO", patientReferralForm.SO_WO);
            command.Parameters.AddWithValue("@MedicalRecordNo", patientReferralForm.MedicalRecordNo);
            command.Parameters.AddWithValue("@IsActive", patientReferralForm.IsActive);
            command.Parameters.AddWithValue("@CNIC", patientReferralForm.CNIC);
            command.Parameters.AddWithValue("@Age", patientReferralForm.Age);
            command.Parameters.AddWithValue("@Gender", patientReferralForm.Gender);
            command.Parameters.AddWithValue("@PhoneNumber", (object?)patientReferralForm.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@Address", patientReferralForm.Address);
            command.Parameters.AddWithValue("@Triage", patientReferralForm.Triage);
            command.Parameters.AddWithValue("@ResponseToTreatment", patientReferralForm.ResponseToTreatment);
            command.Parameters.AddWithValue("@PatientConditionAtExit", patientReferralForm.PatientConditionAtExit);
            command.Parameters.AddWithValue("@Pulse", (object?)patientReferralForm.Pulse ?? DBNull.Value);
            command.Parameters.AddWithValue("@BP", (object?)patientReferralForm.BP ?? DBNull.Value);
            command.Parameters.AddWithValue("@TEMP", (object?)patientReferralForm.TEMP ?? DBNull.Value);
            command.Parameters.AddWithValue("@RR", (object?)patientReferralForm.RR ?? DBNull.Value);
            command.Parameters.AddWithValue("@SPO2", (object?)patientReferralForm.SPO2 ?? DBNull.Value);
            command.Parameters.AddWithValue("@Stability", patientReferralForm.Stability);
            command.Parameters.AddWithValue("@Diagnosis", patientReferralForm.Diagnosis);
            command.Parameters.AddWithValue("@Complications", (object?)patientReferralForm.Complications ?? DBNull.Value);
            command.Parameters.AddWithValue("@AnyDrugAllergy", (object?)patientReferralForm.AnyDrugAllergy ?? DBNull.Value);
            command.Parameters.AddWithValue("@Instruction", (object?)patientReferralForm.Instruction ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReferringMode", patientReferralForm.ReferringMode);
            command.Parameters.AddWithValue("@ReasonOfRefer", (object?)patientReferralForm.ReasonOfRefer ?? DBNull.Value);
            command.Parameters.AddWithValue("@DateIn", patientReferralForm.DateIn);
            command.Parameters.AddWithValue("@DateOut", patientReferralForm.DateOut);
            command.Parameters.AddWithValue("@ReferredFromDepartment", patientReferralForm.ReferredFromDepartment);
            command.Parameters.AddWithValue("@DoctorId", patientReferralForm.DoctorId);
            command.Parameters.AddWithValue("@HospitalID", patientReferralForm.HospitalID);
            command.Parameters.AddWithValue("@UserId", patientReferralForm.UserId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdatePatientRFAsync(PatientReferralForm patientReferralForm)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("sp_UpdatePatientDetail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@ReferralID", patientReferralForm.ReferralID);
            command.Parameters.AddWithValue("@ReferredFrom", patientReferralForm.ReferredFrom);
            command.Parameters.AddWithValue("@NatureOfReferral", patientReferralForm.NatureOfReferral);
            command.Parameters.AddWithValue("@Shift", patientReferralForm.Shift);
            command.Parameters.AddWithValue("@PatientName", patientReferralForm.PatientName);
            command.Parameters.AddWithValue("@SO_WO", patientReferralForm.SO_WO);
            command.Parameters.AddWithValue("@MedicalRecordNo", patientReferralForm.MedicalRecordNo);
            command.Parameters.AddWithValue("@IsActive", patientReferralForm.IsActive);
            command.Parameters.AddWithValue("@CNIC", patientReferralForm.CNIC);
            command.Parameters.AddWithValue("@Age", patientReferralForm.Age);
            command.Parameters.AddWithValue("@Gender", patientReferralForm.Gender);
            command.Parameters.AddWithValue("@PhoneNumber", (object?)patientReferralForm.PhoneNumber ?? DBNull.Value);
            command.Parameters.AddWithValue("@Address", patientReferralForm.Address);
            command.Parameters.AddWithValue("@Triage", patientReferralForm.Triage);
            command.Parameters.AddWithValue("@ResponseToTreatment", patientReferralForm.ResponseToTreatment);
            command.Parameters.AddWithValue("@PatientConditionAtExit", patientReferralForm.PatientConditionAtExit);
            command.Parameters.AddWithValue("@Pulse", (object?)patientReferralForm.Pulse ?? DBNull.Value);
            command.Parameters.AddWithValue("@BP", (object?)patientReferralForm.BP ?? DBNull.Value);
            command.Parameters.AddWithValue("@TEMP", (object?)patientReferralForm.TEMP ?? DBNull.Value);
            command.Parameters.AddWithValue("@RR", (object?)patientReferralForm.RR ?? DBNull.Value);
            command.Parameters.AddWithValue("@SPO2", (object?)patientReferralForm.SPO2 ?? DBNull.Value);
            command.Parameters.AddWithValue("@Stability", patientReferralForm.Stability);
            command.Parameters.AddWithValue("@Diagnosis", patientReferralForm.Diagnosis);
            command.Parameters.AddWithValue("@Complications", (object?)patientReferralForm.Complications ?? DBNull.Value);
            command.Parameters.AddWithValue("@AnyDrugAllergy", (object?)patientReferralForm.AnyDrugAllergy ?? DBNull.Value);
            command.Parameters.AddWithValue("@Instruction", (object?)patientReferralForm.Instruction ?? DBNull.Value);
            command.Parameters.AddWithValue("@ReferringMode", patientReferralForm.ReferringMode);
            command.Parameters.AddWithValue("@ReasonOfRefer", (object?)patientReferralForm.ReasonOfRefer ?? DBNull.Value);
            command.Parameters.AddWithValue("@DateIn", patientReferralForm.DateIn);
            command.Parameters.AddWithValue("@DateOut", patientReferralForm.DateOut);
            command.Parameters.AddWithValue("@ReferredFromDepartment", patientReferralForm.ReferredFromDepartment);
            command.Parameters.AddWithValue("@DoctorId", patientReferralForm.DoctorId);
            command.Parameters.AddWithValue("@HospitalID", patientReferralForm.HospitalID);
            command.Parameters.AddWithValue("@UserId", patientReferralForm.UserId);

            await command.ExecuteNonQueryAsync();
        }

        //Delete method
        public async Task DeletePatientRFAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "sp_DeletePatientDetail";

            using var command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ReferralID", id);

            await command.ExecuteNonQueryAsync();
        }
    
    }
}
