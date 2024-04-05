using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Database
{
    internal class DatabaseInitializer
    {
        private readonly DatabaseConnection dbConnection;
        readonly string ScriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "database.sql");

        public DatabaseInitializer()
        {
            dbConnection = new DatabaseConnection();
        }

        public async Task<bool> InitializeDatabaseAsync()
        {
            if (await dbConnection.DatabaseExistsAsync())
            {
                return true;
            }
            else
            {
                if (await CreateDatabaseAsync(dbConnection.GetDbName()))
                {
                    if (await CreateDatabaseFromScriptAsync() == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public async Task<int> CreateDatabaseFromScriptAsync()
        {
            try
            {
                Console.WriteLine("path: " + ScriptFilePath);

                if (!File.Exists(ScriptFilePath))
                {
                    Console.WriteLine("SQL script file not found.");
                    return 1;
                }

                string connectionString = dbConnection.GetServerConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Execute script
                            string scriptPath = Path.Combine(Application.StartupPath, "Database", "database.sql");
                            string script = System.IO.File.ReadAllText(scriptPath);
                            var batches = script.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var batch in batches)
                            {
                                using (SqlCommand command = new SqlCommand(batch, connection, transaction))
                                {
                                    await command.ExecuteNonQueryAsync();
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();

                            using (SqlCommand command = new SqlCommand($"DROP DATABASE {dbConnection.GetDbName()}", connection))
                            {
                                await command.ExecuteNonQueryAsync();
                            }

                            throw;
                        }
                    }
                }

                return 0;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.FileName}");
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }


        public async Task<bool> CreateDatabaseAsync(string databaseName)
        {
            try
            {
                string connectionString = dbConnection.GetServerConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string createDatabaseQuery = $"CREATE DATABASE {databaseName};";
                    SqlCommand cmd = new SqlCommand(createDatabaseQuery, connection);
                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            
        }
    }
}
