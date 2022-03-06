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


namespace kr
{
    public partial class grafik : Form
    {
        public grafik()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void grafik_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT kalendar.id_dogovora AS [№ договора],kalendar.date_plan  AS [Запланировання дата], kalendar.date_fact AS [Фактическа дата],  kalendar.summ AS [Сумма] FROM kalendar", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }
    }
}
