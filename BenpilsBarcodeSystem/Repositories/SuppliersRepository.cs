using BenpilsBarcodeSystem.Database;
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
        public static string tbl_name = "tbl_suppliers", tbl_supplier_items = "tbl_supplier_items";
        public static string col_id = "supplier_id", col_contact_name = "contact_name", col_contact_no = "contact_no", col_address = "address", col_is_active = "is_active", col_item_id = "item_id", col_supplier_item_id = "supplier_item_id";

        public SuppliersRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetSupplierAsync(bool isActive, string searchText = "")
        {
            string selectQuery = $"SELECT {col_id}, {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} WHERE {col_is_active} = '{isActive}'";

            if (string.IsNullOrWhiteSpace(searchText))
            {

            }
            else
            {
                selectQuery = $"SELECT {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} WHERE {col_is_active} = '{isActive}'";

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

        public async Task<bool> ArchiveSupplierAsync(int id, bool archive = false)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_is_active} = '{archive}' WHERE {col_id} = @ID";

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

        public async Task<int> GetActiveSuppliersCount()
        {
            int count = 0;
            string countQuery = $"SELECT COUNT({col_id}) FROM {tbl_name} WHERE {col_is_active} = 'true'";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(countQuery, con))
                    {
                        count = (int)await cmd.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return count;
        }

        public async Task<Supplier> GetSupplierByOrderIdAsync(int orderId, SqlTransaction transaction = null)
        {
            string query = $"SELECT {col_id}, {col_contact_name}, {col_contact_no}, {col_address} FROM {tbl_name} INNER JOIN {PurchaseOrderRepository.tbl_purchase_order} ON {tbl_name}.{col_id} = {PurchaseOrderRepository.tbl_purchase_order}.{col_id} WHERE {PurchaseOrderRepository.tbl_purchase_order}.{PurchaseOrderRepository.col_order_id} = @orderId";

            Supplier supplier = null;

            try
            {
                using (SqlConnection con = transaction?.Connection ?? databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int supplierId = reader.GetInt32(reader.GetOrdinal(col_id));
                                string contactName = reader.GetString(reader.GetOrdinal(col_contact_name));
                                string contactNo = reader.GetString(reader.GetOrdinal(col_contact_no));
                                string address = reader.GetString(reader.GetOrdinal(col_address));

                                supplier = new Supplier
                                {
                                    SupplierID = supplierId,
                                    ContactName = contactName,
                                    ContactNo = contactNo,
                                    Address = address
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return supplier;
        }
    }
}
