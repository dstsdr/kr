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
        public bool check = true;

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
            string a = "";            
            if (namebox.Text != "") { namebox.BackColor = Color.White; }
            else { namebox.BackColor = Color.DarkGray; a += "название банка\n"; check = false; }
            if (korschetbox.Text != "") { korschetbox.BackColor = Color.White; }
            else { korschetbox.BackColor = Color.DarkGray; a += "кор. счет\n"; check = false; }
            if (bikbox.Text != "") { bikbox.BackColor = Color.White; }
            else { bikbox.BackColor = Color.DarkGray; a += "БИК\n"; check = false; }
            if (citybox.Text != "") { citybox.BackColor = Color.White; }
            else { citybox.BackColor = Color.DarkGray; a += "город\n"; check = false; }
            if (streetbox.Text != "") { streetbox.BackColor = Color.White; }
            else { streetbox.BackColor = Color.DarkGray; a += "улица\n"; check = false; }
            if (podrazdelbox.Text != "") { podrazdelbox.BackColor = Color.White; }
            else { podrazdelbox.BackColor = Color.DarkGray; a += "подразделение\n"; check = false; }
            if (textBox4.Text != "") { textBox4.BackColor = Color.White; }
            else { textBox4.BackColor = Color.DarkGray; a += "номер здания\n"; check = false; }
            {
                MessageBox.Show("Для добавления записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                Connection.Open();
                string sql = "insert into Банк(Название,[Кор. счет],БИК,Подразделение,Город,Улица,Дом ) Values" +
                    " ('" + namebox.Text + "','" + korschetbox.Text + "','" + bikbox.Text + "','" + podrazdelbox.Text + "','" + citybox.Text + "','" + streetbox.Text + "','" + textBox4.Text + "')";
                SqlCommand command = new SqlCommand(sql, Connection);
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении банка");
                }
                else
                {
                    MessageBox.Show("Банк добавлен");
                }
                Connection.Close();
                dataset();
            }
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

        private void citybox_TextChanged(object sender, EventArgs e)
        {

        }

        private void citybox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (citybox.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void streetbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void streetbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (streetbox.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
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

        private void bikbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            bikbox.MaxLength = 9;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void korschetbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            korschetbox.MaxLength = 20;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void podrazdelbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            podrazdelbox.MaxLength = 10;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[dataGridView1.ColumnCount - 1];

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int s = dataGridView1.CurrentRow.Index;
            namebox.Text = dataGridView1[0, s].Value.ToString();//фамилия
            korschetbox.Text = dataGridView1[1, s].Value.ToString();//имя
            bikbox.Text = dataGridView1[2, s].Value.ToString();// отчество
            podrazdelbox.Text = dataGridView1[3, s].Value.ToString();//серия
            citybox.Text = dataGridView1[4, s].Value.ToString();//номер
            streetbox.Text = dataGridView1[5, s].Value.ToString();//должн
            textBox4.Text = dataGridView1[6, s].Value.ToString();//телефон
            button8.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string a = "";
            if (namebox.Text != "") { namebox.BackColor = Color.White; }
            else { namebox.BackColor = Color.DarkGray; a += "название банка\n"; check = false; }
            if (korschetbox.Text != "") { korschetbox.BackColor = Color.White; }
            else { korschetbox.BackColor = Color.DarkGray; a += "кор. счет\n"; check = false; }
            if (bikbox.Text != "") { bikbox.BackColor = Color.White; }
            else { bikbox.BackColor = Color.DarkGray; a += "БИК\n"; check = false; }
            if (citybox.Text != "") { citybox.BackColor = Color.White; }
            else { citybox.BackColor = Color.DarkGray; a += "город\n"; check = false; }
            if (streetbox.Text != "") { streetbox.BackColor = Color.White; }
            else { streetbox.BackColor = Color.DarkGray; a += "улица\n"; check = false; }
            if (podrazdelbox.Text != "") { podrazdelbox.BackColor = Color.White; }
            else { podrazdelbox.BackColor = Color.DarkGray; a += "подразделение\n"; check = false; }
            if (textBox4.Text != "") { textBox4.BackColor = Color.White; }
            else { textBox4.BackColor = Color.DarkGray; a += "номер здания\n"; check = false; }
            if (check == false)
            {
                MessageBox.Show("Для изменения записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                Connection.Open();
                SqlCommand command = new SqlCommand("UPDATE [Банк] SET [Название] = '" + namebox.Text + "', [Кор. счет]" +
                    "='" + korschetbox.Text + "', Подразделение='" + podrazdelbox.Text + "'," +
                    "Город='" + citybox.Text + "', Улица='" + streetbox.Text + "'," +
                    "Дом='" + textBox4.Text + "' WHERE [БИК]= " + dataGridView1.CurrentRow.Cells[2].Value, Connection);
                command.Parameters.AddWithValue("@percent", dataGridView1.CurrentCell.Value);
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при изменении банка");
                }
                else
                {
                    MessageBox.Show("Банк изменен");
                }
                Connection.Close();
                button8.Visible = false;
            }
        }
    }
}
