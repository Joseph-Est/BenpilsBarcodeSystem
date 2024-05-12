using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace BenpilsBarcodeSystem.Repository
{
    internal class POSRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        public static string tbl_transactions = "tbl_transactions", tbl_transaction_details = "tbl_transaction_details";
        public static string col_transaction_id = "transaction_id", col_transaction_date = "transaction_date", col_operated_by = "operated_by", col_payment_received = "payment_received";
        public static string col_id = "id", col_item_id = "item_id", col_quantity = "Quantity", col_total = "total", col_status = "status", col_discount = "discount";
        public static string transaction_completed = "COMPLETED", transaction_refunded = "REFUNDED";

        public POSRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<bool> InsertTransactionAsync(string transactionId, Cart cart, decimal payment, decimal discount = 0)
        {
            string insertOrderQuery = $"INSERT INTO {tbl_transactions} ({col_transaction_id}, {col_operated_by}, {col_payment_received}, {col_discount}) VALUES (@transactionId, @operatedBy, @paymentReceived, @discount)";
            string insertOrderDetailsQuery = $"INSERT INTO {tbl_transaction_details} ({col_transaction_id}, {col_item_id}, {col_quantity}, {col_total}, {col_discount}) VALUES (@transactionId, @ItemId, @Quantity, @total, @discount)";
            string getItemQuantityQuery = $"SELECT {InventoryRepository.col_quantity} FROM {InventoryRepository.tbl_name} WHERE {InventoryRepository.col_id} = @ItemId";
            string updateItemQuantityQuery = $"UPDATE tbl_item_master_data SET Quantity = Quantity - @Quantity WHERE id = @ItemId";

            int totalCartItemCount = cart.GetTotalItemCount();

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    ReportsRepository repository = new ReportsRepository();

                    using (transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@transactionId", transactionId);
                            cmd.Parameters.AddWithValue("@operatedBy", CurrentUser.User.ID);
                            cmd.Parameters.AddWithValue("@paymentReceived", payment);
                            cmd.Parameters.AddWithValue("@discount", discount);

                            await cmd.ExecuteNonQueryAsync();

                            cmd.CommandText = insertOrderDetailsQuery;

                            int itemCount = 0;
                            

                            foreach (var item in cart.Items)
                            {
                                itemCount += item.Quantity;

                                decimal itemTotal = item.SellingPrice * item.Quantity;
                                decimal itemDiscount = (itemTotal / cart.GetTotalPrice()) * discount;

                                using (SqlCommand cmdDetails = new SqlCommand(insertOrderDetailsQuery, con, transaction))
                                {
                                    cmdDetails.Parameters.AddWithValue("@transactionId", transactionId);
                                    cmdDetails.Parameters.AddWithValue("@ItemId", item.Id);
                                    cmdDetails.Parameters.AddWithValue("@Quantity", item.Quantity);
                                    cmdDetails.Parameters.AddWithValue("@total", item.SellingPrice * item.Quantity);
                                    cmdDetails.Parameters.AddWithValue("@discount", itemDiscount);

                                    await cmdDetails.ExecuteNonQueryAsync();
                                }

                                int oldStock;
                                int newStock;

                                using (SqlCommand cmdGetItemQuantity = new SqlCommand(getItemQuantityQuery, con, transaction))
                                {
                                    cmdGetItemQuantity.Parameters.AddWithValue("@ItemId", item.Id);
                                    oldStock = Convert.ToInt32(await cmdGetItemQuantity.ExecuteScalarAsync());
                                }

                                newStock = oldStock - item.Quantity;

                                if (newStock < 0)
                                {
                                    throw new Exception("New stock quantity cannot be negative.");
                                }

                                using (SqlCommand cmdUpdateItemQuantity = new SqlCommand(updateItemQuantityQuery, con, transaction))
                                {
                                    cmdUpdateItemQuantity.Parameters.AddWithValue("@ItemId", item.Id);
                                    cmdUpdateItemQuantity.Parameters.AddWithValue("@Quantity", item.Quantity);

                                    await cmdUpdateItemQuantity.ExecuteNonQueryAsync();
                                }

                                bool reportAdded = await repository.AddInventoryReportAsync(transaction, item.Id, null, $"Item Sold", item.Quantity, oldStock, newStock, CurrentUser.User.ID, $"Transaction no. {transactionId}");

                                if (!reportAdded)
                                {
                                    throw new Exception("Failed to add inventory report");
                                }
                            }

                            bool auditTrailAdded = await repository.AddAuditTrailAsyncTransaction(transaction, CurrentUser.User.ID, "POS Transaction", $"User has sold {itemCount} item(s). Transaction no. {transactionId}.");

                            if (!auditTrailAdded)
                            {
                                throw new Exception("Failed to add audit trail");
                            }
                        }

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<(Cart, decimal paymentReceived, decimal discount, string salesPerson, string transactionDate)> GetSalesDetailsAsync(string transactionId)
        {
            string selectTransactionQuery = $"SELECT {col_operated_by}, {col_payment_received}, {col_discount}, {col_transaction_date} FROM {tbl_transactions} WHERE {col_transaction_id} = @transactionId";
            string selectOrderDetailsQuery = $"SELECT {col_item_id}, {col_quantity}, {col_total} FROM {tbl_transaction_details} WHERE {col_transaction_id} = @transactionId";
            string selectSalesPersonQuery = $"SELECT CONCAT({UserCredentialsRepository.col_first_name}, ' ', {UserCredentialsRepository.col_last_name}) FROM {UserCredentialsRepository.tbl_name} WHERE {UserCredentialsRepository.col_id} = @userId";

            Cart cart = new Cart();
            decimal discount = 0;
            decimal paymentReceived = 0;
            string salesPerson = "";
            string transactionDate = "";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    // Retrieve transaction details
                    using (SqlCommand cmdTransaction = new SqlCommand(selectTransactionQuery, con))
                    {
                        cmdTransaction.Parameters.AddWithValue("@transactionId", transactionId);
                        SqlDataReader reader = await cmdTransaction.ExecuteReaderAsync();

                        if (reader.Read())
                        {
                            // Get paymentReceived and salesPersonId
                            DateTime date = reader.GetDateTime(3);
                            transactionDate = Util.ConvertDateShort(date);
                            discount = reader.GetDecimal(2);
                            paymentReceived = reader.GetDecimal(1);
                            int salesPersonId = reader.GetInt32(0);

                            reader.Close();

                            // Retrieve salesPerson
                            using (SqlCommand cmdSalesPerson = new SqlCommand(selectSalesPersonQuery, con))
                            {
                                cmdSalesPerson.Parameters.AddWithValue("@userId", salesPersonId);
                                salesPerson = (string)await cmdSalesPerson.ExecuteScalarAsync();
                            }
                        }

                        reader.Close();
                    }

                    // Lists to store item details
                    List<int> itemIds = new List<int>();
                    List<int> quantities = new List<int>();
                    List<decimal> totals = new List<decimal>();

                    // Retrieve order details and store in arrays
                    using (SqlCommand cmdOrderDetails = new SqlCommand(selectOrderDetailsQuery, con))
                    {
                        cmdOrderDetails.Parameters.AddWithValue("@transactionId", transactionId);
                        using (SqlDataReader reader = await cmdOrderDetails.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                // Add items details to arrays
                                itemIds.Add(reader.GetInt32(0));
                                quantities.Add(reader.GetInt32(1));
                                totals.Add(reader.GetDecimal(2));
                            }
                        }
                    }

                    for (int i = 0; i < itemIds.Count; i++)
                    {
                        // Retrieve item details using itemId
                        string selectItemQuery = $"SELECT * FROM {InventoryRepository.tbl_name} WHERE {InventoryRepository.col_id} = @itemId";

                        using (SqlCommand cmdItem = new SqlCommand(selectItemQuery, con))
                        {
                            cmdItem.Parameters.AddWithValue("@itemId", itemIds[i]);
                            using (SqlDataReader itemReader = await cmdItem.ExecuteReaderAsync())
                            {
                                if (itemReader.Read())
                                {
                                    PurchaseItem purchaseItem = new PurchaseItem
                                    {
                                        Id = itemIds[i],
                                        ItemName = itemReader.GetString(itemReader.GetOrdinal(InventoryRepository.col_item_name)),
                                        Brand = itemReader.GetString(itemReader.GetOrdinal(InventoryRepository.col_brand)),
                                        Size = itemReader.GetString(itemReader.GetOrdinal(InventoryRepository.col_size)),
                                        Quantity = quantities[i],
                                        SellingPrice = totals[i] / quantities[i],
                                    };

                                    // Add item to the cart
                                    cart.Items.Add(purchaseItem);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            

            return (cart, paymentReceived, discount, salesPerson, transactionDate);
        }

        public async Task<List<SalesData>> GetSalesAsync(DateTime dateFrom, DateTime dateTo, bool getAll = false)
        {
            List<SalesData> salesDataList = new List<SalesData>();

            string selectQuery = $@"
                SELECT 
                    t.{col_transaction_date}, 
                    SUM(td.{col_quantity}) AS TotalItemSold, 
                    SUM(td.{col_total} - td.{col_discount}) AS TotalSales,
                    imd.{InventoryRepository.col_purchase_price} as PurchasePrice,
                    imd.{InventoryRepository.col_item_name} AS ItemName,
                    imd.{InventoryRepository.col_brand} AS ItemBrand,
                    imd.{InventoryRepository.col_size} AS ItemSize,
                    imd.{InventoryRepository.col_category} AS Category
                FROM 
                    {tbl_transactions} t
                JOIN 
                    {tbl_transaction_details} td ON t.{col_transaction_id} = td.{col_transaction_id}
                JOIN 
                    {InventoryRepository.tbl_name} imd ON td.{col_item_id} = imd.{InventoryRepository.col_id}";

            if (!getAll)
            {
                selectQuery += $@" WHERE t.{col_transaction_date} BETWEEN @dateFrom AND @dateTo AND t.{col_status} = '{transaction_completed}'";
            }
            else
            {
                selectQuery += $@" WHERE t.{col_status} = '{transaction_completed}'";
            }

            selectQuery += $@"
                GROUP BY 
                    t.{col_transaction_date}, 
                    imd.{InventoryRepository.col_purchase_price},
                    imd.{InventoryRepository.col_item_name},
                    imd.{InventoryRepository.col_brand},
                    imd.{InventoryRepository.col_size},
                    imd.{InventoryRepository.col_category}
                ORDER BY 
                    t.{col_transaction_date} DESC";

            DateTime startDateWithTime = dateFrom.Date.Add(new TimeSpan(00, 00, 00));
            DateTime endDateWithTime = dateTo.Date.Add(new TimeSpan(23, 59, 59));

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@dateFrom", startDateWithTime.ToString("s"));
                        cmd.Parameters.AddWithValue("@dateTo", endDateWithTime.ToString("s"));

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DateTime transactionDate = reader.GetDateTime(reader.GetOrdinal(col_transaction_date));
                                int totalItemSold = reader.GetInt32(reader.GetOrdinal("TotalItemSold"));
                                decimal totalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales"));
                                decimal purchasePrice = reader.GetDecimal(reader.GetOrdinal("PurchasePrice"));
                                string itemName = reader.GetString(reader.GetOrdinal("ItemName"));
                                string itemBrand = reader.GetString(reader.GetOrdinal("itemBrand"));
                                string itemSize = reader.GetString(reader.GetOrdinal("itemSize"));
                                string category = reader.GetString(reader.GetOrdinal("Category"));

                                SalesData salesData = new SalesData
                                {
                                    Date = transactionDate,
                                    TotalItemSold = totalItemSold,
                                    TotalSales = totalSales,
                                    TotalProfit = totalItemSold * ((totalSales/totalItemSold) - purchasePrice),
                                    ItemName = itemName,
                                    Brand = itemBrand,
                                    Size = itemSize,
                                    Category = category
                                };

                                salesDataList.Add(salesData);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return salesDataList;
        }

        public async Task<DataTable> GetTransactionsAsync(string searchText = "")
        {
            string whereClause = $"WHERE {col_status} = '{transaction_completed}'";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND ({col_transaction_id} LIKE '%' + @SearchTxt + '%')";
            }

            string selectQuery = $@"
                SELECT {col_transaction_id}, {col_transaction_date} 
                FROM {tbl_transactions} 
                {whereClause} 
                ORDER BY {col_transaction_date} DESC";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTxt", searchText);

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("formatted_transaction_date", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime transactionDate = Convert.ToDateTime(row[POSRepository.col_transaction_date]);

                            row["formatted_transaction_date"] = Util.ConvertDateLongWithTime(transactionDate);
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

        public Task<DataTable> GetSalesChartDataAsync(List<SalesData> salesDataList, string timeFrame = null, string interval = null)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("SalesAmount", typeof(decimal));

            if (salesDataList.Count > 0)
            {
                var groupedData = salesDataList.GroupBy(d =>
                {
                    if (timeFrame == "today")
                    {
                        return d.Date.ToString("h tt");
                    }
                    else if(timeFrame == "week")
                    {
                        return d.Date.ToString("dd-MM-yy");
                    }else if(timeFrame == "month")
                    {
                        return d.Date.ToString("dd-MM-yy");
                    }
                    else
                    {
                        return interval == "month" ? d.Date.ToString("MM-yy") : d.Date.ToString("dd-MM-yy");
                    }
                })
                .Select(g => new
                {
                    Date = timeFrame == "today" ? g.Key : timeFrame == "year" && interval == "month" ? DateTime.ParseExact(g.Key, "MM-yy", CultureInfo.InvariantCulture).ToString("MMM") : DateTime.ParseExact(g.Key, "dd-MM-yy", CultureInfo.InvariantCulture).ToString("MMM-dd"),
                    SalesAmount = g.Sum(s => s.TotalSales)
                })
                .OrderBy(g =>
                {
                    if (timeFrame == "today" && DateTime.TryParseExact(g.Date, "h tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        return result;
                    }
                    else if (timeFrame == "year" && interval == "month" && DateTime.TryParseExact(g.Date, "MMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                    else if (DateTime.TryParseExact(g.Date, "MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    {
                        return result;
                    }
                    else
                    {
                        throw new FormatException($"Invalid date format for the given time frame: {timeFrame}");
                    }

                });

                foreach (var item in groupedData)
                {
                    dt.Rows.Add(item.Date, item.SalesAmount);
                }
            }

            return Task.FromResult(dt);
        }

        //public Task<DataTable> GetSalesChartDataAsync(List<SalesData> salesDataList, string timeFrame = null)
        //{

        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("Date", typeof(string));
        //    dt.Columns.Add("SalesAmount", typeof(decimal));

        //    if (salesDataList.Count > 0)
        //    {
        //        var groupedData = salesDataList.GroupBy(d =>
        //        {
        //            if (timeFrame == "today")
        //            {
        //                return d.Date.ToString("h tt");
        //            }
        //            else if (timeFrame == "week")
        //            {
        //                return d.Date.ToString("dd-MM-yy");
        //            }
        //            else if (timeFrame == "month")
        //            {
        //                return d.Date.ToString("MM-yy");
        //            }
        //            else
        //            {
        //                return d.Date.ToString("yy");
        //            }
        //        })
        //        .Select(g => new
        //        {
        //            Date = timeFrame == "today" ? g.Key : timeFrame == "week" ? DateTime.ParseExact(g.Key, "dd-MM-yy", CultureInfo.InvariantCulture).ToString("MMM-dd") :
        //                   timeFrame == "month" ? DateTime.ParseExact(g.Key, "MM-yy", CultureInfo.InvariantCulture).ToString("MMM") : DateTime.ParseExact(g.Key, "yy", CultureInfo.InvariantCulture).ToString("yyyy"),
        //            SalesAmount = g.Sum(s => s.TotalSales)
        //        })
        //        .OrderBy(g =>
        //        {
        //            if (timeFrame == "today" && DateTime.TryParseExact(g.Date, "h tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        //            {
        //                return result;
        //            }
        //            else if (timeFrame == "week" && DateTime.TryParseExact(g.Date, "MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        //            {
        //                return result;
        //            }
        //            else if (timeFrame == "month" && DateTime.TryParseExact(g.Date, "MMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        //            {
        //                return result;
        //            }
        //            else if (DateTime.TryParseExact(g.Date, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        //            {
        //                return result;
        //            }
        //            else
        //            {
        //                throw new FormatException($"Invalid date format for the given time frame: {timeFrame}");
        //            }
        //        });

        //        foreach (var item in groupedData)
        //        {
        //            dt.Rows.Add(item.Date, item.SalesAmount);
        //        }
        //    }

        //    return Task.FromResult(dt);
        //}

        public Task<DataTable> GetTopSellingItems(List<SalesData> salesDataList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DisplayItemName", typeof(string)); 
            dt.Columns.Add("TotalItemSold", typeof(int));

            var groupedSalesData = salesDataList.GroupBy(s => s.DisplayItemName)
                                                 .Select(g => new
                                                 {
                                                     DisplayItemName = g.Key,  
                                                     TotalItemSold = g.Sum(s => s.TotalItemSold)
                                                 })
                                                 .OrderByDescending(g => g.TotalItemSold)
                                                 .Take(5);

            foreach (var item in groupedSalesData)
            {
                dt.Rows.Add($"({item.TotalItemSold}) {item.DisplayItemName}", item.TotalItemSold); 
            }

            return Task.FromResult(dt);
        }

        public async Task<int> RefundTransactionAsync(string transactionId, Cart cart)
        {
            string updateTransactionQuery = $"UPDATE {tbl_transactions} SET {col_status} = @status WHERE {col_transaction_id} = @transactionId";
            string getItemQuantityQuery = $"SELECT {InventoryRepository.col_quantity} FROM {InventoryRepository.tbl_name} WHERE {InventoryRepository.col_id} = @ItemId AND {InventoryRepository.col_is_active} = 1";
            string updateItemQuantityQuery = $"UPDATE {InventoryRepository.tbl_name} SET quantity = quantity + @Quantity WHERE id = @ItemId";

            SqlTransaction transaction = null;

            int returnInt = 0;

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    ReportsRepository repository = new ReportsRepository();

                    using (transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(updateTransactionQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@status", transaction_refunded);
                            cmd.Parameters.AddWithValue("@transactionId", transactionId);
                            await cmd.ExecuteNonQueryAsync();

                            int itemCount = 0;

                            foreach (var item in cart.Items)
                            {
                                itemCount += item.Quantity;

                                int oldStock;
                                int newStock;

                                using (SqlCommand cmdGetItemQuantity = new SqlCommand(getItemQuantityQuery, con, transaction))
                                {
                                    cmdGetItemQuantity.Parameters.AddWithValue("@ItemId", item.Id);
                                    object result = await cmdGetItemQuantity.ExecuteScalarAsync();
                                    if (result == null)
                                    {
                                        returnInt = 1;
                                        throw new Exception($"Item with ID {item.Id} is archived and cannot be refunded.");
                                    }
                                    oldStock = Convert.ToInt32(result);
                                }

                                newStock = oldStock + item.Quantity;

                                if (newStock < 0)
                                {
                                    returnInt = 2;
                                    throw new Exception("New stock quantity cannot be negative.");
                                }

                                using (SqlCommand cmdUpdateItemQuantity = new SqlCommand(updateItemQuantityQuery, con, transaction))
                                {
                                    cmdUpdateItemQuantity.Parameters.AddWithValue("@ItemId", item.Id);
                                    cmdUpdateItemQuantity.Parameters.AddWithValue("@Quantity", item.Quantity);

                                    await cmdUpdateItemQuantity.ExecuteNonQueryAsync();
                                }

                                bool reportAdded = await repository.AddInventoryReportAsync(transaction, item.Id, null, $"Item Refunded", item.Quantity, oldStock, newStock, CurrentUser.User.ID, $"Transaction no. {transactionId}");

                                if (!reportAdded)
                                {
                                    returnInt = 3;
                                    throw new Exception("Failed to add inventory report");
                                }
                            }

                            bool auditTrailAdded = await repository.AddAuditTrailAsyncTransaction(transaction, CurrentUser.User.ID, "POS Transaction", $"User has refunded {itemCount} item(s). Transaction no. {transactionId}.");

                            if (!auditTrailAdded)
                            {
                                returnInt = 4;
                                throw new Exception("Failed to add audit trail");
                            }
                        }

                        transaction.Commit();
                    }
                }
                returnInt = 0;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine("An error occurred: " + ex.Message);
                returnInt = returnInt == 0 ? 5 : returnInt;
            }

            return returnInt;
        }


    }
}
