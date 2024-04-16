using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Repository
{
    internal class ReportsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public static string tbl_inventory_report = "tbl_inventory_report", tbl_audit_trail = "tbl_audit_trail", tbl_item_modifications = "tbl_item_modifications";
        public static string col_id = "id", col_item_id = "item_id", col_purchase_order_id = "purchase_order_id", col_date = "date", col_action = "action", col_quantity = "Quantity",
                             col_modified_by = "modified_by", col_remarks = "remarks", col_old_stock = "old_stock", col_new_stock = "new_stock";
        public static string col_user_id = "user_id", col_details = "details";
        public static string col_modification_id = "modification_id", col_field_modified = "field_modified", col_old_value = "old_value", col_new_value = "new_value"; 

        public ReportsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        //INVENTORY REPORT

        public async Task<bool> AddInventoryReportAsync(SqlTransaction transaction, int itemId, int? purchaseOrderId, string action, int quantity, int oldStock, int newStock, int modifiedBy, string remarks)
        {
            string insertQuery = $"INSERT INTO {tbl_inventory_report} ({col_item_id}, {col_purchase_order_id}, {col_action}, {col_quantity}, {col_modified_by}, {col_old_stock}, {col_new_stock}, {col_remarks}) " +
                                 "VALUES (@ItemId, @PurchaseOrderId, @Action, @Quantity, @ModifiedBy, @OldStock, @NewStock,@Remarks)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, transaction.Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@ItemId", itemId);
                    cmd.Parameters.AddWithValue("@PurchaseOrderId", (object)purchaseOrderId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("@OldStock", oldStock);
                    cmd.Parameters.AddWithValue("@NewStock", newStock);
                    cmd.Parameters.AddWithValue("@Remarks", remarks);

                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<DataTable> GetInventoryReportsAsync(DateTime startDate, DateTime endDate, string searchText = "", int pageNumber = 1, int pageSize = 30)
        {
            string whereClause = $"WHERE ir.{col_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (im.{InventoryRepository.col_barcode} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_item_name} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_brand} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_size} LIKE '%' + @SearchTxt + '%' OR 
                                ir.{col_action} LIKE '%' + @SearchTxt + '%')";
            }

            int skip = (pageNumber - 1) * pageSize;

            string selectQuery = $@"
                SELECT 
                    ir.{col_id},
                    ir.{col_item_id},
                    im.{InventoryRepository.col_barcode},
                    CASE
                        WHEN im.{InventoryRepository.col_brand} = 'N/A' AND im.{InventoryRepository.col_size} = 'N/A' THEN im.{InventoryRepository.col_item_name}
                        WHEN im.{InventoryRepository.col_brand} = 'N/A' THEN im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_size}
                        WHEN im.{InventoryRepository.col_size} = 'N/A' THEN im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_brand}
                        ELSE im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_brand} + ', ' + im.{InventoryRepository.col_size}
                    END AS item,
                    ir.{col_purchase_order_id},
                    ir.{col_date},
                    ir.{col_action},
                    ir.{col_quantity},
                    uc.{UserCredentialsRepository.col_username} AS {col_modified_by},
                    ir.{col_remarks},
                    ir.{col_old_stock},
                    ir.{col_new_stock}
                FROM {tbl_inventory_report} ir
                JOIN {InventoryRepository.tbl_name} im ON ir.{col_item_id} = im.{InventoryRepository.col_id}
                JOIN {UserCredentialsRepository.tbl_name} uc ON ir.{col_modified_by} = uc.{UserCredentialsRepository.col_id}
                ORDER BY ir.{col_date} DESC
                OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTxt", searchText);

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("formatted_date", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime reportDate = Convert.ToDateTime(row[col_date]);
                            row["formatted_date"] = Util.ConvertDateLongWithTime(reportDate);
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

        public async Task<int> GetInventoryReportCountAsync(DateTime startDate, DateTime endDate, string searchText = "")
        {
            string whereClause = $"WHERE ir.{col_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (im.{InventoryRepository.col_barcode} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_item_name} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_brand} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_size} LIKE '%' + @SearchTxt + '%' OR 
                                ir.{col_action} LIKE '%' + @SearchTxt + '%')";
            }

            string countQuery = $@"SELECT COUNT(*) 
                                FROM {tbl_inventory_report} ir
                                JOIN {InventoryRepository.tbl_name} im ON ir.{col_item_id} = im.{InventoryRepository.col_id}
                               {whereClause}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand command = new SqlCommand(countQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        command.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@SearchTxt", searchText);

                        int count = (int)await command.ExecuteScalarAsync();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
        }

        public async Task<bool> AddModificationReportAsync(SqlTransaction transaction, int itemId, int modifiedBy ,string fieldModified, string oldValue, string newValue)
        {
            string insertQuery = $"INSERT INTO {tbl_item_modifications} ({col_item_id}, {col_modified_by}, {col_field_modified}, {col_old_value}, {col_new_value}) " +
                                 "VALUES (@ItemId, @ModifiedBy, @FieldModified, @OldValue, @NewValue)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, transaction.Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@ItemId", itemId);
                    cmd.Parameters.AddWithValue("@ModifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("@FieldModified", fieldModified);
                    cmd.Parameters.AddWithValue("@OldValue", oldValue);
                    cmd.Parameters.AddWithValue("@NewValue", newValue);

                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        //PURCHASE ORDER

        public async Task<DataTable> GetPurchaseOrderTransactionsAsync(DateTime startDate, DateTime endDate, string searchText = null, int pageNumber = 1, int pageSize = 30)
        {
            string whereClause = $"WHERE po.{PurchaseOrderRepository.col_order_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (po.{PurchaseOrderRepository.col_order_id} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%' OR 
                                s.{SuppliersRepository.col_contact_name} LIKE '%' + @SearchTxt + '%' OR 
                                po.{PurchaseOrderRepository.col_status} LIKE '%' + @SearchTxt + '%')";
            }


            int skip = (pageNumber - 1) * pageSize;

            string selectQuery = $"SELECT po.{PurchaseOrderRepository.col_order_id}, u.{UserCredentialsRepository.col_username} as operated_by, po.{PurchaseOrderRepository.col_backorder_from}, s.{SuppliersRepository.col_contact_name}, po.{PurchaseOrderRepository.col_order_date}, po.{PurchaseOrderRepository.col_receiving_date}, po.{PurchaseOrderRepository.col_status}, ISNULL(po.{PurchaseOrderRepository.col_remarks}, 'N/A') AS {PurchaseOrderRepository.col_remarks} FROM {PurchaseOrderRepository.tbl_purchase_order} po " +
                $"INNER JOIN {SuppliersRepository.tbl_name} s ON po.{PurchaseOrderRepository.col_supplier_id} = s.{SuppliersRepository.col_id} " +
                $"INNER JOIN {UserCredentialsRepository.tbl_name} u ON po.{PurchaseOrderRepository.col_operated_by} = u.{UserCredentialsRepository.col_id} " +
                $"{whereClause} " +
                $"ORDER BY po.{PurchaseOrderRepository.col_order_date} DESC " +
                $"OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTxt", searchText);

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("formatted_order_date", typeof(string));
                        dt.Columns.Add("formatted_receiving_date", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime orderDate = Convert.ToDateTime(row[PurchaseOrderRepository.col_order_date]);
                            DateTime receivingDate = Convert.ToDateTime(row[PurchaseOrderRepository.col_receiving_date]);

                            row["formatted_order_date"] = Util.ConvertDateLongWithTime(orderDate);
                            row["formatted_receiving_date"] = Util.ConvertDateLong(receivingDate);

                            string backorderFrom = row[PurchaseOrderRepository.col_backorder_from]?.ToString();

                            if (!string.IsNullOrEmpty(backorderFrom) && backorderFrom != "0")
                            {
                                string remarks = row["remarks"]?.ToString();
                                remarks = string.IsNullOrEmpty(remarks) ? backorderFrom : $"Backorder ({backorderFrom}). {remarks}";
                                row["remarks"] = remarks;
                            }
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

        public async Task<int> GetPurchaseOrderTransactionsCountAsync(DateTime startDate, DateTime endDate, string searchText = "")
        {
            string whereClause = $"WHERE po.{PurchaseOrderRepository.col_order_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (po.{PurchaseOrderRepository.col_order_id} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%' OR 
                                s.{SuppliersRepository.col_contact_name} LIKE '%' + @SearchTxt + '%' OR 
                                po.{PurchaseOrderRepository.col_status} LIKE '%' + @SearchTxt + '%')";
            }

            string countQuery = $@"SELECT COUNT(*) 
                                FROM {PurchaseOrderRepository.tbl_purchase_order} po 
                                INNER JOIN {SuppliersRepository.tbl_name} s ON po.{PurchaseOrderRepository.col_supplier_id} = s.{SuppliersRepository.col_id}
                                INNER JOIN {UserCredentialsRepository.tbl_name} u ON po.{PurchaseOrderRepository.col_operated_by} = u.{UserCredentialsRepository.col_id}
                               {whereClause}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand command = new SqlCommand(countQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        command.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@SearchTxt", searchText);

                        int count = (int)await command.ExecuteScalarAsync();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
        }


        public async Task<DataTable> GetPurchaseOrderTransactionsFullAsync(DateTime startDate, DateTime endDate)
        {
            string whereClause = $"WHERE po.{PurchaseOrderRepository.col_order_date} BETWEEN @StartDate AND @EndDate";

            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    whereClause += $@" AND (po.{PurchaseOrderRepository.col_order_id} LIKE '%' + @SearchTxt + '%' OR 
            //                u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%' OR 
            //                s.{SuppliersRepository.col_contact_name} LIKE '%' + @SearchTxt + '%' OR 
            //                po.{PurchaseOrderRepository.col_status} LIKE '%' + @SearchTxt + '%' OR
            //                imd.{InventoryRepository.col_item_name} LIKE '%' + @SearchTxt + '%' OR
            //                imd.{InventoryRepository.col_brand} LIKE '%' + @SearchTxt + '%' OR
            //                imd.{InventoryRepository.col_size} LIKE '%' + @SearchTxt + '%')";
            //}

            string selectQuery = $"SELECT po.{PurchaseOrderRepository.col_order_id}, u1.{UserCredentialsRepository.col_username} as operated_by,  u2.{UserCredentialsRepository.col_username} as fulfilled_by, po.{PurchaseOrderRepository.col_backorder_from}, s.{SuppliersRepository.col_contact_name}, po.{PurchaseOrderRepository.col_order_date}, po.{PurchaseOrderRepository.col_receiving_date}, po.{PurchaseOrderRepository.col_status}, ISNULL(po.{PurchaseOrderRepository.col_remarks}, 'N/A') AS {PurchaseOrderRepository.col_remarks}, pod.{PurchaseOrderRepository.col_item_id}, pod.{PurchaseOrderRepository.col_order_quantity}, pod.{PurchaseOrderRepository.col_total}, pod.{PurchaseOrderRepository.col_received_quantity}, imd.{InventoryRepository.col_item_name}, imd.{InventoryRepository.col_brand}, imd.{InventoryRepository.col_size} FROM {PurchaseOrderRepository.tbl_purchase_order} po " +
                $"INNER JOIN {SuppliersRepository.tbl_name} s ON po.{PurchaseOrderRepository.col_supplier_id} = s.{SuppliersRepository.col_id} " +
                $"INNER JOIN {UserCredentialsRepository.tbl_name} u1 ON po.{PurchaseOrderRepository.col_operated_by} = u1.{UserCredentialsRepository.col_id} " +
                $"INNER JOIN {UserCredentialsRepository.tbl_name} u2 ON po.{PurchaseOrderRepository.col_fulfilled_by} = u2.{UserCredentialsRepository.col_id} " +
                $"INNER JOIN {PurchaseOrderRepository.tbl_purchase_order_details} pod ON po.{PurchaseOrderRepository.col_order_id} = pod.{PurchaseOrderRepository.col_order_id} " +
                $"INNER JOIN {InventoryRepository.tbl_name} imd ON pod.{PurchaseOrderRepository.col_item_id} = imd.{InventoryRepository.col_id} " +
                $"{whereClause} ORDER BY po.{PurchaseOrderRepository.col_order_date} DESC";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        foreach (DataRow row in dt.Rows)
                        {
                            string backorderFrom = row[PurchaseOrderRepository.col_backorder_from]?.ToString();

                            if (!string.IsNullOrEmpty(backorderFrom) && backorderFrom != "0")
                            {
                                string remarks = row["remarks"]?.ToString();
                                remarks = string.IsNullOrEmpty(remarks) ? backorderFrom : $"Backorder ({backorderFrom}). {remarks}";
                                row["remarks"] = remarks;
                            }
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

        //SALES REPORT

        public async Task<DataTable> GetSalesAsync(DateTime startDate, DateTime endDate, string searchText = "", int pageNumber = 1, int pageSize = 30)
        {
            string whereClause = $"WHERE t.{POSRepository.col_transaction_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (t.{POSRepository.col_transaction_id} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_item_name} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%')";
            }

            int skip = (pageNumber - 1) * pageSize;

            string selectQuery = $@"SELECT t.{POSRepository.col_transaction_id}, u.{UserCredentialsRepository.col_username}, t.{POSRepository.col_payment_received}, t.{POSRepository.col_transaction_date}, 
                                CASE
                                    WHEN im.{InventoryRepository.col_brand} = 'N/A' AND im.{InventoryRepository.col_size} = 'N/A' THEN im.{InventoryRepository.col_item_name}
                                    WHEN im.{InventoryRepository.col_brand} = 'N/A' THEN im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_size}
                                    WHEN im.{InventoryRepository.col_size} = 'N/A' THEN im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_brand}
                                    ELSE im.{InventoryRepository.col_item_name} + ', ' + im.{InventoryRepository.col_brand} + ', ' + im.{InventoryRepository.col_size}
                                END AS item_name,
                                d.{POSRepository.col_quantity}, d.{POSRepository.col_total}
                                FROM {POSRepository.tbl_transactions} t 
                                INNER JOIN {UserCredentialsRepository.tbl_name} u ON t.{POSRepository.col_operated_by} = u.{UserCredentialsRepository.col_id} 
                                INNER JOIN {POSRepository.tbl_transaction_details} d ON t.{POSRepository.col_transaction_id} = d.{POSRepository.col_transaction_id} 
                                INNER JOIN {InventoryRepository.tbl_name} im ON d.{POSRepository.col_item_id} = im.{InventoryRepository.col_id}
                                {whereClause} 
                                ORDER BY t.{POSRepository.col_transaction_date} DESC
                                OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
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

        public async Task<int> GetSalesCountAsync(DateTime startDate, DateTime endDate, string searchText = "")
        {
            string whereClause = $"WHERE t.{POSRepository.col_transaction_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (t.{POSRepository.col_transaction_id} LIKE '%' + @SearchTxt + '%' OR 
                                im.{InventoryRepository.col_item_name} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%')";
            }

            string countQuery = $@"SELECT COUNT(*) 
                                FROM {POSRepository.tbl_transactions} t
                                INNER JOIN {UserCredentialsRepository.tbl_name} u ON t.{POSRepository.col_operated_by} = u.{UserCredentialsRepository.col_id} 
                                INNER JOIN {POSRepository.tbl_transaction_details} d ON t.{POSRepository.col_transaction_id} = d.{POSRepository.col_transaction_id} 
                                {whereClause}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand command = new SqlCommand(countQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        command.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@SearchTxt", searchText);

                        int count = (int)await command.ExecuteScalarAsync();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
        }


        //AUDIT TRAIL

        public async Task<bool> AddAuditTrailAsync(SqlConnection connection, int userId, string action, string details)
        {
            string insertQuery = $"INSERT INTO {tbl_audit_trail} ({col_user_id}, {col_action}, {col_details}) " +
                                 "VALUES (@UserId, @Action, @Details)";

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Action", action);
                    cmd.Parameters.AddWithValue("@Details", details);

                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<DataTable> GetAuditTrailAsync(DateTime startDate, DateTime endDate, string searchText = "", int pageNumber = 1, int pageSize = 30)
        {
            string whereClause = $"WHERE a.{col_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_first_name} LIKE '%' + @SearchTxt + '%' OR 
                                u.{UserCredentialsRepository.col_last_name} LIKE '%' + @SearchTxt + '%' OR 
                                a.{col_action} LIKE '%' + @SearchTxt + '%')";
            }

            int skip = (pageNumber - 1) * pageSize;

            string selectQuery = $@"SELECT a.{col_id}, u.{UserCredentialsRepository.col_username}, u.{UserCredentialsRepository.col_first_name}, 
                            u.{UserCredentialsRepository.col_last_name}, a.{col_action}, a.{col_date}, a.{col_details} 
                            FROM {tbl_audit_trail} a 
                            INNER JOIN {UserCredentialsRepository.tbl_name} u ON a.{col_user_id} = u.{UserCredentialsRepository.col_id} 
                            {whereClause} 
                            ORDER BY a.{col_date} DESC 
                            OFFSET {skip} ROWS FETCH NEXT {pageSize} ROWS ONLY";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        adapter.SelectCommand.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        adapter.SelectCommand.Parameters.AddWithValue("@SearchTxt", searchText);

                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("formatted_date", typeof(string));
                        dt.Columns.Add("name", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime date = Convert.ToDateTime(row[col_date]);

                            row["formatted_date"] = Util.ConvertDateLongWithTime(date);
                            row["name"] = $"{row[UserCredentialsRepository.col_first_name]} {row[UserCredentialsRepository.col_last_name]}";
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

        public async Task<int> GetAuditTrailCountAsync(DateTime startDate, DateTime endDate, string searchText = "")
        {
            string whereClause = $"WHERE a.{col_date} BETWEEN @StartDate AND @EndDate";

            if (!string.IsNullOrEmpty(searchText))
            {
                whereClause += $@" AND (u.{UserCredentialsRepository.col_username} LIKE '%' + @SearchTxt + '%' OR 
                            u.{UserCredentialsRepository.col_first_name} LIKE '%' + @SearchTxt + '%' OR 
                            u.{UserCredentialsRepository.col_last_name} LIKE '%' + @SearchTxt + '%' OR 
                            a.{col_action} LIKE '%' + @SearchTxt + '%')";
            }

            string countQuery = $@"SELECT COUNT(*) 
                           FROM {tbl_audit_trail} a 
                           INNER JOIN {UserCredentialsRepository.tbl_name} u ON a.{col_user_id} = u.{UserCredentialsRepository.col_id} 
                           {whereClause}";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlCommand command = new SqlCommand(countQuery, con))
                    {
                        DateTime startDateWithTime = startDate.Date.Add(new TimeSpan(00, 00, 00));
                        DateTime endDateWithTime = endDate.Date.Add(new TimeSpan(23, 59, 59));

                        command.Parameters.AddWithValue("@StartDate", startDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@EndDate", endDateWithTime.ToString("s"));
                        command.Parameters.AddWithValue("@SearchTxt", searchText);

                        int count = (int)await command.ExecuteScalarAsync();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return -1;
            }
        }

        //EXPORT

        public async Task<DataTable> GetInventoryReportExportDT(DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = await GetInventoryReportsAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove(col_item_id);
            dt.Columns.Remove(col_purchase_order_id);
            dt.Columns.Remove("formatted_date");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {InventoryRepository.col_barcode, "Barcode"},
                    {col_date, "Date"},
                    {col_action, "Action"},
                    {col_quantity, "Quantity"},
                    {col_modified_by, "Operated By"},
                    {col_old_stock, "Old Stock"},
                    {col_new_stock, "New Stock"},
                    {col_remarks, "Remarks"},
                    {"item", "Item"},
                    {"id", "Report ID"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Report ID",
                    "Date",
                    "Operated By",
                    "Action",
                    "Barcode",
                    "Item",
                    "Quantity",
                    "Old Stock",
                    "New Stock",
                    "Remarks",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        public async Task<DataTable> GetPurchaseOrderExportDT(DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = await GetPurchaseOrderTransactionsFullAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("backorder_from");
            dt.Columns.Remove("item_id");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {PurchaseOrderRepository.col_order_id, "Order ID"},
                    {PurchaseOrderRepository.col_operated_by, "Ordered By"},
                    {PurchaseOrderRepository.col_fulfilled_by, "Fulfilled By"},
                    {SuppliersRepository.col_contact_name, "Supplier"},
                    {PurchaseOrderRepository.col_order_date, "Order Date"},
                    {PurchaseOrderRepository.col_receiving_date, "Delivery Date"},
                    {PurchaseOrderRepository.col_status, "Status"},
                    {PurchaseOrderRepository.col_remarks, "Remarks"},
                    {InventoryRepository.col_item_name, "Item"},
                    {InventoryRepository.col_brand, "Brand"},
                    {InventoryRepository.col_size, "Size"},
                    {PurchaseOrderRepository.col_order_quantity, "Order Qty"},
                    {PurchaseOrderRepository.col_total, "Total"},
                    {PurchaseOrderRepository.col_received_quantity, "Received Qty"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Order ID",
                    "Order Date",
                    "Delivery Date",
                    "Ordered By",
                    "Supplier",
                    "Item",
                    "Brand",
                    "Size",
                    "Order Qty",
                    "Total",
                    "Received Qty",
                    "Status",
                    "Fulfilled By",
                    "Remarks",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        public async Task<DataTable> GetSalesTransactionsExportDT(DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = await GetSalesAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("formatted_transaction_date");
            dt.Columns.Remove(POSRepository.col_payment_received);

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {POSRepository.col_transaction_id, "Transaction ID"},
                    {UserCredentialsRepository.col_username, "Salesperson"},
                    {POSRepository.col_transaction_date, "Transaction Date"},
                    {"item_name", "Item"},
                    {POSRepository.col_quantity, "Quantity"},
                    {POSRepository.col_total, "Total"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Transaction ID",
                    "Transaction Date",
                    "Salesperson",
                    "Item",
                    "Quantity",
                    "Total",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }


        public async Task<DataTable> GetAuditTrailExportDT(DateTime dateFrom, DateTime dateTo)
        {
            DataTable dt = await GetAuditTrailAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("name");
            dt.Columns.Remove("formatted_date");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {UserCredentialsRepository.col_username, "Username"},
                    {UserCredentialsRepository.col_first_name, "First Name"},
                    {UserCredentialsRepository.col_last_name, "Last Name"},
                    {col_action, "Action"},
                    {col_date, "Date"},
                    {col_details, "Details"},
                    {"id", "ID"}
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Date",
                    "ID",
                    "Username",
                    "First Name",
                    "Last Name",
                    "Action",
                    "Details",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }
    }
}
