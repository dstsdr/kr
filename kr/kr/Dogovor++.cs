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
    public partial class Dogovor__ : Form
    {
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");
        public Dogovor__()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           Connection.Open();
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT INN, LastName FROM Клиенты";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["INN"].ToString() + " " + dr["LastName"]);
            }
            string sql = "insert into Договор(id_group,id_sotrudnik,INN_klienta, id_naznach, id_vida, data_zakluch, srok_pogasheniya, vuplata, id_stavki, cost_naznach, Neustoyka) Values" +
                " ('" + seriya.Text + "','" + nomer.Text + "','" + LastName.Text + "','" + FirstName.Text + "','" + otchestvo.Text + "','" + phone.Text + "','" + INN.Text + "','" + dohod.Text + "','" + schet.Text + "','" + job.Text + "','" + stash.Text + "','" + street.Text + "','" + city.Text + "','"
                + home.Text + "','" + ssuda.Text + "','" + podrazdel.Text + "','" + kem.Text + "','" + where.Text + "','" + date + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Dogovor___Load(object sender, EventArgs e)
        {
           // Connection.Close();
            Connection.Open();  
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT INN, LastName FROM Клиенты";
            cmd.ExecuteNonQuery();  
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);    
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["INN"].ToString()+ " " + dr["LastName"]);
            }
            SQLiteCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT name FROM [Целевое назначение кредита]";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox2.Items.Add(dr2["name"].ToString());
            }
            SQLiteCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT name FROM [Вид кредита]";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SQLiteDataAdapter da3 = new SQLiteDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                comboBox3.Items.Add(dr3["name"].ToString());
            }
            SQLiteCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT percent FROM [Процентная ставка]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SQLiteDataAdapter da4 = new SQLiteDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                comboBox4.Items.Add(dr4["percent"].ToString());
            }
            SQLiteCommand cmd5 = Connection.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT name FROM [Группа риска]";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SQLiteDataAdapter da5 = new SQLiteDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                comboBox5.Items.Add(dr5["name"]);
            }
            SQLiteCommand cmd6 = Connection.CreateCommand();
            cmd6.CommandType = CommandType.Text;
            cmd6.CommandText = "SELECT Сотрудники.id, Сотрудники.LastName, Должность.name  FROM Должность INNER JOIN Сотрудники ON Должность.id_dolznosti = Сотрудники.id";

            cmd6.ExecuteNonQuery();
            DataTable dt6 = new DataTable();
            SQLiteDataAdapter da6 = new SQLiteDataAdapter(cmd6);
            da6.Fill(dt6);
            foreach (DataRow dr6 in dt6.Rows)
            {
                comboBox6.Items.Add(dr6["id"].ToString()+ " " + dr6["LastName"] + " " + dr6["name"]);
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.Text)
            {
                case "0": sotridnik frm2 = new sotridnik(); frm2.Show(); break;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "0": klient frm2 = new klient(); frm2.Show(); break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
            //    case "0": сделать ++ через месседж бокс break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.Text)
            {
                //    case "0": сделать ++ через месседж бокс break;
            }
        }
    }
}
