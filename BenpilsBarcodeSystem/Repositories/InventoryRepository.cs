using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenpilsBarcodeSystem.Helpers;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Repository
{
    internal class InventoryRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_name = "tbl_item_master_data";
        private string col_id = "id", col_barcode = "barcode", col_product_id = "product_id", col_item_name = "item_name", col_motor_brand = "motor_brand", 
                       col_brand = "brand", col_unit_price = "unit_price", col_quantity = "quantity", col_category = "category", col_size = "size", col_is_active = "is_active";

        public InventoryRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetProductsAsync(string searchText = "", string category = "All", string brand = "All")
        {
            string selectQuery;

            if (string.IsNullOrWhiteSpace(searchText) && category == "All" && brand == "All")
            {
                selectQuery = $"SELECT * FROM {tbl_name} WHERE {col_is_active} = '1'";
            }
            else
            {
                selectQuery = $"SELECT * FROM {tbl_name} WHERE {col_is_active} = '1'";

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    selectQuery += $" AND ({col_item_name} LIKE @searchText OR " +
                                   $"{col_motor_brand} LIKE @searchText OR " +
                                   $"{col_brand} LIKE @searchText OR " +
                                   $"{col_category} LIKE @searchText OR " +
                                   $"{col_barcode} LIKE @searchText OR " +
                                   $"{col_product_id} LIKE @searchText)";
                }

                if (!string.IsNullOrWhiteSpace(category))
                {
                    if (category == "All")
                    {

                    }
                    else
                    {
                        selectQuery += $" AND {col_category} = @category";
                    }
                }

                if (!string.IsNullOrWhiteSpace(brand))
                {
                    if (brand == "All")
                    {

                    }
                    else
                    {
                        selectQuery += $" AND {col_brand} = @brand";
                    }
                   
                }
            }

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@searchText", $"%{searchText}%");
                        adapter.SelectCommand.Parameters.AddWithValue("@category", category);
                        adapter.SelectCommand.Parameters.AddWithValue("@brand", brand);

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("status", typeof(string));
                        dt.Columns.Add("formatted_price", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            int quantity = Convert.ToInt32(row[col_quantity]);

                            if (quantity > 1000)
                                row["status"] = "High-Stock";
                            else if (quantity > 100)
                                row["status"] = "In-Stock";
                            else if (quantity == 0)
                                row["status"] = "No Stock";
                            else
                                row["status"] = "Low-Stock";

                            row["formatted_price"] = InputValidator.StringToFormattedPrice(row[col_unit_price].ToString());
                        }

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

        public async Task AddProductAsync(string barcode, int productId, string itemName, string motorBrand, string brand, decimal unitPrice, int quantity, string category, string size)
        {
            string insertQuery = $"INSERT INTO {tbl_name} ({col_barcode}, {col_product_id}, {col_item_name}, {col_motor_brand}, {col_brand}, {col_unit_price}, {col_quantity}, {col_category}, {col_size}, {col_is_active}) " +
                                 "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size, @IsActive)";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@MotorBrand", motorBrand);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Size", size);
                        cmd.Parameters.AddWithValue("@IsActive", 1);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async Task UpdateProductAsync(int id, string barcode, string itemName, string motorBrand, string brand, decimal unitPrice, int quantity, string category, string size)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_barcode} = @Barcode, {col_item_name} = @ItemName, {col_motor_brand} = @MotorBrand, {col_brand} = @Brand, {col_unit_price} = @UnitPrice, {col_quantity} = @Quantity, {col_category} = @Category, {col_size} = @Size WHERE {col_id} = @ID";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@MotorBrand", motorBrand);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Size", size);
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

        public async Task<bool> IsDataExistsAsync(string columnName, string data)
        {
            string selectQuery = $"SELECT COUNT(*) FROM {tbl_name} WHERE {columnName} = @Data COLLATE SQL_Latin1_General_CP1_CI_AS";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Data", data);
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

        public async Task ArchiveProductAsync(int id)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_is_active} = 0 WHERE {col_id} = @ID";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
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

        public async Task<(List<string>, List<string>)> GetCategoryBrandValuesAsync()
        {
            List<string> uniqueValuesColumn1 = new List<string>();
            List<string> uniqueValuesColumn2 = new List<string>();

            string selectQuery = $"SELECT DISTINCT {col_category}, {col_brand} FROM {tbl_name}";

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
                                string value1 = reader[col_category].ToString();
                                string value2 = reader[col_brand].ToString();

                                uniqueValuesColumn1.Add(value1);
                                uniqueValuesColumn2.Add(value2);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            uniqueValuesColumn1.Insert(0, "All");
            uniqueValuesColumn2.Insert(0, "All");

            return (uniqueValuesColumn1, uniqueValuesColumn2);
        }

        public async Task<bool> DeductStockAsync(int id, int amountToDeduct)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_quantity} = {col_quantity} - @AmountToDeduct WHERE {col_id} = @Id";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@AmountToDeduct", amountToDeduct);
                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}
