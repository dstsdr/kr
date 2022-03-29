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
            SqlDataAdapter adapter = new SqlDataAdapter("Select Календарь.[Номер договора] as[№ договора], Календарь.[Дата запланированная] as [Плановая дата], Календарь.[Дата фактическая] as [Дата оплаты], Договор.[Ежемесячный платеж], Календарь.[Сумма оплаты], Календарь.[Основной долг], Календарь.Проценты as [%], Календарь.Остаток FROM Календарь inner join Договор ON Календарь.[Номер договора]=Договор.[№] ", Connection);
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

        private void button3_Click(object sender, EventArgs e)//add
        {

        }


        private void button1_Click(object sender, EventArgs e) //izmen
        {
            int s = dataGridView1.CurrentCell.RowIndex;
            dateTimePicker1.Text = dataGridView1[1, s].Value.ToString();
            textBox2.Text = dataGridView1[3, s].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void add_Click(object sender, EventArgs e)
        {
            Connection.Open();
            string query = "UPDATE [Календарь] SET [Сумма оплаты]=@summ, [Дата фактическая]=@date  WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value +" AND [Дата запланированная]=@dateplan";
            
            SqlCommand command = new SqlCommand(query, Connection);
            command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox2.Text));
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@dateplan", dataGridView1.CurrentRow.Cells[1].Value.ToString());

            /*SqlCommand query = new SqlCommand("insert into Календарь([Сумма оплаты],[Дата фактическая]) Values" +
                    " (@summ, @date) WHERE [Номер договора]= " + dataGridView1.CurrentRow.Cells[0].Value + " AND [Дата запланированная]=@dateplan", Connection);
           */ if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при добавлении");
            }
            Connection.Close();
            Update();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int s = dataGridView1.CurrentRow.Index;
            if (dataGridView1[2, s].Value.ToString() != "")
            {
                textBox2.Text = dataGridView1[4, s].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[s].Cells[2].Value);
            }             

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!=',')
            {
                e.Handled = true;

            }
        }
    }
}
