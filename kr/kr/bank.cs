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
    public partial class bank : Form
    {
        public bank()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void dataset()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Банк", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label4.Text = "Количество записей " + rows.ToString();
           
        }
        private void bank_Load(object sender, EventArgs e)
        {
            dataset();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string sql = "insert into Банк(Название,[Кор. счет],БИК,Подразделение,Город,Улица,Дом ) Values" +
                " ('" + namebox.Text + "','" + korschetbox.Text + "','" + bikbox.Text + "','" + podrazdelbox.Text + "','" + citybox.Text + "','" + streetbox.Text + "','" + textBox4.Text +"')";
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
            dataset();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            if (textBox1.Text == "")
            {
                dataset();
            }   
            else
            {
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Банк where Название like'%" + textBox1.Text + "%' or [Кор. счет] like '%" + textBox1.Text + "%' or БИК like '" + textBox1.Text + "%' or Подразделение like '%" + textBox1.Text + "%' or Город like '%" + textBox1.Text + "%' or Улица like '%" + textBox1.Text + "%';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label4.Text = "Количество записей " + rows.ToString();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "DELETE FROM [Банк] WHERE [БИК]= " + dataGridView1.CurrentRow.Cells[2].Value;
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
            SqlCommand command = new SqlCommand("UPDATE [Банк] SET [Название] = '"+dataGridView1.CurrentRow.Cells[0].Value.ToString()+ "', [Кор. счет]" +
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
    }
}
