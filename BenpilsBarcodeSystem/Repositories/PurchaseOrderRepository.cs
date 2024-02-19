using BenpilsBarcodeSystem.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class PurchaseOrderRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public PurchaseOrderRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
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
