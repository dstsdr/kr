using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace kr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        SQLiteConnection Connection = new SQLiteConnection("Data Source=kredit.db");

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("Select * From klient", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }
    }
}
