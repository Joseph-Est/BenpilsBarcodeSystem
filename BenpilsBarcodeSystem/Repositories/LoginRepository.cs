using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class LoginRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_name = "tbl_user_credentials";
        private string col_id = "id", col_first_name = "first_name", col_last_name = "last_name", col_username = "username", col_password = "password", col_designation = "designation", 
                       col_address = "address", col_contact_no = "contact_no", col_is_active = "is_active";

        public LoginRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetUserCredentials(string username, string password)
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
                                return dataTable;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Database connection failed.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }
    }
}
