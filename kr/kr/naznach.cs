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
    public partial class naznach : Form
    {
        public naznach()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void naznach_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_naznacheniya AS [№], name AS [вид] FROM [Целевое назначение кредита]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Connection.Open();
            string sql = "insert into [Целевое назначение кредита](name) Values ('" + namebox.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_naznacheniya AS [№], name AS [вид] FROM [Целевое назначение кредита]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            else
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select id_naznacheniya AS [№], name AS [Назначение] from [Целевое назначение кредита] where id_naznacheniya like'" + textBox1.Text + "' or name like '" + textBox1.Text + "';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
        }
    }
}
