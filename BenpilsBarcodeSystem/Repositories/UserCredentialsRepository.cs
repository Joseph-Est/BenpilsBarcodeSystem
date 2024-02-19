using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class UserCredentialsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public UserCredentialsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }
    }
}
