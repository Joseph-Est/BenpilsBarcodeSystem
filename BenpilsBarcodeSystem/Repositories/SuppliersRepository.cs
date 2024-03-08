﻿using BenpilsBarcodeSystem.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repositories
{
    internal class SuppliersRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_name = "tbl_suppliers";
        private string col_id = "supplier_id", col_contact_name = "contact_name", col_contact_no = "contact_no", col_address = "address", col_is_active = "is_active";

        public SuppliersRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetSupplierAsync(string searchText = "")
        {
            string selectQuery = $"SELECT {col_id}, {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} WHERE {col_is_active} = '1'";

            if (string.IsNullOrWhiteSpace(searchText))
            {

            }
            else
            {
                selectQuery = $"SELECT {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} WHERE {col_is_active} = '1'";

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    selectQuery += $" AND ({col_contact_name} LIKE @searchText OR " +
                                   $"{col_address} LIKE @searchText OR " +
                                   $"{col_contact_no} LIKE @searchText)";
                }
            }

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@searchText", $"%{searchText}%");

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public async Task AddSupplierAsync(string contactName, string contactNo, string address)
        {
            string insertQuery = $"INSERT INTO {tbl_name} ({col_contact_name}, {col_contact_no}, {col_address}) " +
                                 "VALUES (@ContactName, @ContactNo, @Address)";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ContactName", contactName);
                        cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                        cmd.Parameters.AddWithValue("@Address", address);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async Task UpdateSupplierAsync(int id, string contactName, string contactNo, string address)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_contact_name} = @ContactName, {col_contact_no} = @ContactNo, {col_address} = @Address WHERE {col_id} = @ID";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ContactName", contactName);
                        cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@ID", id);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async Task<bool> AreDataExistsAsync(string column1, string data1, string column2, string data2)
        {
            string selectQuery = $"SELECT COUNT(*) FROM {tbl_name} WHERE {column1} = @Data1 COLLATE SQL_Latin1_General_CP1_CI_AS AND {column2} = @Data2 COLLATE SQL_Latin1_General_CP1_CI_AS";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Data1", data1);
                        cmd.Parameters.AddWithValue("@Data2", data2);
                        int count = (int)await cmd.ExecuteScalarAsync();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> ArchiveSupplierAsync(int id)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_is_active} = 0 WHERE {col_id} = @ID";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            List<Supplier> uniqueValuesColumn = new List<Supplier>();

            string selectQuery = $"SELECT {col_id}, {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} WHERE {col_is_active} = '1'";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int supplierId = reader.GetInt32(reader.GetOrdinal(col_id));
                                string contactName = reader.GetString(reader.GetOrdinal(col_contact_name));
                                string contactNo = reader.GetString(reader.GetOrdinal(col_contact_no));
                                string address = reader.GetString(reader.GetOrdinal(col_address));

                                Supplier supplier = new Supplier
                                {
                                    SupplierID = supplierId,
                                    ContactName = contactName,
                                    ContactNo = contactNo,
                                    Address = address
                                };

                                uniqueValuesColumn.Add(supplier);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return uniqueValuesColumn;
        }
    }
}