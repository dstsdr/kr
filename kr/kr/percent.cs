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
    public partial class percent : Form
    {
        public percent()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");

        private void percent_Load(object sender, EventArgs e)
        {
            dataset();
        }
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT [№], [%] FROM [Процентная ставка]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label2.Text = "Количество записей " + rows.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string sql = "insert into [Процентная ставка]([%]) Values ('" + namebox.Text + "')";
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
            dataset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                dataset();
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select [№], [%] from [Процентная ставка] where [№] like '%" + textBox1.Text + "%' or [%] like '%" + textBox1.Text + "%';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "DELETE FROM [Процентная ставка] WHERE [№]= " + dataGridView1.CurrentRow.Cells[0].Value;
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
            Connection.Open();
            SqlCommand command = new SqlCommand("UPDATE [Процентная ставка] SET [%] = @percent WHERE [№]= " + dataGridView1.CurrentRow.Cells[0].Value, Connection);
            command.Parameters.AddWithValue("@percent", dataGridView1.CurrentCell.Value);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при изменении");
            }
            Connection.Close();
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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void namebox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;

            }
        }
    }
}
