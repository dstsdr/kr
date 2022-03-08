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
    public partial class percent : Form
    {
        public percent()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void percent_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_stavki AS [№], percent AS [%] FROM [Процентная ставка] ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string sql = "insert into [Процентная ставка](percent) Values ('" + namebox.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT id_stavki AS [№], percent AS [%] FROM [Процентная ставка]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            else
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select id_stavki AS [№], percent AS [%] from [Процентная ставка] where id_stavki like '" + textBox1.Text + "'% or percent like '" + textBox1.Text + "';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
        }
    }
}
