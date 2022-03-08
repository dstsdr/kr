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
    public partial class bank : Form
    {
        public bank()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void bank_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Банк ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];            
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string sql = "insert into Банк(name,korr_shet,BIK,kodPodrazdel,city,street,homeNumber ) Values" +
                " ('" + namebox.Text + "','" + korschetbox.Text + "','" + bikbox.Text + "','" + podrazdelbox.Text + "','" + citybox.Text + "','" + streetbox.Text + "','" + numberbox.Text +"')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            Connection.Open();
            if (textBox1.Text == "")
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Банк ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }   
            else
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("select * from Банк where name like'" + textBox1.Text + "' or korr_shet like '" + textBox1.Text + "' or BIK like '" + textBox1.Text + "' or kodPodrazdel like '" + textBox1.Text + "' or city like '" + textBox1.Text + "' or street like '" + textBox1.Text + "';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
           
        }
    }
}
