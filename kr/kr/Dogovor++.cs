using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace kr
{
    public partial class Dogovor__ : Form
    {
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");
        public Dogovor__()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void kalendar (double vuplata, int CreditPeriod, double SumCredit, double InterestRateYear) // составление календаря
        {
            double ItogCreditSum = vuplata * CreditPeriod; // Итоговая сумма кредита
            double SumCreditOperation = SumCredit;
            double ItogCreditSumOperation = ItogCreditSum;
            double ItogPlus = 0;
            var months = DateTime.Today; //ToString("dd'/'MM'/'yyyy");
            for (int i = 0; i < CreditPeriod; ++i)
            {
                double procent = SumCreditOperation * (InterestRateYear / 100) / 12;
                SumCreditOperation -= vuplata - procent;
                string osn, percent, ostatok, kd;
                // month = i + 1; //номер месяца
                months=months.AddMonths(1);
                //textBox4.Text = months.ToString("dd'/'MM'/'yyyy") +"\n";
                   kd = vuplata.ToString("N2"); //Ежемесячный платеж
                  osn = (vuplata - procent).ToString("N2"); //Платеж за основной долг
                  percent = procent.ToString("N2"); //Платеж процента
                  ostatok = SumCreditOperation.ToString("N2"); //Основной остаток
                  ItogCreditSumOperation -= vuplata;
                  ItogPlus = Convert.ToDouble(ostatok);
                  Connection.Open();
                  string sql = "insert into kalendar(summ, date_plan, [osn dolg],[po percent], ostatok) Values" +
                  " ('" + kd + "','" + months.ToString("dd'/'MM'/'yyyy") + "','" + osn + "','" + percent + "','" + ostatok + "')";
                  SQLiteCommand command = new SQLiteCommand(sql, Connection);
                  command.ExecuteNonQuery();
                  Connection.Close();
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            double SumCredit = Convert.ToDouble(textBox3.Text); // Сумма кредита
            double InterestRateYear = Convert.ToDouble(procent.Text); // Процентная ставка, ГОДОВАЯ
            double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
            int CreditPeriod = Convert.ToInt32(textBox2.Text); // Срок кредита, переводим в месяцы, если указан в годах
            CreditPeriod *= 12;
            double vuplata = SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod))); // Ежемесячный платеж
            kalendar(vuplata, CreditPeriod, SumCredit, InterestRateYear);
           /* string date = DateTime.Today.ToString("dd'/'MM'/'yyyy");          //дата  
            string group, naznach, percent, vid, sotrud, klient;
            Connection.Open();
            SQLiteCommand cmd5 = Connection.CreateCommand(); // группа риска
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT id_group FROM [Группа риска] WHERE name='" + risk.Text + "'";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SQLiteDataAdapter da5 = new SQLiteDataAdapter(cmd5);
            da5.Fill(dt5);
            group = dt5.Rows[0][0].ToString();            
            SQLiteCommand cmd2 = Connection.CreateCommand(); //целевое назначение
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT id_naznacheniya FROM [Целевое назначение кредита] WHERE name='" + nazn.Text + "'";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd2);
            da2.Fill(dt2);
            naznach=dt2.Rows[0][0].ToString();
            SQLiteCommand cmd3 = Connection.CreateCommand(); // вид кредита
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT id_vida FROM [Вид кредита] WHERE name='" + this.vid.Text + "'";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SQLiteDataAdapter da3 = new SQLiteDataAdapter(cmd3);
            da3.Fill(dt3);            
            vid=dt3.Rows[0][0].ToString();
            SQLiteCommand cmd4 = Connection.CreateCommand(); //проценты
            cmd4.CommandType = CommandType.Text;
            string pr= procent.Text.Replace(",", ".");
            cmd4.CommandText = "SELECT id_stavki FROM [Процентная ставка] WHERE percent='" + pr + "'";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SQLiteDataAdapter da4 = new SQLiteDataAdapter(cmd4);
            da4.Fill(dt4);            
            percent=dt4.Rows[0][0].ToString();
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
             string sql = "insert into Договор(id_group,id_sotrudnik,INN_klienta, id_naznach, id_vida, data_zakluch, srok_pogasheniya, vuplata, id_stavki, cost_naznach, Neustoyka) Values" +
                " ('" + group + "','" + sotrud + "','" + klient + "','" + naznach + "','" + vid + "','" + date + "','" + textBox2.Text + "','" + vuplata + "','" + percent + "','" + textBox3.Text + "','" + textBox1.Text +"')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();*/
        }

        private void Dogovor___Load(object sender, EventArgs e)
        {
           // Connection.Close();
            Connection.Open();  
            SQLiteCommand cmd = Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT INN, LastName FROM Клиенты";
            cmd.ExecuteNonQuery();  
            DataTable dt = new DataTable();
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            da.Fill(dt);    
            foreach (DataRow dr in dt.Rows)
            {
                klientcmb.Items.Add(dr["INN"].ToString()+ " " + dr["LastName"]);
            }
            SQLiteCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT name FROM [Целевое назначение кредита]";
            cmd2.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(cmd2);
            da2.Fill(dt2);
            foreach (DataRow dr2 in dt2.Rows)
            {
                nazn.Items.Add(dr2["name"].ToString());
            }
            SQLiteCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT name FROM [Вид кредита]";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SQLiteDataAdapter da3 = new SQLiteDataAdapter(cmd3);
            da3.Fill(dt3);
            foreach (DataRow dr3 in dt3.Rows)
            {
                vid.Items.Add(dr3["name"].ToString());
            }
            SQLiteCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT percent FROM [Процентная ставка]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SQLiteDataAdapter da4 = new SQLiteDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                procent.Items.Add(dr4["percent"].ToString());
            }
            SQLiteCommand cmd5 = Connection.CreateCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.CommandText = "SELECT name FROM [Группа риска]";
            cmd5.ExecuteNonQuery();
            DataTable dt5 = new DataTable();
            SQLiteDataAdapter da5 = new SQLiteDataAdapter(cmd5);
            da5.Fill(dt5);
            foreach (DataRow dr5 in dt5.Rows)
            {
                risk.Items.Add(dr5["name"]);
            }
            SQLiteCommand cmd6 = Connection.CreateCommand();
            cmd6.CommandType = CommandType.Text;
            cmd6.CommandText = "SELECT Сотрудники.id, Сотрудники.LastName, Должность.name  FROM Должность INNER JOIN Сотрудники ON Должность.id_dolznosti = Сотрудники.id";
            cmd6.ExecuteNonQuery();
            DataTable dt6 = new DataTable();
            SQLiteDataAdapter da6 = new SQLiteDataAdapter(cmd6);
            da6.Fill(dt6);
            foreach (DataRow dr6 in dt6.Rows)
            {
                sotr.Items.Add(dr6["id"].ToString()+ " " + dr6["LastName"] + " " + dr6["name"]);
            }
            Connection.Close();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (sotr.Text)
            {
                case "0": sotridnik frm2 = new sotridnik(); frm2.Show(); break;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (klientcmb.Text)
            {
                case "0": klient frm2 = new klient(); frm2.Show(); break;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (procent.Text)
            {
            //    case "0": сделать ++ через месседж бокс break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (nazn.Text)
            {
                //    case "0": сделать ++ через месседж бокс break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
