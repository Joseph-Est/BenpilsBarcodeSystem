using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenpilsBarcodeSystem.Entities;

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

        public async Task<DataTable> GetUserCredentialsAsync()
        {
            string selectQuery = $"SELECT * FROM {tbl_name} WHERE {col_is_active} = '1'";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));
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
                        string query = $"SELECT * FROM {tbl_name} WHERE {col_username} = @Username AND {col_password} = @Password";

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
                                    CurrentUser.User = new User
                                    {
                                        iD = Convert.ToInt32(dataTable.Rows[0][col_id]),
                                        FirstName = dataTable.Rows[0][col_first_name].ToString(),
                                        LastName = dataTable.Rows[0][col_last_name].ToString(),
                                        Username = dataTable.Rows[0][col_username].ToString(),
                                        Designation = dataTable.Rows[0][col_designation].ToString()
                                    };
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
    }
}
