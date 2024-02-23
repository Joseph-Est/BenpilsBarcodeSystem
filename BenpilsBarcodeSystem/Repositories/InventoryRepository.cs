using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class InventoryRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public InventoryRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetProductsAsync()
        {
            string selectQuery = "SELECT * FROM tbl_itemmasterdata";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("Status", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            int quantity = Convert.ToInt32(row["Quantity"]);

                            if (quantity > 1000)
                                row["Status"] = "High-Stock";
                            else if (quantity > 100)
                                row["Status"] = "In-Stock";
                            else if (quantity == 0)
                                row["Status"] = "No Stock";
                            else
                                row["Status"] = "Low-Stock";
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
            string insertQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                 "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

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

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async Task UpdateProductAsync(int productId, string barcode, string itemName, string motorBrand, string brand, decimal unitPrice, int quantity, string category, string size)
        {
            string updateQuery = "UPDATE tbl_itemmasterdata SET Barcode = @Barcode, ItemName = @ItemName, MotorBrand = @MotorBrand, Brand = @Brand, UnitPrice = @UnitPrice, Quantity = @Quantity, Category = @Category, Size = @Size WHERE ProductID = @ProductId";

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
                        cmd.Parameters.AddWithValue("@ProductId", productId);

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
            string selectQuery = $"SELECT COUNT(*) FROM tbl_itemmasterdata WHERE {columnName} = @Data";

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
            string selectQuery = $"SELECT COUNT(*) FROM tbl_itemmasterdata WHERE {column1} = @Data1 AND {column2} = @Data2";

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
    }
}
