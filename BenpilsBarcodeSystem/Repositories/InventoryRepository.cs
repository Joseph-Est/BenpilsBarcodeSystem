 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenpilsBarcodeSystem.Helpers;
using System.Windows.Forms;
using BenpilsBarcodeSystem.Entities;

namespace BenpilsBarcodeSystem.Repository
{
    internal class InventoryRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_name = "tbl_item_master_data";
        private string col_id = "id", col_barcode = "barcode", col_item_name = "item_name", col_motor_brand = "motor_brand", 
                       col_brand = "brand", col_purchase_price = "purchase_price", col_selling_price = "selling_price", col_quantity = "quantity", col_category = "category", col_size = "size", col_is_active = "is_active";

        private int lowStockThreshold = 20;
        private int highStockThreshold = 100;

        public InventoryRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetProductsAsync(string searchText = "", string category = "All", string brand = "All")
        {
            string selectQuery = $"SELECT {col_id}, {col_barcode}, {col_item_name}, {col_brand}, {col_motor_brand}, {col_purchase_price}, {col_selling_price}, {col_quantity}, {col_category}, {col_size} FROM {tbl_name} WHERE {col_is_active} = '1'";


            if (string.IsNullOrWhiteSpace(searchText) && category == "All" && brand == "All")
            {
               
            }
            else
            {
                selectQuery = $"SELECT {col_id}, {col_barcode}, {col_item_name}, {col_brand}, {col_motor_brand}, {col_purchase_price}, {col_selling_price}, {col_quantity}, {col_category}, {col_size} FROM {tbl_name} WHERE {col_is_active} = '1'";

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    selectQuery += $" AND ({col_item_name} LIKE @searchText OR " +
                                   $"{col_motor_brand} LIKE @searchText OR " +
                                   $"{col_brand} LIKE @searchText OR " +
                                   $"{col_category} LIKE @searchText OR " +
                                   $"{col_barcode} LIKE @searchText)";
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
                        dt.Columns.Add("formatted_purchase_price", typeof(string));
                        dt.Columns.Add("formatted_selling_price", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            int quantity = Convert.ToInt32(row[col_quantity]);

                            if (quantity > highStockThreshold)
                                row["status"] = "High-Stock";
                            else if (quantity >= lowStockThreshold)
                                row["status"] = "In-Stock";
                            else if (quantity < lowStockThreshold && quantity > 0)
                                row["status"] = "Low-Stock";
                            else
                                row["status"] = "No Stock";

                            row["formatted_purchase_price"] = InputValidator.StringToFormattedPrice(row[col_purchase_price].ToString());
                            row["formatted_selling_price"] = InputValidator.StringToFormattedPrice(row[col_selling_price].ToString());
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

        public async Task<bool> AddProductAsync(string barcode, string itemName, string category, string brand, string motorBrand, string size, int quantity, decimal purchasePrice, decimal sellingPrice, int? supplier)
        {
            string insertQuery = $"INSERT INTO {tbl_name} ({col_barcode}, {col_item_name}, {col_motor_brand}, {col_brand}, {col_purchase_price}, {col_selling_price}, {col_quantity}, {col_category}, {col_size}) " +
                                 "OUTPUT INSERTED.id " +
                                 "VALUES (@Barcode, @ItemName, @MotorBrand, @Brand, @PurchasePrice, @SellingPrice, @Quantity, @Category, @Size)";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@MotorBrand", motorBrand);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                        cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Size", size);

                        int itemId = (int)await cmd.ExecuteScalarAsync();

                        if (supplier > 0)
                        {
                            string insertSupplierItemQuery = $"INSERT INTO tbl_supplier_items (supplier_id, item_id) VALUES (@SupplierId, @ItemId)";
                            using (SqlCommand supplierCmd = new SqlCommand(insertSupplierItemQuery, con, transaction))
                            {
                                supplierCmd.Parameters.AddWithValue("@SupplierId", supplier);
                                supplierCmd.Parameters.AddWithValue("@ItemId", itemId);
                                await supplierCmd.ExecuteNonQueryAsync();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> UpdateProductAsync(int id, string barcode, string itemName, string category, string brand, string motorBrand, string size, int quantity, decimal purchasePrice, decimal sellingPrice)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_barcode} = @Barcode, {col_item_name} = @ItemName, {col_motor_brand} = @MotorBrand, {col_brand} = @Brand, {col_purchase_price} = @PurchasePrice, {col_selling_price} = @SellingPrice, {col_quantity} = @Quantity, {col_category} = @Category, {col_size} = @Size WHERE {col_id} = @ID";

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
                        cmd.Parameters.AddWithValue("@PurchasePrice", purchasePrice);
                        cmd.Parameters.AddWithValue("@SellingPrice", sellingPrice);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Category", category);
                        cmd.Parameters.AddWithValue("@Size", size);
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

        public async Task<bool> isItemExists(string itemName, string brand, string motorBrand, string size)
        {
            string selectQuery = $"SELECT COUNT(*) FROM {tbl_name} WHERE " +
                $"{col_item_name} = @ItemName COLLATE SQL_Latin1_General_CP1_CI_AS AND " +
                $"{col_brand} = @Brand COLLATE SQL_Latin1_General_CP1_CI_AS AND " +
                $"{col_motor_brand} = @MotorBrand COLLATE SQL_Latin1_General_CP1_CI_AS AND " +
                $"{col_size} = @Size COLLATE SQL_Latin1_General_CP1_CI_AS";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
                        cmd.Parameters.AddWithValue("@Brand", brand);
                        cmd.Parameters.AddWithValue("@MotorBrand", motorBrand);
                        cmd.Parameters.AddWithValue("@Size", size);
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

        public async Task<bool> ArchiveProductAsync(int id)
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

        public async Task<(List<string>, List<string>, List<string>)> GetCategoryBrandMotorBrandValuesAsync()
        {
            List<string> uniqueValuesColumn1 = new List<string>();
            List<string> uniqueValuesColumn2 = new List<string>();
            List<string> uniqueValuesColumn3 = new List<string>();

            string selectQuery = $"SELECT DISTINCT {col_category}, {col_brand}, {col_motor_brand} FROM {tbl_name}";

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
                                string value3 = reader[col_motor_brand].ToString();

                                uniqueValuesColumn1.Add(value1);
                                uniqueValuesColumn2.Add(value2);
                                uniqueValuesColumn3.Add(value3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            uniqueValuesColumn1.Insert(0, "Uncategorized");
            uniqueValuesColumn2.Insert(0, "None");
            uniqueValuesColumn3.Insert(0, "None");

            return (uniqueValuesColumn1, uniqueValuesColumn2, uniqueValuesColumn3);
        }

        public async Task<List<string>> GetDistinctValuesAsync(string columnName, string defaultValue)
        {
            List<string> uniqueValues = new List<string>();

            string selectQuery = $"SELECT DISTINCT {columnName} FROM {tbl_name}";

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
                                string value = reader[columnName].ToString();
                                uniqueValues.Add(value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            if (!uniqueValues.Contains(defaultValue))
            {
                uniqueValues.Insert(0, defaultValue);
            }

            return uniqueValues;
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

        public async Task<List<Item>> GetSupplierItems(int supplierId)
        {
            List<Item> uniqueValuesColumn = new List<Item>();

            string selectQuery = $"SELECT i.{col_id}, i.{col_item_name}, i.{col_brand}, i.{col_motor_brand}, i.{col_purchase_price}, i.{col_selling_price}, i.{col_quantity}, i.{col_category}, i.{col_size} FROM {tbl_name} i INNER JOIN tbl_supplier_items si ON i.{col_id} = si.item_id WHERE si.supplier_id = @supplierId AND i.{col_is_active} = '1'";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@supplierId", supplierId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal(col_id));
                                string itemName = reader.GetString(reader.GetOrdinal(col_item_name));
                                string category = reader.GetString(reader.GetOrdinal(col_category));
                                string brand = reader.GetString(reader.GetOrdinal(col_brand));
                                string motorBrand = reader.GetString(reader.GetOrdinal(col_motor_brand));
                                string size = reader.GetString(reader.GetOrdinal(col_size));
                                int quantity = reader.GetInt32(reader.GetOrdinal(col_quantity));
                                decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal(col_purchase_price));
                                decimal sellingPrice = reader.GetDecimal(reader.GetOrdinal(col_selling_price));

                                Item item = new Item
                                {
                                    Id = id,
                                    ItemName = itemName,
                                    Category = category,
                                    Brand = brand,
                                    MotorBrand = motorBrand,
                                    Size = size,
                                    Quantity = quantity,
                                    PurchasePrice = purchasePrice,
                                    SellingPrice = sellingPrice
                                };

                                uniqueValuesColumn.Add(item);
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

        public async Task<List<Item>> GetNonSupplierItems(int supplierId)
        {
            List<Item> uniqueValuesColumn = new List<Item>();

            string selectQuery = $"SELECT i.{col_id}, i.{col_item_name}, i.{col_brand}, i.{col_motor_brand}, i.{col_purchase_price}, i.{col_selling_price}, i.{col_quantity}, i.{col_category}, i.{col_size} FROM {tbl_name} i WHERE i.{col_is_active} = '1' AND NOT EXISTS (SELECT 1 FROM tbl_supplier_items si WHERE i.{col_id} = si.item_id AND si.supplier_id = @supplierId)";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@supplierId", supplierId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal(col_id));
                                string itemName = reader.GetString(reader.GetOrdinal(col_item_name));
                                string category = reader.GetString(reader.GetOrdinal(col_category));
                                string brand = reader.GetString(reader.GetOrdinal(col_brand));
                                string motorBrand = reader.GetString(reader.GetOrdinal(col_motor_brand));
                                string size = reader.GetString(reader.GetOrdinal(col_size));
                                int quantity = reader.GetInt32(reader.GetOrdinal(col_quantity));
                                decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal(col_purchase_price));
                                decimal sellingPrice = reader.GetDecimal(reader.GetOrdinal(col_selling_price));

                                Item item = new Item
                                {
                                    Id = id,
                                    ItemName = itemName,
                                    Category = category,
                                    Brand = brand,
                                    MotorBrand = motorBrand,
                                    Size = size,
                                    Quantity = quantity,
                                    PurchasePrice = purchasePrice,
                                    SellingPrice = sellingPrice
                                };

                                uniqueValuesColumn.Add(item);
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

        public async Task<Supplier> GetSupplier(int itemId)
        {
            Supplier supplier = null;

            string selectQuery = @"
                                SELECT i.supplier_id, i.contact_name, i.contact_no
                                FROM tbl_suppliers i
                                INNER JOIN tbl_supplier_items si ON i.supplier_id = si.supplier_id
                                WHERE si.item_id = @item_id";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                int supplierId = (int)reader["supplier_id"];
                                string contactName = reader["contact_name"].ToString();
                                string contactNo = reader["contact_no"].ToString();

                                supplier = new Supplier
                                {
                                    SupplierID = supplierId,
                                    ContactName = contactName,
                                    ContactNo = contactNo,
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

        public async Task<bool> AddProductToSuppier(int supplierId, int itemId)
        {
            string insertSupplierItemQuery = $"INSERT INTO tbl_supplier_items (supplier_id, item_id) VALUES (@SupplierId, @ItemId)";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(insertSupplierItemQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        await cmd.ExecuteNonQueryAsync();

                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<Item> GetItemByBarcodeAsync(string barcode)
        {
            Item item = null;

            string query = $"SELECT * FROM {tbl_name} WHERE {col_barcode} = @barcode";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@barcode", barcode);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new Item
                            {
                                Id = reader.GetInt32(reader.GetOrdinal(col_id)),
                                ItemName = reader.GetString(reader.GetOrdinal(col_item_name)),
                                Brand = reader.GetString(reader.GetOrdinal(col_brand)),
                                Size = reader.GetString(reader.GetOrdinal(col_size)),
                                SellingPrice = reader.GetDecimal(reader.GetOrdinal(col_selling_price)),
                                Quantity = reader.GetInt32(reader.GetOrdinal(col_quantity))
                            };
                        }
                    }
                }
            }

            return item;
        }

        public async Task<(Supplier, Cart, string)> GetOrderDetails(int orderId)
        {
            Supplier supplier = null;
            Cart cart = new Cart();
            string orderedBy = null;

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    string sql = $"SELECT supplier_id, operated_by FROM tbl_purchase_order WHERE order_id = @order_id";
                    int supplierId;
                    int orderedById;
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                supplierId = (int)reader["supplier_id"];
                                orderedById = (int)reader["operated_by"];
                            }
                            else
                            {
                                throw new Exception("Order not found");
                            }
                        }
                    }

                    sql = $"SELECT * FROM tbl_suppliers WHERE supplier_id = @supplier_id";
                    using (SqlCommand cmdSupplier = new SqlCommand(sql, con))
                    {
                        cmdSupplier.Parameters.AddWithValue("@supplier_id", supplierId);
                        using (SqlDataReader supplierReader = await cmdSupplier.ExecuteReaderAsync())
                        {
                            if (await supplierReader.ReadAsync())
                            {
                                supplier = new Supplier
                                {
                                    SupplierID = (int)supplierReader["supplier_id"],
                                    ContactName = supplierReader["contact_name"].ToString(),
                                    ContactNo = supplierReader["contact_no"].ToString(),
                                    Address = supplierReader["address"].ToString()
                                };
                            }
                            else
                            {
                                throw new Exception("Supplier not found");
                            }
                        }
                    }

                    sql = $"SELECT username FROM tbl_user_credentials WHERE id = @ordered_by_id";
                    using (SqlCommand cmdUser = new SqlCommand(sql, con))
                    {
                        cmdUser.Parameters.AddWithValue("@ordered_by_id", orderedById);
                        orderedBy = (string)await cmdUser.ExecuteScalarAsync();
                    }

                    sql = $"SELECT * FROM tbl_purchase_order_details WHERE order_id = @order_id";
                    using (SqlCommand cmdOrderDetails = new SqlCommand(sql, con))
                    {
                        cmdOrderDetails.Parameters.AddWithValue("@order_id", orderId);
                        List<int> itemIds = new List<int>();
                        List<int> quantities = new List<int>();
                        List<decimal> totals = new List<decimal>();
                        List<int> receivedQuantities = new List<int>();
                        using (SqlDataReader reader = await cmdOrderDetails.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                itemIds.Add(reader["item_id"] == DBNull.Value ? 0 : (int)reader["item_id"]);
                                quantities.Add(reader["order_quantity"] == DBNull.Value ? 0 : (int)reader["order_quantity"]);
                                totals.Add(reader["total"] == DBNull.Value ? 0m : (decimal)reader["total"]);
                                receivedQuantities.Add(reader["received_quantity"] == DBNull.Value ? 0 : (int)reader["received_quantity"]);
                            }
                        }

                        for (int i = 0; i < itemIds.Count; i++)
                        {
                            sql = $"SELECT * FROM {tbl_name} WHERE {col_id} = @item_id";
                            using (SqlCommand cmdItem = new SqlCommand(sql, con))
                            {
                                cmdItem.Parameters.AddWithValue("@item_id", itemIds[i]);
                                using (SqlDataReader itemReader = await cmdItem.ExecuteReaderAsync())
                                {
                                    if (await itemReader.ReadAsync())
                                    {
                                        PurchaseItem purchaseItem = new PurchaseItem
                                        {
                                            Id = (int)itemReader[col_id],
                                            ItemName = itemReader[col_item_name].ToString(),
                                            Brand = itemReader[col_brand].ToString(),
                                            Size = itemReader[col_size].ToString(),
                                            Quantity = quantities[i],
                                            PurchasePrice = totals[i] / quantities[i],
                                            ReceivedQuantity = receivedQuantities[i],
                                        };
                                        cart.Items.Add(purchaseItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Return the Supplier and Cart
            return (supplier, cart, orderedBy);
        }

    }
}
