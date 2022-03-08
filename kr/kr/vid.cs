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
    public partial class vid : Form
    {
        public vid()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void vid_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_vida AS [№], name AS [вид] FROM [Вид кредита]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string sql = "insert into [Вид кредита](name) Values ('" + namebox.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_vida AS [№], name AS [вид] FROM [Вид кредита]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            else
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select id_vida AS [№], name AS [вид] from [Вид кредита] where id_vida AS [№] like'" + textBox1.Text + "' or name  AS [вид] like '" + textBox1.Text + "';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void namebox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
