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
    public partial class refinans : Form
    {
        public refinans()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void Update()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select Рефинансирование.[№],Рефинансирование.[№ договора], Рефинансирование.[date] as [Дата составления], Рефинансирование.vuplata as [Платеж], .[Процентная ставка].[%],CONCAT (Сотрудники.[№], ' ', Сотрудники.Фамилия) AS[Сотрудник] from Рефинансирование inner join Сотрудники ON Сотрудники.[№] = Рефинансирование.[№ сотрудника] inner join [Процентная ставка] ON [Процентная ставка].[№]=Рефинансирование.[%] ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
        }
        private void refinans_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT [%] FROM [Процентная ставка]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                procstav.Items.Add(dr4["%"].ToString());
            }
            SqlCommand cmd5 = Connection.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT [№] FROM [Договор]";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                number.Items.Add(dr5["№"]);
            }
            SqlCommand cmd6 = Connection.CreateCommand();
            cmd6.CommandType = CommandType.Text;
            cmd6.CommandText = "SELECT Сотрудники.[№], Сотрудники.[Фамилия], Должность.[Название]  FROM Должность INNER JOIN Сотрудники ON Должность.[№] = Сотрудники.[Должность]";
            cmd6.ExecuteNonQuery();
            DataTable dt6 = new DataTable();
            SqlDataAdapter da6 = new SqlDataAdapter(cmd6);
            da6.Fill(dt6);
            foreach (DataRow dr6 in dt6.Rows)
            {
                comboBox1.Items.Add(dr6["№"].ToString() + " " + dr6["Фамилия"] + " " + dr6["Название"]);
            }
            Connection.Close();
            Update();
        }

        private void vid_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (number.SelectedIndex)
            {
                case 0: Dogovor frm2 = new Dogovor(); frm2.Show(); break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: sotridnik frm2 = new sotridnik(); frm2.Show(); break;
            }
        }

        private void procent_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (procstav.SelectedIndex)
            {
                case 0: percent frm2 = new percent(); frm2.Show(); break;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            string vozvrat = dateTimePicker1.Value.ToString();
            string percent, sotrud;
            Connection.Open();            
            SqlCommand cmd4 = Connection.CreateCommand(); //проценты
            cmd4.CommandType = CommandType.Text;
            string pr = procstav.Text.Replace(",", ".");
            cmd4.CommandText = "SELECT [№] FROM [Процентная ставка] WHERE [%]='" + pr + "'";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            percent = dt4.Rows[0][0].ToString();
            // сотрудник
            string Combo = comboBox1.Text;
            string[] words = Combo.Split(' ');
            sotrud = words[0];
            string dogovor = number.Text;
            string date = DateTime.Today.ToString("dd'.'MM'.'yyyy");          //дата  
            int s = dataGridView1.CurrentCell.RowIndex;
            //добавление
            SqlCommand command = new SqlCommand("UPDATE Рефинансирование SET [№ договора]=@dogovor,[№ сотрудника]=@sotr, date=@date, [%]=@percent, [vuplata]=@summ WHERE [№]= " + s, Connection);
            command.Parameters.AddWithValue("@dogovor", dogovor);
            command.Parameters.AddWithValue("@sotr", sotrud);
            command.Parameters.AddWithValue("@vozvrat", vozvrat);
            command.Parameters.AddWithValue("@percent", percent);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox2.Text));
            command.ExecuteNonQuery();
            Connection.Close();
            Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vozvrat = dateTimePicker1.Value.ToString();
            string group, naznach, percent, vid, sotrud, klient;
            Connection.Open();
            SqlCommand cmd4 = Connection.CreateCommand(); //проценты
            cmd4.CommandType = CommandType.Text;
            string pr = procstav.Text.Replace(",", ".");
            cmd4.CommandText = "SELECT [№] FROM [Процентная ставка] WHERE [%]='" + pr + "'";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            percent = dt4.Rows[0][0].ToString();
            // сотрудник
            string Combo = comboBox1.Text;
            string[] words = Combo.Split(' ');
            sotrud = words[0];
            string dogovor = number.Text;
            string date = DateTime.Today.ToString("dd'.'MM'.'yyyy");          //дата  
            //добавление
            SqlCommand command = new SqlCommand("insert into Рефинансирование ([№ договора],[№ сотрудника], date, [%], [vuplata]) Values (@dogovor, @sotr, @date, @percent, @summ)", Connection);
            command.Parameters.AddWithValue("@dogovor", dogovor);
            command.Parameters.AddWithValue("@sotr", sotrud);
            command.Parameters.AddWithValue("@vozvrat", vozvrat);
            command.Parameters.AddWithValue("@percent", percent);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox2.Text));
            command.ExecuteNonQuery();
            Connection.Close();
            Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int s = dataGridView1.CurrentCell.RowIndex;
            number.Text = dataGridView1[1, s].Value.ToString();
            dateTimePicker1.Text = dataGridView1[2, s].Value.ToString();
            textBox2.Text = dataGridView1[3, s].Value.ToString();
            procstav.Text = dataGridView1[4, s].Value.ToString();
            comboBox1.Text = dataGridView1[5, s].Value.ToString();
            button1.Visible = false;
            add.Visible = true;
        }
    }
}
