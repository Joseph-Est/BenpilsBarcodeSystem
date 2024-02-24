using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class UserCredentialsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;
        private string tbl_name = "tbl_user_credentials";
        private string col_id = "id", col_first_name = "first_name", col_last_name = "last_name", col_username = "username", col_password = "password", col_designation = "designation",
                       col_address = "address", col_contact_no = "contact_no", col_is_active = "is_active";

        public UserCredentialsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }

        public async Task<DataTable> GetUserCredentialsAsync()
        {
            string selectQuery = $"SELECT * FROM {tbl_name} WHERE {col_is_active} = '1'";

            try
            {
                using (SqlConnection con = databaseConnection.OpenConnection())
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
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
    }
}
