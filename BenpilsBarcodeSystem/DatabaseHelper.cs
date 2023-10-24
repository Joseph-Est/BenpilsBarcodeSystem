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
        private string connectionString = "Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True";

        public DataTable GetSupplierData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Modify this query according to your database structure and relationships.
                string query = "SELECT SupplierID, ContactName, CompanyName                                                                                                                                                                                                                                                                                                                                                                                                                                                                    FROM tbl_supplier";

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
        }
    }
}
