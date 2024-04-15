using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Entities
{
    internal class BackupSettings
    {
        public bool BackupInventory { get; set; }
        public bool BackupSuppliers { get; set; }
        public bool BackupPurchaseOrder { get; set; }
        public bool BackupSalesTransactions { get; set; }
        public bool BackupInventoryReport { get; set; }
        public bool BackupAuditTrail { get; set; }

        public BackupInterval Interval { get; set; }
        public string IntervalValue { get; set; }

        public bool UseActiveHours { get; set; }
        public TimeSpan ActiveStartTime { get; set; }
        public TimeSpan ActiveEndTime { get; set; }
        public bool IsEnabled { get; set; }
        public string SaveLocation { get; set; }
    }

    public enum BackupInterval
    {
        Hourly,
        EveryXHours,
        Daily,
        Weekly,
        Monthly
    }
}
