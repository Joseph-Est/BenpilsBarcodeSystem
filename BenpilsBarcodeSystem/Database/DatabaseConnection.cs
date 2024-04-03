using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Database
{
    internal class DatabaseConnection
    {
        private readonly string DataSource;
        private const string InitialCatalog = "BenpilsMotorcycleDatabase";
        private readonly string connectionString;

        public DatabaseConnection()
        {
            string projectConnectionString = Properties.Settings.Default.ConnectionString;

            if (!string.IsNullOrEmpty(projectConnectionString))
            {
                connectionString = projectConnectionString;
            }
            else
            {
                DataSource = Environment.MachineName;
                connectionString = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security=True";
            }
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
