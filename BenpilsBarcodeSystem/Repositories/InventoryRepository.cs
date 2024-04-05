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
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Utils;

namespace BenpilsBarcodeSystem.Repository
{
    internal class InventoryRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        public static string tbl_name = "tbl_item_master_data";
        public static string col_id = "id", col_barcode = "barcode", col_item_name = "item_name", col_motor_brand = "motor_brand", 
                       col_brand = "brand", col_purchase_price = "purchase_price", col_selling_price = "selling_price", col_quantity = "Quantity", col_category = "category", col_size = "size", col_is_active = "is_active", col_date_created = "date_created", col_date_updated = "date_updated";

        private int lowStockThreshold = 20;
        private int highStockThreshold = 100;

        public InventoryRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetProductsAsync(bool isActive, string searchText = "", string category = "All", string brand = "All", bool showNoStock = true)
        {
            string whereClause = $"{col_is_active} = '{isActive}'";

            if (!showNoStock)
            {
                whereClause += $" AND {col_quantity} > 0";

            }

            string selectQuery = $"SELECT {col_id}, {col_barcode}, {col_item_name}, {col_brand}, {col_motor_brand}, {col_purchase_price}, {col_selling_price}, {col_quantity}, {col_category}, {col_size}, {col_date_created} FROM {tbl_name} WHERE {whereClause}";


            if (string.IsNullOrWhiteSpace(searchText) && category == "All" && brand == "All")
            {
               
            }
            else
            {
                selectQuery = $"SELECT {col_id}, {col_barcode}, {col_item_name}, {col_brand}, {col_motor_brand}, {col_purchase_price}, {col_selling_price}, {col_quantity}, {col_category}, {col_size}, {col_date_created} FROM {tbl_name} WHERE {whereClause}";

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
                            string insertSupplierItemQuery = $"INSERT INTO {SuppliersRepository.tbl_supplier_items} ({SuppliersRepository.col_id}, {SuppliersRepository.col_item_id}) VALUES (@SupplierId, @ItemId)";
                            using (SqlCommand supplierCmd = new SqlCommand(insertSupplierItemQuery, con, transaction))
                            {
                                supplierCmd.Parameters.AddWithValue("@SupplierId", supplier);
                                supplierCmd.Parameters.AddWithValue("@ItemId", itemId);
                                await supplierCmd.ExecuteNonQueryAsync();
                            }
                        }

                        ReportsRepository repository = new ReportsRepository();

                        bool reportAdded = await repository.AddInventoryReportAsync(transaction, itemId, null, "Add Item", quantity, 0, quantity, CurrentUser.User.ID, "Item added succesfully");

                        if (!reportAdded)
                        {
                            throw new Exception("Failed to add inventory report");
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

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
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

                        //if (rowsAffected > 0)
                        //{
                        //    ReportsRepository repository = new ReportsRepository();
                        //    bool reportAdded = await repository.AddInventoryReportAsync(transaction, id, null, "Update Product", Quantity, modifiedBy, "Product updated successfully");

                        //    if (!reportAdded)
                        //    {
                        //        throw new Exception("Failed to add inventory report");
                        //    }
                        //}

                        transaction.Commit();
                        return rowsAffected > 0;
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

        public async Task<bool> IsItemExists(string itemName, string brand, string motorBrand, string size)
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

        public async Task<bool> ArchiveProductAsync(int id, bool archive = true)
        {
            string updateQuery = $"UPDATE {tbl_name} SET {col_is_active} = '{!archive}' WHERE {col_id} = @ID";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            ReportsRepository repository = new ReportsRepository();
                            bool reportAdded = await repository.AddInventoryReportAsync(transaction, id, null, archive == true ? "Archive Item" : "Restore Item", 0, 0, 0, CurrentUser.User.ID, archive == true ? "Item archived succesfully" : "Item restored succesfully");

                            if (!reportAdded)
                            {
                                throw new Exception("Failed to add inventory report");
                            }
                        }

                        transaction.Commit();
                        return rowsAffected > 0;
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

        public async Task<(List<string>, List<string>)> GetCategoryBrandValuesAsync()
        {
            List<string> uniqueValuesColumn1 = new List<string>();
            List<string> uniqueValuesColumn2 = new List<string>();

            string selectQuery = $"SELECT {col_category}, {col_brand} FROM {tbl_name} WHERE {col_category} NOT IN ('All') AND {col_brand} NOT IN ('All') AND {col_is_active} = 'true'";

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
                                string value1 = reader[col_category].ToString().Trim();
                                string value2 = reader[col_brand].ToString().Trim();

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

            uniqueValuesColumn1 = uniqueValuesColumn1.Distinct().ToList();
            uniqueValuesColumn2 = uniqueValuesColumn2.Distinct().ToList();

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

            string selectQuery = $"SELECT DISTINCT {columnName} FROM {tbl_name} WHERE {columnName} NOT IN ('{defaultValue}')";

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

            uniqueValues.Insert(0, defaultValue);

            return uniqueValues;
        }

        public async Task<bool> DeductStockAsync(int id, int amountToDeduct, string remarks)
        {
            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    string selectQuery = $"SELECT {col_quantity} FROM {tbl_name} WHERE {col_id} = @Id";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, con, transaction);
                    selectCmd.Parameters.AddWithValue("@Id", id);
                    int oldStock = (int)await selectCmd.ExecuteScalarAsync();

                    string updateQuery = $"UPDATE {tbl_name} SET {col_quantity} = {col_quantity} - @AmountToDeduct WHERE {col_id} = @Id";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con, transaction);
                    updateCmd.Parameters.AddWithValue("@AmountToDeduct", amountToDeduct);
                    updateCmd.Parameters.AddWithValue("@Id", id);
                    int rowsAffected = await updateCmd.ExecuteNonQueryAsync();

                    int newStock = oldStock - amountToDeduct;

                    if (rowsAffected > 0)
                    {
                        ReportsRepository repository = new ReportsRepository();
                        bool reportAdded = await repository.AddInventoryReportAsync(transaction, id, null, "Reduce Stock", amountToDeduct, oldStock, newStock, CurrentUser.User.ID, remarks);

                        if (!reportAdded)
                        {
                            throw new Exception("Failed to add inventory report");
                        }
                    }

                    transaction.Commit();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<List<Item>> GetSupplierItems(int supplierId)
        {
            List<Item> uniqueValuesColumn = new List<Item>();

            string selectQuery = $"SELECT i.{col_id}, i.{col_item_name}, i.{col_brand}, i.{col_motor_brand}, i.{col_purchase_price}, i.{col_selling_price}, i.{col_quantity}, i.{col_category}, i.{col_size} FROM {tbl_name} i INNER JOIN {SuppliersRepository.tbl_supplier_items} si ON i.{col_id} = si.{SuppliersRepository.col_item_id} WHERE si.{SuppliersRepository.col_id} = @supplierId AND i.{col_is_active} = '1'";

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

            string selectQuery = $"SELECT i.{col_id}, i.{col_item_name}, i.{col_brand}, i.{col_motor_brand}, i.{col_purchase_price}, i.{col_selling_price}, i.{col_quantity}, i.{col_category}, i.{col_size} FROM {tbl_name} i WHERE i.{col_is_active} = '1' AND NOT EXISTS (SELECT 1 FROM {SuppliersRepository.tbl_supplier_items} si WHERE i.{col_id} = si.{SuppliersRepository.col_item_id} AND si.{SuppliersRepository.col_id} = @supplierId)";

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

        public async Task<bool> AddProductToSuppier(int supplierId, int itemId)
        {
            string insertSupplierItemQuery = $"INSERT INTO {SuppliersRepository.tbl_supplier_items} ({SuppliersRepository.col_id}, {SuppliersRepository.col_item_id}) VALUES (@SupplierId, @ItemId)";

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

        public async Task<Item> GetItemByIDAsync(int id)
        {
            Item item = null;

            string query = $"SELECT * FROM {tbl_name} WHERE {col_id} = @id";

            using (SqlConnection con = databaseConnection.OpenConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);

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

        public async Task<int> GetItemAcount(bool isActive = true)
        {
            int count = 0;
            string countQuery;

            if (isActive)
            {
                countQuery = $"SELECT COUNT({col_id}) FROM {tbl_name} WHERE {col_is_active} = 'true'";
            }
            else
            {
                countQuery = $"SELECT COUNT({col_id}) FROM {tbl_name}";
            }

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

        public async Task<List<Item>> GetLowStockItemsAsync()
        {
            List<Item> lowStockItems = new List<Item>();

            string selectQuery = $"SELECT {col_barcode}, {col_item_name}, {col_brand}, {col_quantity}, {col_size} FROM {tbl_name} WHERE {col_quantity} < {lowStockThreshold} AND {col_is_active} = 'true' ORDER BY {col_quantity} DESC";

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
                                string barcode = reader.GetString(reader.GetOrdinal(col_barcode));
                                string itemName = reader.GetString(reader.GetOrdinal(col_item_name));
                                string brand = reader.GetString(reader.GetOrdinal(col_brand));
                                string size = reader.GetString(reader.GetOrdinal(col_size));
                                int quantity = reader.GetInt32(reader.GetOrdinal(col_quantity));

                                Item item = new Item
                                {
                                    Barcode = barcode,
                                    ItemName = itemName,
                                    Brand = brand,
                                    Size = size,
                                    Quantity = quantity,
                                };

                                lowStockItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return lowStockItems;
        }

        public async Task<bool> HasPendingOrdersAsync(int itemId)
        {
            string query = $"SELECT COUNT(*) FROM {PurchaseOrderRepository.tbl_purchase_order} po " +
                           $"INNER JOIN {PurchaseOrderRepository.tbl_purchase_order_details} pod ON po.{PurchaseOrderRepository.col_order_id} = pod.{PurchaseOrderRepository.col_order_id} " +
                           $"WHERE pod.{PurchaseOrderRepository.col_item_id} = @ItemId AND po.{PurchaseOrderRepository.col_status} = @Status";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ItemId", itemId);
                        cmd.Parameters.AddWithValue("@Status", PurchaseOrderRepository.pending_status);

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

        public async Task<int> GetCategoryCount()
        {
            int count = 0;
            string countQuery = $"SELECT COUNT(DISTINCT {col_category}) FROM {tbl_name}";

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

        public async Task<DataTable> GetItemCountByCategoryAsync()
        {
            DataTable itemCounts = new DataTable();
            itemCounts.Columns.Add("Category", typeof(string));
            itemCounts.Columns.Add("Count", typeof(int));

            string countQuery = $@"
                SELECT 
                    CASE 
                        WHEN {col_category} = 'N/A' THEN 'Uncategorized'
                        ELSE {col_category}
                    END as Category,
                    COUNT(*) as Count
                FROM {tbl_name} 
                GROUP BY Category
                ORDER BY Count DESC";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(countQuery, con))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int count = Convert.ToInt32(reader[1]);
                                string category = $"{count} {reader[0]}";
                                itemCounts.Rows.Add(category, count);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return itemCounts;
        }

        public async Task<DataTable> GetBrandPopularityAsync()
        {
            DataTable brandPopularity = new DataTable();
            brandPopularity.Columns.Add("Brand", typeof(string));
            brandPopularity.Columns.Add("ItemCount", typeof(int));

            string countQuery = $@"
                SELECT TOP 8 
                i.{col_brand}, SUM(td.{POSRepository.col_quantity}) 
                FROM {POSRepository.tbl_transaction_details} td
                INNER JOIN {tbl_name} i ON td.{POSRepository.col_item_id} = i.{col_id}
                WHERE i.{col_brand} != 'N/A'
                GROUP BY i.{col_brand}
                ORDER BY SUM(td.{POSRepository.col_quantity}) DESC";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(countQuery, con))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int itemCount = Convert.ToInt32(reader[1]);
                                string brand = $"{reader[0]} ({itemCount}) ";
                                brandPopularity.Rows.Add(brand, itemCount);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return brandPopularity;
        }

        public async Task<decimal> GetTotalStockValue(bool useSellingPrice = true)
        {
            decimal totalStockValue = 0;
            string priceColumn = useSellingPrice ? col_selling_price : col_purchase_price;
            string query = $"SELECT SUM({col_quantity} * {priceColumn}) FROM {tbl_name}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        totalStockValue = (result != DBNull.Value) ? (decimal)result : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return totalStockValue;
        }

        public async Task<decimal> GetPotentialProfit()
        {
            decimal potentialProfit = 0;
            string query = $"SELECT SUM({col_quantity} * ({col_selling_price} - {col_purchase_price})) FROM {tbl_name}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        potentialProfit = (result != DBNull.Value) ? (decimal)result : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return potentialProfit;
        }

        public async Task<(List<SalesData> TopLowestProfitMarginItems, List<SalesData> TopHighestProfitMarginItems)> GetTopProfitMarginItemsAsync()
        {
            List<SalesData> topLowestProfitMarginItems = new List<SalesData>();
            List<SalesData> topHighestProfitMarginItems = new List<SalesData>();

            string queryLowestProfitMargin = $@"
                SELECT TOP 5 
                    {col_item_name},
                    {col_brand},
                    {col_size},
                    AVG(CASE WHEN {col_selling_price} = 0 THEN 0 ELSE ({col_selling_price} - {col_purchase_price}) / {col_selling_price} END) AS AvgProfitMargin
                FROM 
                    {tbl_name}
                WHERE
                    {col_is_active} = 'true'
                GROUP BY
                    {col_item_name},
                    {col_brand},
                    {col_size}
                ORDER BY 
                    AvgProfitMargin ASC";

            string queryHighestProfitMargin = $@"
                SELECT TOP 5 
                    {col_item_name},
                    {col_brand},
                    {col_size},
                    AVG(CASE WHEN {col_selling_price} = 0 THEN 0 ELSE ({col_selling_price} - {col_purchase_price}) / {col_selling_price} END) AS AvgProfitMargin
                FROM 
                    {tbl_name}
                WHERE
                    {col_is_active} = 'true'
                GROUP BY
                    {col_item_name},
                    {col_brand},
                    {col_size}
                ORDER BY 
                    AvgProfitMargin DESC";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(queryLowestProfitMargin, con))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                SalesData item = new SalesData
                                {
                                    ItemName = reader[col_item_name].ToString(),
                                    Brand = reader[col_brand].ToString(),
                                    Size = reader[col_size].ToString(),
                                    AvgProfitMargin = Convert.ToDecimal(reader["AvgProfitMargin"])
                                };
                                topLowestProfitMarginItems.Add(item);
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(queryHighestProfitMargin, con))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                SalesData item = new SalesData
                                {
                                    ItemName = reader[col_item_name].ToString(),
                                    Brand = reader[col_brand].ToString(),
                                    Size = reader[col_size].ToString(),
                                    AvgProfitMargin = Convert.ToDecimal(reader["AvgProfitMargin"])
                                };
                                topHighestProfitMarginItems.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return (topLowestProfitMarginItems, topHighestProfitMarginItems);
        }

        public Task<DataTable> GetSalesByCategoryAsync(List<SalesData> salesDataList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("TotalSales", typeof(decimal));

            if (salesDataList.Count > 0)
            {
                var groupedData = salesDataList.GroupBy(d => d.Category)
                    .Select(g => new
                    {
                        Category = g.Key,
                        TotalSales = g.Sum(s => s.TotalSales)
                    })
                    .OrderByDescending(g => g.TotalSales);

                foreach (var item in groupedData)
                {
                    dt.Rows.Add(item.Category, item.TotalSales);
                }
            }

            return Task.FromResult(dt);
        }

        public Task<DataTable> GetSalesByBrandAsync(List<SalesData> salesDataList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Brand", typeof(string));
            dt.Columns.Add("TotalSales", typeof(decimal));

            if (salesDataList.Count > 0)
            {
                var groupedData = salesDataList.GroupBy(d => d.Brand)
                    .Select(g => new
                    {
                        Brand = g.Key == "N/A" ? "Unbranded" : g.Key,
                        TotalSales = g.Sum(s => s.TotalSales)
                    })
                    .OrderByDescending(g => g.TotalSales)
                    .Take(8); // Limit the results to the top 10 categories

                foreach (var item in groupedData)
                {
                    dt.Rows.Add(item.Brand, item.TotalSales);
                }
            }

            return Task.FromResult(dt);
        }

        public async Task<DataTable> GetInventoryExportDT()
        {
            DataTable dt = await GetProductsAsync(true);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("formatted_purchase_price");
            dt.Columns.Remove("formatted_selling_price");
            dt.Columns.Remove("status");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {col_id, "Product ID"},
                    {col_barcode, "Barcode"},
                    {col_item_name, "Item"},
                    {col_brand, "Brand"},
                    {col_motor_brand, "Motor Brand"},
                    {col_category, "Category"},
                    {col_size, "Size"},
                    {col_date_created, "Date Created"},
                    {col_purchase_price, "Purchase Price"},
                    {col_selling_price, "Selling Price"},
                    {col_quantity, "Quantity"}
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Product ID",
                    "Date Created",
                    "Barcode",
                    "Category",
                    "Item",
                    "Brand",
                    "Motor Brand",
                    "Size",
                    "Purchase Price",
                    "Selling Price",
                    "Quantity"
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

    }
}
