using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;

namespace BenpilsBarcodeSystem.Repository
{
    internal class UserCredentialsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        public static string tbl_name = "tbl_user_credentials";
        public static string col_id = "id", col_first_name = "first_name", col_last_name = "last_name", col_username = "username", col_password = "password", col_designation = "designation",
                       col_address = "address", col_contact_no = "contact_no", col_is_active = "is_active";

        public UserCredentialsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<bool> AddUserAsync(string firstName, string lastName, string username, string password, string designation, string address, string contactNo)
        {
            string insertQuery = $"INSERT INTO {tbl_name} ({col_first_name}, {col_last_name}, {col_username}, {col_password}, {col_designation}, {col_address}, {col_contact_no}) " +
                                 "VALUES (@FirstName, @LastName, @Username, @Password, @Designation, @Address, @ContactNo)";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Designation", designation);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@ContactNo", contactNo);

                        await cmd.ExecuteNonQueryAsync();
                        //ReportsRepository repository = new ReportsRepository();

                        //bool reportAdded = await repository.AddAuditTrailAsync(con, CurrentUser.User.iD, "Add User", $"Added new user: {username}");

                        //if (!reportAdded)
                        //{
                        //    throw new Exception("Failed to add audit trail");
                        //}

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateUserAsync(int id, string firstName, string lastName, string username, string password, string designation, string address, string contactNo)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_first_name} = @FirstName, {col_last_name} = @LastName, {col_username} = @Username, {col_password} = @Password, {col_designation} = @Designation, {col_address} = @Address, {col_contact_no} = @ContactNo WHERE {col_id} = @ID";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", firstName);
                        cmd.Parameters.AddWithValue("@LastName", lastName);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Designation", designation);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        //if (rowsAffected > 0)
                        //{
                        //    ReportsRepository repository = new ReportsRepository();
                        //    bool reportAdded = await repository.AddInventoryReportAsync(transaction, id, null, "Update Product", quantity, modifiedBy, "Product updated successfully");

                        //    if (!reportAdded)
                        //    {
                        //        throw new Exception("Failed to add inventory report");
                        //    }
                        //}

                        transaction.Commit();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<DataTable> GetUserCredentialsAsync(bool isActive, string searchText = null)
        {
            string selectQuery = $"SELECT * FROM {tbl_name} WHERE {col_is_active} = '{isActive}'";

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                selectQuery += $" AND ({col_first_name} LIKE @searchText OR " +
                                   $"{col_last_name} LIKE @searchText OR " +
                                   $"{col_designation} LIKE @searchText OR " +
                                   $"{col_contact_no} LIKE @searchText OR " +
                                   $"{col_username} LIKE @searchText OR " +
                                   $"{col_address} LIKE @searchText)";
            }

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("name", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            row["name"] = $"{row[col_first_name]} {row[col_last_name]}" ;
                        }

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                using (SqlConnection connection = await Task.Run(() => databaseConnection.OpenConnection()))
                {
                    if (connection != null)
                    {
                        string query = $"SELECT * FROM {tbl_name} WHERE {col_username} = @Username AND {col_password} = @Password AND {col_is_active} = 'true'";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                await Task.Run(() => adapter.Fill(dataTable));

                                if (dataTable.Rows.Count > 0)
                                {
                                    int userId = Convert.ToInt32(dataTable.Rows[0][col_id]);
                                    CurrentUser.User = new User
                                    {
                                        iD = userId,
                                        FirstName = dataTable.Rows[0][col_first_name].ToString(),
                                        LastName = dataTable.Rows[0][col_last_name].ToString(),
                                        Username = dataTable.Rows[0][col_username].ToString(),
                                        Designation = dataTable.Rows[0][col_designation].ToString()
                                    };

                                    ReportsRepository repository = new ReportsRepository();

                                    bool auditTrailAdded = await repository.AddAuditTrailAsync(connection, userId, "Login", "User logged in successfully.");

                                    if (!auditTrailAdded)
                                    {
                                        throw new Exception("Failed to add audit trail");
                                    }

                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Database connection failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                using (SqlConnection connection = await Task.Run(() => databaseConnection.OpenConnection()))
                {
                    if (connection != null)
                    {
                        ReportsRepository repository = new ReportsRepository();

                        bool auditTrailAdded = await repository.AddAuditTrailAsync(connection, CurrentUser.User.iD, "Logout", "User logged out successfully.");

                        if (!auditTrailAdded)
                        {
                            throw new Exception("Failed to add audit trail");
                        }

                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Database connection failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }

        public async Task<bool> IsDataExistsAsync(string columnName, string data)
        {
            string selectQuery = $"SELECT COUNT(*) FROM {tbl_name} WHERE {columnName} = @Data COLLATE SQL_Latin1_General_CP1_CI_AS";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Data", data);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> ArchiveUserAsync(int id, bool archive = false)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_is_active} = '{archive}' WHERE {col_id} = @ID";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        //if (rowsAffected > 0)
                        //{
                        //    ReportsRepository repository = new ReportsRepository();
                        //    bool reportAdded = await repository.AddInventoryReportAsync(transaction, id, null, "Archive Item", 0, 0, 0, CurrentUser.User.iD, "Item archived succesfully");

                        //    if (!reportAdded)
                        //    {
                        //        throw new Exception("Failed to add inventory report");
                        //    }
                        //}

                        transaction.Commit();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
