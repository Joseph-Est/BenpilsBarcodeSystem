using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class ReportsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public ReportsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }
    }
}
