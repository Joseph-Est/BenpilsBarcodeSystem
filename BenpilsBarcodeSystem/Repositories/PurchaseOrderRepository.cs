using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repositories
{
    internal class PurchaseOrderRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        private string tbl_purchase_order = "tbl_purchase_order", tbl_purchase_order_details = "tbl_purchase_order_details";
        private string col_order_id = "order_id", col_supplier_id = "supplier_id", col_order_date = "order_date", col_receiving_date = "receiving_date", col_status = "status", col_remarks = "remarks", col_is_backorder = "is_backorder",
            col_fulfillment_date = "fulfillment_date" ,col_order_details_id = "id", col_item_id = "item_id", col_order_quantity = "order_quantity", col_total = "total", 
            col_operated_by = "operated_by", col_fulfilled_by = "fulfilled_by", col_received_quantity = "received_quantity";

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
    }
}
