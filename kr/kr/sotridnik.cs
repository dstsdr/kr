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
    public partial class sotridnik : Form
    {
        public sotridnik()
        {
            InitializeComponent();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
	    SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");
        private void button1_Click(object sender, EventArgs e)
        {
            string Combo = comboBox2.Text;
            string[] words = Combo.Split(' ');
            string BIK = words[0];
            Combo = comboBox1.Text;
            words = Combo.Split(' ');
            string dolznost = words[0];
            Connection.Open();
            string sql = "insert into Сотрудники(seriya, nomer, LastName, firstName, otchestvo, BIK, id_dolznosti, phone) " + "Values ('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + BIK + "','" + dolznost + "','" + textBox6.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void sotridnik_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT BIK, name FROM Банк";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["BIK"].ToString() + " " + dr["name"].ToString());
            }
            SQLiteCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT * FROM Должность";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox1.Items.Add(dr2["id_dolznosti"].ToString()+ " " + dr2["name"].ToString());
            }

            Connection.Close();
        }
    }   //string sql = "insert into Банк(setiya,nomer,LastName, firstName, otchestvo, BIK, id_dolznosti, phone) Values ('"+
}
