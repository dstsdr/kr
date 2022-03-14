﻿using System;
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
                SqlCommand command = new SqlCommand("insert into [Календарь]([Дата запланированная], [Основной долг], [Проценты], [Остаток]) Values" +
                  " (@date, @OSN, @PERCENT, @OST)", Connection);
                command.Parameters.AddWithValue("@date", months.ToString("dd'.'MM'.'yyyy"));
                command.Parameters.AddWithValue("@OSN", osn);
                command.Parameters.AddWithValue("@PERCENT", percent);
                command.Parameters.AddWithValue("@OST", ostatok);
                command.ExecuteNonQuery();
                  Connection.Close();
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            string vozvrat = dateTimePicker1.Value.ToString();
            vozvrat = vozvrat.Substring(0, vozvrat.IndexOf(' ') + 1);
            int CreditPeriod = ((dateTimePicker1.Value.Year - DateTime.Today.Year) * 12) + dateTimePicker1.Value.Month - DateTime.Today.Month;
            double SumCredit = Convert.ToDouble(textBox3.Text); // Сумма кредита
            double InterestRateYear = Convert.ToDouble(procent.Text); // Процентная ставка, ГОДОВАЯ
            double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
           // int CreditPeriod = Convert.ToInt32(textBox2.Text); // Срок кредита, переводим в месяцы, если указан в годах
            CreditPeriod *= 12;
            decimal vuplata = Convert.ToDecimal(SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod)))); // Ежемесячный платеж
            kalendar(Convert.ToDouble(vuplata), CreditPeriod, SumCredit, InterestRateYear);

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
            naznach=dt2.Rows[0][0].ToString();
            SqlCommand cmd3 = Connection.CreateCommand(); // вид кредита
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT [Код] FROM [Вид кредита] WHERE [Вид]='" + this.vid.Text + "'";
            cmd3.ExecuteNonQuery();
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            da3.Fill(dt3);            
            vid=dt3.Rows[0][0].ToString();
            SqlCommand cmd4 = Connection.CreateCommand(); //проценты
            cmd4.CommandType = CommandType.Text;
            string pr= procent.Text.Replace(",", ".");
            cmd4.CommandText = "SELECT [№] FROM [Процентная ставка] WHERE [%]='" + pr + "'";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
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
          /*   string sql = "insert into Договор([№ группы риска],[№ сотрудника],[ИНН клиента], [№ назначения], [№ вида], [дата заключения], [Срок погашения], [Ежемесячный платеж], [№ ставки], [Сумма], [Неустойка]) Values" +
                " ('" + group + "','" + sotrud + "','" + klient + "','" + naznach + "','" + vid + "','" + date + "','" + vozvrat + "','" + vuplata + "','" + percent + "','" + textBox3.Text + "','" + textBox1.Text +"')";
           */ SqlCommand command = new SqlCommand("insert into Договор([№ группы риска],[№ сотрудника],[ИНН клиента], [№ назначения], [№ вида], [дата заключения], [Срок погашения], [Ежемесячный платеж], [№ ставки], [Сумма], [Неустойка]) Values" +
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
            Connection.Close();
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
               case "0":  percent frm2 = new percent(); frm2.Show(); break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (nazn.Text)
            {
                   case "0":  naznach frm2 = new naznach(); frm2.Show(); break;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
