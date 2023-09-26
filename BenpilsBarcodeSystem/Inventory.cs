using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales();
            pos.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchase = new Purchasing();
            purchase.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports rp = new Reports();
            rp.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport sreport = new StatisticReport();
            sreport.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserCredentials Uc = new UserCredentials();
            Uc.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.Show();
            this.Close();
        }
    }
}
