using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            string dolznost;
            string Combo = comboBox2.Text;
            string[] words = Combo.Split(' ');
            string BIK = words[0];
         /*   Combo = comboBox1.Text;
            words = Combo.Split(' ');
            string dolznost = words[0];*/
            Connection.Open();
            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT [№] FROM Должность WHERE Название='" + comboBox1.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            dolznost = dt2.Rows[0][0].ToString();            
            string sql = "insert into Сотрудники([Серия паспорта], [Номер паспорта], Фамилия, Имя, Отчество, БИК, [Должность], Телефон) " + "Values ('" + textBox4.Text + "','" + textBox5.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + BIK + "','" + dolznost + "','" + textBox6.Text + "')";
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                   case 0:  dolznost frm2 = new dolznost(); frm2.Show(); break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox2.SelectedIndex)
            {
                   case 0:  bank frm2 = new bank(); frm2.Show(); break;
            }
        }
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Сотрудники.[№], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество," +
                " Сотрудники.[Серия паспорта], Сотрудники.[Номер паспорта], Должность.Название AS Должность, Сотрудники.Телефон, " +
                "Банк.Название AS Банк FROM Банк" +
                " INNER JOIN(Должность INNER JOIN Сотрудники ON Должность.[№] = Сотрудники.Должность) ON Банк.БИК = Сотрудники.БИК", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label7.Text = "Количество записей " + rows.ToString();
        }
        private void sotridnik_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT БИК, Название FROM Банк";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["БИК"].ToString() + " " + dr["Название"].ToString());
            }
            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT Название FROM Должность";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                comboBox1.Items.Add(dr2["Название"].ToString());
            }

            Connection.Close();
            dataset();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (index != dataGridView1.Rows.Count - 2)
            {
                dataGridView1.Rows[index].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, index + 1];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (index != 0)
            {
                dataGridView1.Rows[index].Selected = true;
                dataGridView1.CurrentCell = dataGridView1[0, index - 1];
            }
        }

        private void button2_Click(object sender, EventArgs e) //поиск
        {           
                 SqlDataAdapter adapter = new SqlDataAdapter("SELECT Сотрудники.[№], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество," +
                 " Сотрудники.[Серия паспорта], Сотрудники.[Номер паспорта], Должность.Название AS Должность, Сотрудники.Телефон, " +
                 "Банк.Название AS Банк FROM Банк INNER JOIN(Должность INNER JOIN Сотрудники ON Должность.[№] = Сотрудники.Должность) " +
                 "ON Банк.БИК = Сотрудники.БИК where Сотрудники.[№] like '%" + textBox7.Text + "%' or Сотрудники.Фамилия like" +
                 " '%" + textBox7.Text + "%' or Сотрудники.Имя like '%" + textBox7.Text + "%' or Сотрудники.Отчество like '%" + textBox7.Text + "%'" +
                 " or Сотрудники.[Серия паспорта] like '%" + textBox7.Text + "%'or Сотрудники.[Номер паспорта] like '%" + textBox7.Text + "%'" +
                 "or  Должность.Название like '%" + textBox7.Text + "%'or Сотрудники.Телефон like '%" + textBox7.Text + "%'" +
                 "or Банк.Название like '%" + textBox7.Text + "%';", Connection);                 
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label7.Text = "Количество записей " + rows.ToString();            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "DELETE FROM [Сотрудники] WHERE [№]= " + dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand command = new SqlCommand(query, Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при удалении");
            }
            else MessageBox.Show("Данные удалены");
            Connection.Close();
            dataset();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int s = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1[1, s].Value.ToString();//фамилия
            textBox2.Text = dataGridView1[2, s].Value.ToString();//имя
            textBox3.Text = dataGridView1[3, s].Value.ToString();// отчество
            textBox4.Text = dataGridView1[4, s].Value.ToString();//серия
            textBox5.Text = dataGridView1[5, s].Value.ToString();//номер
            comboBox1.Text = dataGridView1[6, s].Value.ToString();//должн
            textBox6.Text = dataGridView1[7, s].Value.ToString();//телефон
            comboBox2.Text = dataGridView1[8, s].Value.ToString(); //банк           
        }

        private void save_Click(object sender, EventArgs e)
        {
            string dolznost;
            string Combo = comboBox2.Text;
            string[] words = Combo.Split(' ');
            string BIK = words[0];
            /*   Combo = comboBox1.Text;
               words = Combo.Split(' ');
               string dolznost = words[0];*/
            Connection.Open();
            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT [№] FROM Должность WHERE Название='" + comboBox1.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            dolznost = dt2.Rows[0][0].ToString();
            string sql = "UPDATE Сотрудники SET[Серия паспорта]='" + textBox4.Text + "', [Номер паспорта]='" + textBox5.Text + "', Фамилия=='" + textBox1.Text + "'" +
                ", Имя='" + textBox2.Text + "', Отчество='" + textBox3.Text + "', БИК='" + BIK + "', [Должность]='" + dolznost + "', Телефон='" + textBox6.Text + "'" ;
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();   
            textBox5.Clear();
            textBox6.Clear();
        }
    }  
}
