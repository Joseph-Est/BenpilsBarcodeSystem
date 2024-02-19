using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BenpilsBarcodeSystem
{
    public class DatabaseHelper
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public DatabaseHelper()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        //-------------------------------------------------LOGIN--------------------------------------------------

        public async Task<DataTable> GetUserCredentials(string username, string password)
        {
            try
            {
                // Open the database connection asynchronously
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
                        // Connection failed
                        Console.WriteLine("Database connection failed.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception here
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }


        public DataTable GetSupplierData()
        {
            try
            {
                using (SqlConnection connection = databaseConnection.OpenConnection())
                {
                    if (connection != null)
                    {
                        string query = "SELECT SupplierID, ContactName FROM tbl_supplier";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);
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