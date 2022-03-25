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
    public partial class klientwiew : Form
    {
        public klientwiew()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT *  FROM Клиенты", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label7.Text = "Количество записей " + rows.ToString();
        }
        private void klientwiew_Load(object sender, EventArgs e)
        {
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

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "DELETE FROM [Клиенты] WHERE [ИНН]= " + dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand command = new SqlCommand(query, Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при удалении");
            }
            else MessageBox.Show("Данные удалены");
            Connection.Close();
            dataset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT* FROM Клиенты where [Серия паспорта] like '%" + textBox7.Text + "%' or [Номер паспорта] like" +
               " '%" + textBox7.Text + "%' or Имя like '%" + textBox7.Text + "%' or Отчество like '%" + textBox7.Text + "%' or [Город прописки] like '%" + textBox7.Text + "%'" +
               " or Фамилия like '%" + textBox7.Text + "%'or Телефон like '%" + textBox7.Text + "%'or [Ссудный счет] like '%" + textBox7.Text + "%'" +
               "or  ИНН like '%" + textBox7.Text + "%'or Счет like '%" + textBox7.Text + "%'or Телефон like '%" + textBox7.Text + "%'or Работа like '%" + textBox7.Text + "%'" +
               "or [Улица прописки] like '%" + textBox7.Text + "%';", Connection);          
             DataSet ds = new DataSet();
             adapter.Fill(ds, "info");
             dataGridView1.DataSource = ds.Tables[0];
             Connection.Close();
             int rows = dataGridView1.Rows.Count - 1;
             label7.Text = "Количество записей " + rows.ToString();
         }

         private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
         {
             Connection.Open();
             SqlCommand command = new SqlCommand("UPDATE [Клиенты] SET [Название] = '" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "', [Кор. счет]" +
                 "='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "', Подразделение='" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "'," +
                 "Город='" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "', Улица='" + dataGridView1.CurrentRow.Cells[5].Value.ToString() + "'," +
                 "Дом='" + dataGridView1.CurrentRow.Cells[6].Value.ToString() + "' WHERE [БИК]= " + dataGridView1.CurrentRow.Cells[2].Value, Connection);
             command.Parameters.AddWithValue("@percent", dataGridView1.CurrentCell.Value);
             if (command.ExecuteNonQuery() != 1)
             {
                 MessageBox.Show("Возникла ошибка при изменении");
             }
             Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            klient frm2 = new klient(); frm2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
