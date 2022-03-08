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
    public partial class statistika : Form
    {
        public statistika()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) dogovor();       
                  
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dogovor ()
        {
            if (comboBox2.SelectedIndex == 0)
            {
               // var months = DateTime.Today; //ToString("dd'/'MM'/'yyyy");
                //months = months.AddMonths(-1);
                string months = "08/02/2022";
                Connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT SUM(Договор.cost_naznach), Договор.data_zakluch, count(Договор.data_zakluch) as [Количество заказов] FROM Договор WHERE data_zakluch>'" + months + "' group by Договор.data_zakluch", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today; //ToString("dd'/'MM'/'yyyy");
                months = months.AddYears(-1);
                Connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT SUM(Договор.cost_naznach), Договор.data_zakluch, count(Договор.data_zakluch) as [Количество заказов] FROM Договор WHERE data_zakluch>'" + months.ToString("dd'/'MM'/'yyyy") + "' group by Договор.data_zakluch", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT SUM(Договор.cost_naznach), Договор.data_zakluch, count(Договор.data_zakluch) as [Количество заказов] FROM Договор WHERE data_zakluch>'" + months.ToString("dd'/'MM'/'yyyy") + "' group by Договор.data_zakluch", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }           
           
        }

        private void statistika_Load(object sender, EventArgs e)
        {

        }
    }
}
