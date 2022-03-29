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
             
         }

        private void button1_Click(object sender, EventArgs e)
        {
            klient frm2 = new klient(); frm2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            int s = dataGridView1.CurrentCell.RowIndex;      

            klient frm = new klient();
            frm.button1.Visible = false;
            frm.save.Visible = true;
            frm.LastName.Text = dataGridView1[0, s].Value.ToString();
            frm.FirstName.Text = dataGridView1[1, s].Value.ToString();
            frm.otchestvo.Text = dataGridView1[2, s].Value.ToString();
            frm.seriya.Text = dataGridView1[3, s].Value.ToString();
            frm.nomer.Text = dataGridView1[4, s].Value.ToString();
            frm.kem.Text = dataGridView1[5, s].Value.ToString();
            frm.where.Text = dataGridView1[6, s].Value.ToString();
            frm.dateTimePicker1.Text = dataGridView1[7, s].Value.ToString();
            frm.phone.Text = dataGridView1[8, s].Value.ToString();
            frm.INN.Text = dataGridView1[9, s].Value.ToString();
            frm.job.Text = dataGridView1[10, s].Value.ToString();
            frm.stash.Text = dataGridView1[11, s].Value.ToString();
            frm.dohod.Text = dataGridView1[12, s].Value.ToString();
            frm.city.Text = dataGridView1[13, s].Value.ToString();
            frm.street.Text = dataGridView1[14, s].Value.ToString();
            frm.home.Text = dataGridView1[15, s].Value.ToString();
            frm.schet.Text = dataGridView1[16, s].Value.ToString();
            frm.ssuda.Text = dataGridView1[17, s].Value.ToString();
            frm.podrazdel.Text = dataGridView1[18, s].Value.ToString();
            frm.ShowDialog();
        }
    }
}
