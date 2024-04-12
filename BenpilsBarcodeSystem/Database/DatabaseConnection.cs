using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Database
{
    internal class DatabaseConnection
    {
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
                string DataSource = ".\\SQLExpress";
                connectionString = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security=True";
                if (TestConnection(connectionString))
                {
                    Properties.Settings.Default.ConnectionString = connectionString;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    DataSource = Environment.MachineName;
                    connectionString = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security=True";
                    
                    if (TestConnection(connectionString))
                    {
                        Properties.Settings.Default.ConnectionString = connectionString;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        throw new Exception("Unable to connect to server");
                    }
                }
            }
        }

        public string GetServerConnectionString()
        {
            string dataSource = new SqlConnectionStringBuilder(connectionString).DataSource;

            return $"Data Source={dataSource};Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5";
        }

        public string GetDbName()
        {
            return InitialCatalog;
        }

        private bool TestConnection(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ConnectionString += ";Connect Timeout=5";
                try
                {
                    connection.Open();
                    Console.WriteLine($"Successfully established a connection with: ({connectionString})");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to establish a connection with: ({connectionString}). The following error occurred: '{ex.Message}'.");
                    return false;
                }
            }
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DatabaseExistsAsync()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"DatabaseExistsMethod Error:{ex}");
                return false;
            }
            
        }
    }
}
