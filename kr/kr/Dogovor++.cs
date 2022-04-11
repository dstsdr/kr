using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kr
{
    public partial class Dogovor__ : Form
    {
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        public int s;
        public Dogovor__()
        {
            InitializeComponent();
        }
        public bool check = true;


        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void kalendar (double vuplata, int CreditPeriod, double SumCredit, double InterestRateYear) // составление календаря
        {
            Connection.Open();
            SqlCommand cmd5 = Connection.CreateCommand(); // группа риска
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT MAX(Договор.[№]) FROM Договор";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            da5.Fill(dt5);
            int dogovor = Convert.ToInt32(dt5.Rows[0][0]);
            Connection.Close();
            double ItogCreditSum = vuplata * CreditPeriod; // Итоговая сумма кредита
            double SumCreditOperation = SumCredit;
            double ItogCreditSumOperation = ItogCreditSum;
            double ItogPlus = 0;
            var months = DateTime.Today; //ToString("dd'/'MM'/'yyyy");
            for (int i = 0; i < CreditPeriod; ++i)
            {
                double procent = SumCreditOperation * (InterestRateYear / 100) / 12;
                SumCreditOperation -= vuplata - procent;
                decimal osn, percent, ostatok, kd;
                // month = i + 1; //номер месяца
                months=months.AddMonths(1);
                //textBox4.Text = months.ToString("dd'/'MM'/'yyyy") +"\n";
                 //  kd = vuplata.ToString("N2"); //Ежемесячный платеж
                  osn = Convert.ToDecimal(vuplata - procent); //Платеж за основной долг
                  percent = Convert.ToDecimal(procent); //Платеж процента
                  ostatok =Convert.ToDecimal(SumCreditOperation); //Основной остаток
                  ItogCreditSumOperation -= vuplata;
                  ItogPlus = Convert.ToDouble(ostatok);
                  Connection.Open();
                /*   string sql = "insert into [Календарь]([Дата запланированная], [Основной долг], [Проценты], [Остаток]) Values" +
                   " ('" + months.ToString("dd'.'MM'.'yyyy") + "','" + osn + "','" + percent + "','" + ostatok + "')";
                 SqlCommand command = new SqlCommand(sql, Connection);*/
                SqlCommand command = new SqlCommand("insert into [Календарь]([Дата запланированная],[Номер договора], [Основной долг], [Проценты], [Остаток], [Сумма оплаты]) Values" +
                  " (@date, @number, @OSN, @PERCENT, @OST,@nol)", Connection);
                command.Parameters.AddWithValue("@date", months.ToString("dd'.'MM'.'yyyy"));
                command.Parameters.AddWithValue("@number", dogovor);
                command.Parameters.AddWithValue("@OSN", osn);
                command.Parameters.AddWithValue("@PERCENT", percent);
                command.Parameters.AddWithValue("@OST", ostatok);
                command.Parameters.AddWithValue("@nol", 0);

                command.ExecuteNonQuery();
                  Connection.Close();
            }
        }
        private void Add (bool check)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "";
            if (sotr.SelectedIndex > 0) { sotr.BackColor = Color.White; }
            else { sotr.BackColor = Color.DarkGray; a += "сотрудник\n"; check = false; }
            if (klientcmb.SelectedIndex > 0) { klientcmb.BackColor = Color.White; }
            else { klientcmb.BackColor = Color.DarkGray; a += "клиент\n"; check = false; }
            if (vid.SelectedIndex > 0) { vid.BackColor = Color.White; }
            else { vid.BackColor = Color.DarkGray; a += "вид кредита\n"; check = false; }
            if (procent.SelectedIndex > 0) { procent.BackColor = Color.White; }
            else { procent.BackColor = Color.DarkGray; a += "процентная ставка\n"; check = false; }
            if (risk.SelectedIndex > 0) { risk.BackColor = Color.White; }
            else { risk.BackColor = Color.DarkGray; a += "группа риска\n"; check = false; }
            if (nazn.SelectedIndex > 0) { nazn.BackColor = Color.White; }
            else { nazn.BackColor = Color.DarkGray; a += "назначение\n"; check = false; }
            if (textBox3.Text != "") { textBox3.BackColor = Color.White; }
            else { textBox3.BackColor = Color.DarkGray; a += "сумма\n"; check = false; }
            if (textBox1.Text != "") { textBox1.BackColor = Color.White; }
            else { textBox1.BackColor = Color.DarkGray; a += "нейстойка\n"; check = false; }
            if (check == false)
            {
                MessageBox.Show("Для добавления записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                string vozvrat = dateTimePicker1.Value.ToString();
                vozvrat = vozvrat.Substring(0, vozvrat.IndexOf(' ') + 1);
                int CreditPeriod = ((dateTimePicker1.Value.Year - DateTime.Today.Year) * 12) + dateTimePicker1.Value.Month - DateTime.Today.Month;
                double SumCredit = Convert.ToDouble(textBox3.Text); // Сумма кредита
                double InterestRateYear = Convert.ToDouble(procent.Text); // Процентная ставка, ГОДОВАЯ
                double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
                                                                        // int CreditPeriod = Convert.ToInt32(textBox2.Text); // Срок кредита, переводим в месяцы, если указан в годах
                //CreditPeriod *= 12;
                decimal vuplata = Convert.ToDecimal(SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod)))); // Ежемесячный платеж


                string date = DateTime.Today.ToString("dd'.'MM'.'yyyy");          //дата  


                string group, naznach, percent, vid, sotrud, klient;
                Connection.Open();
                SqlCommand cmd5 = Connection.CreateCommand(); // группа риска
                cmd5.CommandType = CommandType.Text;
                cmd5.CommandText = "SELECT [№] FROM [Группа риска] WHERE [Группа]='" + risk.Text + "'";
                cmd5.ExecuteNonQuery();
                DataTable dt5 = new DataTable();
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                da5.Fill(dt5);
                group = dt5.Rows[0][0].ToString();
                SqlCommand cmd2 = Connection.CreateCommand(); //целевое назначение
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT [№] FROM [Целевое назначение кредита] WHERE [Название]='" + nazn.Text + "'";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                naznach = dt2.Rows[0][0].ToString();
                SqlCommand cmd3 = Connection.CreateCommand(); // вид кредита
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "SELECT [Код] FROM [Вид кредита] WHERE [Вид]='" + this.vid.Text + "'";
                cmd3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(dt3);
                vid = dt3.Rows[0][0].ToString();
                SqlCommand cmd4 = Connection.CreateCommand(); //проценты
                cmd4.CommandType = CommandType.Text;
                string pr = procent.Text.Replace(",", ".");
                cmd4.CommandText = "SELECT [№] FROM [Процентная ставка] WHERE [%]='" + pr + "'";
                cmd4.ExecuteNonQuery();
                DataTable dt4 = new DataTable();
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(dt4);
                percent = dt4.Rows[0][0].ToString();
                // сотрудник
                string dolznost;
                string Combo = sotr.Text;
                string[] words = Combo.Split(' ');
                sotrud = words[0];
                // klient
                Combo = klientcmb.Text;
                words = Combo.Split(' ');
                klient = words[0];
                //добавление
                 SqlCommand command = new SqlCommand("insert into Договор([№ группы риска],[№ сотрудника],[ИНН клиента], [№ назначения], [№ вида], [дата заключения], [Срок погашения], [Ежемесячный платеж], [№ ставки], [Сумма], [Неустойка]) Values" +
                     " (@group, @sotr, @klient, @naznach, @vid, @date, @vozvrat, @vuplata, @percent, @summ, @neust)", Connection);
                 command.Parameters.AddWithValue("@group", group);
                 command.Parameters.AddWithValue("@sotr", sotrud);
                 command.Parameters.AddWithValue("@klient", klient);
                 command.Parameters.AddWithValue("@naznach", naznach);
                 command.Parameters.AddWithValue("@vid", vid);
                 command.Parameters.AddWithValue("@date", date);
                 command.Parameters.AddWithValue("@vozvrat", vozvrat);
                 command.Parameters.AddWithValue("@vuplata", vuplata);
                 command.Parameters.AddWithValue("@percent", percent);
                 command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox3.Text));
                 command.Parameters.AddWithValue("@neust", float.Parse(textBox1.Text));
                 command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении договора");
                }
                else
                {
                    MessageBox.Show("Договор добавлен");
                }
                Connection.Close();
                 kalendar(Convert.ToDouble(vuplata), CreditPeriod, SumCredit, InterestRateYear);
            }
            
        }

        private void Dogovor___Load(object sender, EventArgs e)
        {
           // Connection.Close();
            Connection.Open();  
            SqlCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ИНН, Фамилия FROM Клиенты";
            cmd.ExecuteNonQuery();  
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);    
            foreach (DataRow dr in dt.Rows)
            {
                klientcmb.Items.Add(dr["ИНН"].ToString()+ " " + dr["Фамилия"]);
            }
            SqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT [Название] FROM [Целевое назначение кредита]";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                nazn.Items.Add(dr2["Название"].ToString());
            }
            SqlCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT [Вид] FROM [Вид кредита]";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                vid.Items.Add(dr3["Вид"].ToString());
            }
            SqlCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT [%] FROM [Процентная ставка]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                procent.Items.Add(dr4["%"].ToString());
            }
            SqlCommand cmd5 = Connection.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT [Группа] FROM [Группа риска]";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                risk.Items.Add(dr5["Группа"]);
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
                sotr.Items.Add(dr6["№"].ToString()+ " " + dr6["Фамилия"] + " " + dr6["Название"]);
            }
            Connection.Close();
            selectkalendar();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (sotr.SelectedIndex)
            {
                case 0: sotridnik frm2 = new sotridnik(); frm2.Show(); break;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (klientcmb.SelectedIndex)
            {
                case 0: klient frm2 = new klient(); frm2.Show(); break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (procent.SelectedIndex)
            {
               case 0:  percent frm2 = new percent(); frm2.Show(); break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (nazn.SelectedIndex)
            {
                   case 0:  naznach frm2 = new naznach(); frm2.Show(); break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (risk.SelectedIndex)
            {
                   case 0:  risk frm2 = new risk(); frm2.Show(); break;
            }
        }

        private void vid_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (vid.SelectedIndex)
            {
                case 0: vid frm2 = new vid(); frm2.Show(); break;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            string a = "";
            if (textBox3.Text != "") { textBox3.BackColor = Color.White; }
            else { textBox3.BackColor = Color.DarkGray; a += "сумма\n"; check = false; }
            if (textBox1.Text != "") { textBox1.BackColor = Color.White; }
            else { textBox1.BackColor = Color.DarkGray; a += "нейстойка\n"; check = false; }
            if (check == false)
            {
                MessageBox.Show("Для изменения записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                string vozvrat = dateTimePicker1.Value.ToString();
                string group, naznach, percent, vid, sotrud, klient;
                Connection.Open();
                SqlCommand cmd5 = Connection.CreateCommand(); // группа риска
                cmd5.CommandType = CommandType.Text;
                cmd5.CommandText = "SELECT [№] FROM [Группа риска] WHERE [Группа]='" + risk.Text + "'";
                cmd5.ExecuteNonQuery();
                DataTable dt5 = new DataTable();
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                da5.Fill(dt5);
                group = dt5.Rows[0][0].ToString();
                SqlCommand cmd2 = Connection.CreateCommand(); //целевое назначение
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT [№] FROM [Целевое назначение кредита] WHERE [Название]='" + nazn.Text + "'";
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                naznach = dt2.Rows[0][0].ToString();
                SqlCommand cmd3 = Connection.CreateCommand(); // вид кредита
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "SELECT [Код] FROM [Вид кредита] WHERE [Вид]='" + this.vid.Text + "'";
                cmd3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(dt3);
                vid = dt3.Rows[0][0].ToString();
                SqlCommand cmd4 = Connection.CreateCommand(); //проценты
                cmd4.CommandType = CommandType.Text;
                string pr = procent.Text.Replace(",", ".");
                cmd4.CommandText = "SELECT [№] FROM [Процентная ставка] WHERE [%]='" + pr + "'";
                cmd4.ExecuteNonQuery();
                DataTable dt4 = new DataTable();
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                da4.Fill(dt4);
                percent = dt4.Rows[0][0].ToString();
                // сотрудник
                string dolznost;
                string Combo = sotr.Text;
                string[] words = Combo.Split(' ');
                sotrud = words[0];
                // klient
                Combo = klientcmb.Text;
                words = Combo.Split(' ');
                klient = words[0];
                //добавление
                SqlCommand command = new SqlCommand("UPDATE Договор SET [№ группы риска]=@group,[№ сотрудника]=@sotr,[ИНН клиента]=@klient, [№ назначения]=@naznach," +
                    " [№ вида]=@vid, [Срок погашения]=@vozvrat, [№ ставки]=@percent, [Сумма]=@summ, [Неустойка]=@neust WHERE [№]= " + s, Connection);
                command.Parameters.AddWithValue("@group", group);
                command.Parameters.AddWithValue("@sotr", sotrud);
                command.Parameters.AddWithValue("@klient", klient);
                command.Parameters.AddWithValue("@naznach", naznach);
                command.Parameters.AddWithValue("@vid", vid);
                command.Parameters.AddWithValue("@vozvrat", vozvrat);
                command.Parameters.AddWithValue("@percent", percent);
                command.Parameters.AddWithValue("@summ", Convert.ToDecimal(textBox3.Text));
                command.Parameters.AddWithValue("@neust", float.Parse(textBox1.Text));
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при изменении договора");
                }
                else
                {
                    MessageBox.Show("Договор изменен");
                }
                Connection.Close();
                updatekalendar();
            }
        }

        private void selectkalendar()
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Календарь WHERE [Номер договора]=" + s, Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void updaterashet ()
        {
            int CreditPeriod = dataGridView1.RowCount;
            double SumCredit = Convert.ToDouble(textBox3.Text); // Сумма кредита
            double InterestRateYear = Convert.ToDouble(procent.Text); // Процентная ставка, ГОДОВАЯ
            double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
            decimal vuplata = Convert.ToDecimal(SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod)))); // Ежемесячный платеж
            double ItogCreditSum = Convert.ToDouble(vuplata) * CreditPeriod; // Итоговая сумма кредита
            double SumCreditOperation = SumCredit;
            double ItogCreditSumOperation = ItogCreditSum;
            double ItogPlus = 0;
            for (int i = 0; i < dataGridView1.RowCount; ++i)
            {
                double procent = SumCreditOperation * (InterestRateYear / 100) / 12;
                SumCreditOperation -= Convert.ToDouble(vuplata) - procent;
                decimal ostatok;
                dataGridView1.Rows[i].Cells[5].Value = Convert.ToDecimal(Convert.ToDouble(vuplata) - procent); //Платеж за основной долг
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToDecimal(procent); //Платеж процента
                ostatok = Convert.ToDecimal(SumCreditOperation); //Основной остаток
                dataGridView1.Rows[i].Cells[7].Value = Convert.ToDecimal(SumCreditOperation); //Основной остаток
                ItogCreditSumOperation -= Convert.ToDouble(vuplata);
                ItogPlus = Convert.ToDouble(ostatok);
            }
            Connection.Open();
            for (int i = 0; i < dataGridView1.RowCount; i++)
             {   SqlCommand command = new SqlCommand("UPDATE [Календарь] SET [Дата запланированная]=@date,[Номер договора]=@number, [Основной долг]=@OSN, [Проценты]=@PERCENT, [Остаток]=@OST " +
                     "WHERE [Код]=@kod", Connection);
                command.Parameters.AddWithValue("@kod", dataGridView1.Rows[i].Cells[4].Value.ToString());//если дата будет кривая, то проблема здесь
                command.Parameters.AddWithValue("@date", dataGridView1.Rows[i].Cells[0].Value.ToString());//если дата будет кривая, то проблема здесь
                 command.Parameters.AddWithValue("@number", dataGridView1.Rows[i].Cells[2].Value);
                 command.Parameters.AddWithValue("@OSN", dataGridView1.Rows[i].Cells[5].Value);
                 command.Parameters.AddWithValue("@PERCENT", dataGridView1.Rows[i].Cells[6].Value);
                 command.Parameters.AddWithValue("@OST", dataGridView1.Rows[i].Cells[7].Value);
                 command.ExecuteNonQuery();
             }
            SqlCommand command1 = new SqlCommand("UPDATE Договор SET [Ежемесячный платеж]=@group WHERE [№]= " + dataGridView1.Rows[0].Cells[2].Value, Connection);
            command1.Parameters.AddWithValue("@group", vuplata);
            command1.ExecuteNonQuery();
            Connection.Close();
        }
        private void updatekalendar()
        {
            // selectkalendar();
          //  string vozvrat = dateTimePicker1.Value.ToString();
          //  vozvrat = vozvrat.Substring(0, vozvrat.IndexOf(' ') + 1);
            int s = dataGridView1.RowCount - 1;
            DateTime olddate = Convert.ToDateTime(dataGridView1.Rows[s].Cells[0].Value);
            if (olddate < dateTimePicker1.Value)
            {
                int raznica = ((dateTimePicker1.Value.Year - olddate.Year) * 12) + dateTimePicker1.Value.Month - olddate.Month;
                while (raznica > 0)
                {
                    olddate = olddate.AddMonths(1);
                    Connection.Open();
                    SqlCommand command = new SqlCommand("insert into [Календарь]([Дата запланированная],[Номер договора]) Values (@date, @number)", Connection);
                    command.Parameters.AddWithValue("@date", olddate.ToString("dd'.'MM'.'yyyy"));
                    command.Parameters.AddWithValue("@number", dataGridView1.Rows[0].Cells[2].Value);
                    command.ExecuteNonQuery();
                    Connection.Close();
                    raznica--;
                    selectkalendar();
                }
                updaterashet();
            }
            else
            {
                int raznica = ((olddate.Year - dateTimePicker1.Value.Year) * 12) + olddate.Month - dateTimePicker1.Value.Month;
                while (raznica > 0)
                {
                    s = dataGridView1.RowCount - 1;
                    //int kod = Convert.ToInt32(dataGridView1.Rows[s].Cells[4].Value);
                    Connection.Open();
                    string query = "DELETE FROM [Календарь] WHERE [Код]= " + dataGridView1.Rows[s].Cells[4].Value;
                    SqlCommand command = new SqlCommand(query, Connection);
                    if (command.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Возникла ошибка при удалении календаря");
                    }
                    Connection.Close();
                    dataGridView1.Rows.RemoveAt(s); //удаление строки из датагридвиев
                    selectkalendar();
                    raznica--;
                }
                updaterashet();
            }
            if (olddate == dateTimePicker1.Value)
            {
                updaterashet();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
