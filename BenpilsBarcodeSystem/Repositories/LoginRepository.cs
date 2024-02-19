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
                    // Check if the connection is successfully established
                    if (connection != null)
                    {
                        string query = "SELECT * FROM tbl_usercredential WHERE username = @username AND password = @password";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", username);
                            command.Parameters.AddWithValue("@password", password);

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
