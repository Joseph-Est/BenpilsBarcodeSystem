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

        public string GetServerConnectionString()
        {
            return $"Data Source={DataSource};Integrated Security=True;TrustServerCertificate=True";
        }

        public string GetDbName()
        {
            return InitialCatalog;
        }

        public SqlConnection OpenConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public SqlConnection OpenServerConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetServerConnectionString());
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2)
                {
                    Console.WriteLine("SQL Server is not installed or not running.");
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DatabaseExistsAsync()
        {
            using (SqlConnection connection = OpenServerConnection())
            {
                if (connection != null)
                {
                    using (SqlCommand command = new SqlCommand($"SELECT database_id FROM sys.databases WHERE Name = '{InitialCatalog}'", connection))
                    {
                        command.CommandTimeout = 10;

                        object result = await command.ExecuteScalarAsync();
                        return result != null;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
