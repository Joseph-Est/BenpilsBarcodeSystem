using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Repository
{
    internal class SettingsRepository
    {
        private readonly Database.DatabaseConnection databaseConnection;

        public SettingsRepository()
        {
            databaseConnection = new Database.DatabaseConnection();
        }
    }
}
