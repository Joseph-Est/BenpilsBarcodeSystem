using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Repositories
{
    internal class PurchaseOrderRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public static string tbl_purchase_order = "tbl_purchase_order", tbl_purchase_order_details = "tbl_purchase_order_details";
        public static string col_order_id = "order_id", col_supplier_id = "supplier_id", col_order_date = "order_date", col_receiving_date = "receiving_date", col_status = "status", col_remarks = "remarks", col_is_backorder = "is_backorder",
            col_fulfillment_date = "fulfillment_date" ,col_order_details_id = "id", col_item_id = "item_id", col_order_quantity = "order_quantity", col_total = "total", 
            col_operated_by = "operated_by", col_fulfilled_by = "fulfilled_by", col_received_quantity = "received_quantity";
        public string pending_status = "PENDING", delivered_status = "DELIVERED", partially_delivered_status = "PARTIALLY DELIVERED";

        public string remarks_complete_delivery = "All items have been delivered as expected.";
        public string remarks_partially_delivered = "Some items have not been delivered yet.";

        public PurchaseOrderRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetPurchaseOrderTransactionsAsync()
        {
            string selectQuery = $"SELECT po.{col_order_id}, s.contact_name, po.{col_order_date}, po.{col_receiving_date} FROM {tbl_purchase_order} po INNER JOIN tbl_suppliers s ON po.{col_supplier_id} = s.supplier_id WHERE po.{col_status} = 'PENDING' AND po.{col_is_backorder} = 0";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        DataTable dt = new DataTable();
                        await Task.Run(() => adapter.Fill(dt));

                        dt.Columns.Add("status", typeof(string));
                        dt.Columns.Add("formatted_order_date", typeof(string));
                        dt.Columns.Add("formatted_receiving_date", typeof(string));

                        foreach (DataRow row in dt.Rows)
                        {
                            DateTime orderDate = Convert.ToDateTime(row[col_order_date]);
                            DateTime receivingDate = Convert.ToDateTime(row[col_receiving_date]);

                            if (DateTime.Today > receivingDate)
                            {
                                row["status"] = "Overdue";
                            }
                            else
                            {
                                row["status"] = "Pending";
                            }

                            row["formatted_order_date"] = Util.ConvertDate(orderDate);
                            row["formatted_receiving_date"] = Util.ConvertDate(receivingDate);
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

        public async Task<bool> InsertPurchaseOrderAsync(int orderID, Cart cart, Supplier supplier, DateTime orderDate, DateTime receivingDate)
        {
            string insertOrderQuery = $"INSERT INTO {tbl_purchase_order} ({col_order_id}, {col_supplier_id}, {col_order_date}, {col_receiving_date}, {col_operated_by}) VALUES (@orderId, @supplierId, @orderDate, @receivingDate, @operatedBy)";
            string insertOrderDetailsQuery = $"INSERT INTO {tbl_purchase_order_details} ({col_order_id}, {col_item_id}, {col_order_quantity}, {col_total}) VALUES (@orderId, @itemId, @quantity, @total)";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@orderId", orderID);
                            cmd.Parameters.AddWithValue("@supplierId", supplier.SupplierID);
                            cmd.Parameters.AddWithValue("@orderDate", orderDate);
                            cmd.Parameters.AddWithValue("@receivingDate", receivingDate);
                            cmd.Parameters.AddWithValue("@operatedBy", CurrentUser.User.iD);

                            await cmd.ExecuteNonQueryAsync(); 

                            cmd.CommandText = insertOrderDetailsQuery;

                            foreach (var item in cart.Items)
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@orderId", orderID);
                                cmd.Parameters.AddWithValue("@itemId", item.Id);
                                cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@total", item.PurchasePrice * item.Quantity);

                                await cmd.ExecuteNonQueryAsync(); 
                            }
                        }

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }

        public async Task<(Supplier, Cart, Dictionary<string, object>)> GetOrderDetails(int orderId)
        {
            Supplier supplier = null;
            Cart cart = new Cart();
            string orderedBy = null;
            string status = null;
            string remarks = null;
            string dateFulfilled = null;
            string fulfilledBy = null;
            bool isBackorder = false;

            Dictionary<string, object> orderDetails = new Dictionary<string, object>
            {
                { "OrderedBy", orderedBy },
                { "Status", status },
                { "Remarks", remarks },
                { "DateFulfilled", dateFulfilled },
                { "FulfilledBy", fulfilledBy },
                { "IsBackorder", isBackorder }
            };

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    string sql = $"SELECT * FROM {tbl_purchase_order} WHERE {col_order_id} = @order_id";
                    int supplierId;
                    int orderedById;
                    int fulfilledById;

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@order_id", orderId);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                supplierId = (int)reader[SuppliersRepository.col_id];
                                orderedById = (int)reader[col_operated_by];
                                fulfilledById = reader[col_fulfilled_by] is DBNull ? 0 : (int)reader[col_fulfilled_by];
                                status = (string)reader[col_status];
                                remarks = reader[col_remarks] is DBNull ? null : (string)reader[col_remarks];
                                isBackorder = (bool)reader[col_is_backorder];
                                dateFulfilled = reader[col_fulfillment_date] is DBNull ? null : (string)reader[col_fulfillment_date];
                            }
                            else
                            {
                                throw new Exception("Order not found");
                            }
                        }
                    }

                    sql = $"SELECT * FROM {SuppliersRepository.tbl_name} WHERE {SuppliersRepository.col_id} = @supplier_id";
                    using (SqlCommand cmdSupplier = new SqlCommand(sql, con))
                    {
                        cmdSupplier.Parameters.AddWithValue("@supplier_id", supplierId);
                        using (SqlDataReader supplierReader = await cmdSupplier.ExecuteReaderAsync())
                        {
                            if (await supplierReader.ReadAsync())
                            {
                                supplier = new Supplier
                                {
                                    SupplierID = (int)supplierReader[SuppliersRepository.col_id],
                                    ContactName = supplierReader[SuppliersRepository.col_contact_name].ToString(),
                                    ContactNo = supplierReader[SuppliersRepository.col_contact_no].ToString(),
                                    Address = supplierReader[SuppliersRepository.col_address].ToString()
                                };
                            }
                            else
                            {
                                throw new Exception("Supplier not found");
                            }
                        }
                    }

                    string[] users = new string[2];
                    int[] userIds = { orderedById, fulfilledById }; 

                    for (int i = 0; i < 2; i++)
                    {
                        if (userIds[i] > 0)
                        {
                            sql = $"SELECT {UserCredentialsRepository.col_username} FROM {UserCredentialsRepository.tbl_name} WHERE {UserCredentialsRepository.col_id} = @id";
                            using (SqlCommand cmdUser = new SqlCommand(sql, con))
                            {
                                cmdUser.Parameters.AddWithValue("@id", userIds[i]);
                                users[i] = (string)await cmdUser.ExecuteScalarAsync();
                            }
                        }
                        else
                        {
                            users[i] = null; 
                        }
                    }

                    orderedBy = users[0];
                    fulfilledBy = users[1];

                    sql = $"SELECT * FROM {tbl_purchase_order_details} WHERE {col_order_id} = @order_id";
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
                                itemIds.Add(reader[col_item_id] == DBNull.Value ? 0 : (int)reader[col_item_id]);
                                quantities.Add(reader[col_order_quantity] == DBNull.Value ? 0 : (int)reader[col_order_quantity]);
                                totals.Add(reader[col_total] == DBNull.Value ? 0m : (decimal)reader[col_total]);
                                receivedQuantities.Add(reader[col_received_quantity] == DBNull.Value ? 0 : (int)reader[col_received_quantity]);
                            }
                        }

                        for (int i = 0; i < itemIds.Count; i++)
                        {
                            sql = $"SELECT * FROM {InventoryRepository.tbl_name} WHERE {InventoryRepository.col_id} = @item_id";
                            using (SqlCommand cmdItem = new SqlCommand(sql, con))
                            {
                                cmdItem.Parameters.AddWithValue("@item_id", itemIds[i]);
                                using (SqlDataReader itemReader = await cmdItem.ExecuteReaderAsync())
                                {
                                    if (await itemReader.ReadAsync())
                                    {
                                        PurchaseItem purchaseItem = new PurchaseItem
                                        {
                                            Id = (int)itemReader[InventoryRepository.col_id],
                                            ItemName = itemReader[InventoryRepository.col_item_name].ToString(),
                                            Brand = itemReader[InventoryRepository.col_brand].ToString(),
                                            Size = itemReader[InventoryRepository.col_size].ToString(),
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

            orderDetails["OrderedBy"] = orderedBy;
            orderDetails["Status"] = status;
            orderDetails["Remarks"] = remarks;
            orderDetails["DateFulfilled"] = dateFulfilled;
            orderDetails["FulfilledBy"] = fulfilledBy;
            orderDetails["IsBackorder"] = isBackorder;

            return (supplier, cart, orderDetails);
        }

        public async Task<bool> CompletePurchaseOrderAsync(int orderId, int fulfilledBy, string status, string remarks, Cart cart)
        {
            string updatePurchaseOrderQuery = $"UPDATE {tbl_purchase_order} SET {col_fulfillment_date} = @fulfillmentDate, {col_fulfilled_by} = @fulfilledBy, {col_remarks} = @remarks, {col_status} = @status WHERE {col_order_id} = @orderId";
            string updatePurchaseOrderDetailsQuery = $"UPDATE {tbl_purchase_order_details} SET {col_received_quantity} = @receivedQuantity WHERE {col_order_id} = @orderId AND {col_item_id} = @itemId";
            string updateItemMasterDataQuery = $"UPDATE {InventoryRepository.tbl_name} SET {InventoryRepository.col_quantity} = {InventoryRepository.col_quantity} + @receivedQuantity WHERE {InventoryRepository.col_id} = @itemId";

            SqlTransaction transaction = null;

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(updatePurchaseOrderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@fulfillmentDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@fulfilledBy", fulfilledBy);
                            cmd.Parameters.AddWithValue("@remarks", remarks);
                            cmd.Parameters.AddWithValue("@status", status);
                            cmd.Parameters.AddWithValue("@orderId", orderId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        foreach (var item in cart.Items)
                        {
                            using (SqlCommand cmd = new SqlCommand(updatePurchaseOrderDetailsQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@receivedQuantity", item.ReceivedQuantity);
                                cmd.Parameters.AddWithValue("@orderId", orderId);
                                cmd.Parameters.AddWithValue("@itemId", item.Id);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            using (SqlCommand cmd = new SqlCommand(updateItemMasterDataQuery, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@receivedQuantity", item.ReceivedQuantity);
                                cmd.Parameters.AddWithValue("@itemId", item.Id);
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        transaction.Commit();
                        return true;  
                    }
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;  
            }
        }
    }
}
