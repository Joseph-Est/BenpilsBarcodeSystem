using BenpilsBarcodeSystem.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class POSRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_transactions = "tbl_transactions", tbl_transaction_details = "tbl_transaction_details";
        private string col_transaction_id = "transaction_id", col_transaction_date = "transaction_date", col_operated_by = "operated_by", col_payment_received = "payment_received";
        private string col_id = "id", col_item_id = "item_id", col_quantity = "quantity", col_total = "total";

        public POSRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<bool> InsertTransactionAsync(string transactionId, Cart cart, decimal payment)
        {
            string insertOrderQuery = $"INSERT INTO {tbl_transactions} ({col_transaction_id}, {col_transaction_date}, {col_operated_by}, {col_payment_received}) VALUES (@transactionId, @transactionDate, @operatedBy, @paymentReceived)";
            string insertOrderDetailsQuery = $"INSERT INTO {tbl_transaction_details} ({col_transaction_id}, {col_item_id}, {col_quantity}, {col_total}) VALUES (@transactionId, @itemId, @quantity, @total)";
            string updateItemQuantityQuery = $"UPDATE tbl_item_master_data SET quantity = quantity - @quantity WHERE id = @itemId";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@transactionId", transactionId);
                            cmd.Parameters.AddWithValue("@transactionDate", DateTime.Today);
                            cmd.Parameters.AddWithValue("@operatedBy", CurrentUser.User.iD);
                            cmd.Parameters.AddWithValue("@paymentReceived", payment);

                            await cmd.ExecuteNonQueryAsync();

                            cmd.CommandText = insertOrderDetailsQuery;

                            foreach (var item in cart.Items)
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@transactionId", transactionId);
                                cmd.Parameters.AddWithValue("@itemId", item.Id);
                                cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                                cmd.Parameters.AddWithValue("@total", item.SellingPrice * item.Quantity);

                                await cmd.ExecuteNonQueryAsync();

                                cmd.CommandText = updateItemQuantityQuery;
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@itemId", item.Id);
                                cmd.Parameters.AddWithValue("@quantity", item.Quantity);

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
