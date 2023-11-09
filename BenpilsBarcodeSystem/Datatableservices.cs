using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem
{
    public class Datatableservices
    { 
    private string connectionString = "Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True";

    public DataTable GetServicesData()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ServicesName,Price FROM tbl_services";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTableservices = new DataTable();
                        adapter.Fill(dataTableservices);
                        return dataTableservices;
                    }
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
}
}
                    
   

