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
    public partial class kalendar : Form
    {
        public kalendar()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");

        private void kalendar_Load(object sender, EventArgs e)
        {
            Update();
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.;
        }
        private void Update()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая] as [Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток, Календарь.Статус FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
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
            SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая] as " +
                "[Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток, Календарь.Статус " +
                "FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] where " +
                "Кадендарь.[Номер договора] like " + "'%" + textBox1.Text +
                "%' or Календарь.[Дата запланированная] like '%" + textBox1.Text + "%' or Календарь.[Дата фактическая] like '%" + textBox1.Text + "%' or Договор.[Ежемесячный платеж] like '%" + textBox1.Text + "%'" +
               " or Календарь.[Сумма оплаты] like '%" + textBox1.Text + "%'or Календарь.[Основной долг] like '%" + textBox1.Text + "%'" +
                "or   Календарь.Проценты like '%" + textBox1.Text + "%'or Календарь.Остаток, like '%" + textBox1.Text + "%'" +
               "or  Календарь.Статус like '%" + textBox1.Text + "%'", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
        }


        private void button1_Click(object sender, EventArgs e) //izmen
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private string status ()
        {
            int s = dataGridView1.CurrentRow.Index;
            string status="";
            decimal st = Convert.ToDecimal(dataGridView1[3, s].Value.ToString()) - Convert.ToDecimal(textBox2.Text);
            if (st == 0)
            {
                status = "Выплачено"; return status;
            }
            if (st < 0)
            { status = "Переплачено"; return status; }
            if (st > 0) { status = "Недоплачено"; return status; }
            return status;
        }
        private void obnovstatus ()
        {
            Connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Договор SET Статус=@status WHERE [№]= " + dataGridView1.CurrentRow.Cells[0].Value, Connection);
         command.Parameters.AddWithValue("@status", statusd());            
            command.ExecuteNonQuery();
            Connection.Close();
        }
        private string statusd ()
        {
            //  Connection.Open();         

            string query = "select COUNT(Календарь.Код) FROM Календарь WHERE Календарь.[Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand command = new SqlCommand(query, Connection);
            decimal kolichestvo = (Int32)command.ExecuteScalar();
            kolichestvo=kolichestvo* Convert.ToDecimal(dataGridView1.CurrentRow.Cells[3].Value);
          //  Connection.Close();
          //  Connection.Open();
            string query2 = "select SUM(Календарь.[Сумма оплаты]) FROM Календарь WHERE Календарь.[Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value;
            SqlCommand command2 = new SqlCommand(query2, Connection);
            decimal vuplacheno = (Decimal)command2.ExecuteScalar();
          //  Connection.Close();
            kolichestvo = kolichestvo - vuplacheno;
            if (kolichestvo <= 0) { query = "Завершен"; return query; }
            else
            {
                
                string query3 = "DECLARE @Min FLOAT = DATEDIFF(DD, (SELECT MAX(Календарь.[Дата запланированная]) from Календарь Where Календарь.[Номер договора] = " + dataGridView1.CurrentRow.Cells[0].Value+ "), GETDATE()) select @MIN";
                SqlCommand command3 = new SqlCommand(query3, Connection);
                float date = float.Parse(command3.ExecuteScalar().ToString());
                textBox1.Text = date.ToString();
                if (date<0) { query = "Незавершен"; return query; }
                else { query = "Просрочен"; return query; }
            }
        }
        private void add_Click(object sender, EventArgs e)
        {
            obnovstatus(); 
            Connection.Open();
            string query = "UPDATE [Календарь] SET [Сумма оплаты]=@summ, [Дата фактическая]=@date, Статус=@status WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value +" AND [Дата запланированная]=@dateplan";
            
            SqlCommand command = new SqlCommand(query, Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при изменении");
            }
            command.Parameters.AddWithValue("@status", status());
            command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox2.Text));
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@dateplan", dataGridView1.CurrentRow.Cells[1].Value.ToString());

            /*SqlCommand query = new SqlCommand("insert into Календарь([Сумма оплаты],[Дата фактическая]) Values" +
                    " (@summ, @date) WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value + " AND [Дата запланированная]=@dateplan", Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при добавлении");
            }
            Connection.Close();
            Update();*/
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
          /*  int s = dataGridView1.CurrentRow.Index;
            if (dataGridView1[2, s].Value.ToString() != "")
            {
                textBox2.Text = dataGridView1[4, s].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[s].Cells[2].Value);
            }   */          

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!=',')
            {
                e.Handled = true;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            obnovstatus();
            Connection.Open();
            string query = "UPDATE [Календарь] SET [Сумма оплаты]=@summ, [Дата фактическая]=@date, Статус=@status WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value + " AND [Дата запланированная]=@dateplan";

            SqlCommand command = new SqlCommand(query, Connection);            
            command.Parameters.AddWithValue("@status", status());
            command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox2.Text));
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@dateplan", dataGridView1.CurrentRow.Cells[1].Value.ToString());
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при изменении");
            }
            /*SqlCommand query = new SqlCommand("insert into Календарь([Сумма оплаты],[Дата фактическая]) Values" +
                    " (@summ, @date) WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value + " AND [Дата запланированная]=@dateplan", Connection);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при добавлении");
            }*/
            Connection.Close();
            Update();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int s = dataGridView1.CurrentCell.RowIndex;
            dateTimePicker1.Text = dataGridView1[1, s].Value.ToString();
            textBox2.Text = dataGridView1[3, s].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked)
                   {
                Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая]" +
                " as [Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток, Календарь.Статус " +
                "FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] Where Календарь.Статус='Недоплачено' ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Не обнаружены записи удовлетворяющие условию", "Записи не найдены");
                }
                dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
               }
              else { Update(); }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая]" +
                    " as [Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток, Календарь.Статус " +
                    "FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] Where Календарь.Статус='Переплачено' ", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Не обнаружены записи удовлетворяющие условию", "Записи не найдены");
                }
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
            }
            else { Update(); }

        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
                 {
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая]" +
                        " as [Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток, Календарь.Статус " +
                        "FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] Where Календарь.Статус like '%Выплачено%' ", Connection);
                DataSet ds = new DataSet();                
                adapter.Fill(ds, "info");
                if (ds.Tables[0].Rows.Count == 1)
                {
                    MessageBox.Show("Не обнаружены записи удовлетворяющие условию", "Записи не найдены");
                }
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
                int rows = dataGridView1.Rows.Count - 1;
                label1.Text = "Количество записей " + rows.ToString();
              }
              else { Update(); }
        }
    }
}
