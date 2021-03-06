using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        public bool check = true;
        private void vid_Load(object sender, EventArgs e)
        {
            dataset();
        }

        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select Код AS [№], Вид from [Вид кредита]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label2.Text = "Количество записей " + rows.ToString();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            string a = "";
            if (namebox.Text != "") { namebox.BackColor = Color.White; }
            else { namebox.BackColor = Color.DarkGray; a += "вид\n"; check = false; }
           
            {
                MessageBox.Show("Для добавления записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                Connection.Open();
                string sql = "insert into [Вид кредита](Вид) Values ('" + namebox.Text + "')";
                SqlCommand command = new SqlCommand(sql, Connection);
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении вида кредита");
                }
                else
                {
                    MessageBox.Show("Вид кредита изменен");
                }
                Connection.Close();
                dataset();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                dataset();
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select Код AS [№], Вид from [Вид кредита] where Код like'%" + textBox1.Text + "%' or Вид  like '%" + textBox1.Text + "%';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label2.Text = "Количество записей " + rows.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "DELETE FROM [Вид кредита] WHERE Код= " + dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand command = new SqlCommand(query, Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при удалении");
            }
            else MessageBox.Show("Данные удалены");
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

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString()=="")
            {
                MessageBox.Show("заполните поле");
            }
            else
            {
                Connection.Open();
                string query = "UPDATE [Вид кредита] SET [Вид] =  '" + dataGridView1.CurrentCell.Value.ToString() + "' WHERE [Код]= " + dataGridView1.CurrentRow.Cells[0].Value;
                SqlCommand command = new SqlCommand(query, Connection);
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при изменении");
                }
                Connection.Close();
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void namebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (namebox.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
