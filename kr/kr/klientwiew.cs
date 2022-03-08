using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kr
{
    public partial class klientwiew : Form
    {
        public klientwiew()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void klientwiew_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Сотрудники ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            adapter = new SQLiteDataAdapter("SELECT * FROM Должность ", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }
    }
}
