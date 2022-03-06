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
    public partial class raschet : Form
    {
        public raschet()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");

        private void raschet_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteCommand cmd4 = Connection.CreateCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.CommandText = "SELECT percent FROM [Процентная ставка]";
            cmd4.ExecuteNonQuery();
            DataTable dt4 = new DataTable();
            SQLiteDataAdapter da4 = new SQLiteDataAdapter(cmd4);
            da4.Fill(dt4);
            foreach (DataRow dr4 in dt4.Rows)
            {
                comboBox6.Items.Add(dr4["percent"].ToString());
            }
        }

        private void PaymentScheduleAnnuitet(double SumCredit, double InterestRateYear, double InterestRateMonth, int CreditPeriod) // Метод расчета Аннуитетного платежа
        {
            double Payment = SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod))); // Ежемесячный платеж
            double ItogCreditSum = Payment * CreditPeriod; // Итоговая сумма кредита

            label7.Text = Payment.ToString("N2"); // Выводим в результаты ежемесячный платёж
            label8.Text = (ItogCreditSum).ToString("N2"); // Выводим в результаты итоговую сумму кредита

            // Заполняем график платежей
            double SumCreditOperation = SumCredit;
            double ItogCreditSumOperation = ItogCreditSum;
            double ItogPlus = 0;
            for (int i = 0; i < CreditPeriod; ++i)
            {
                double procent = SumCreditOperation * (InterestRateYear / 100) / 12;
                SumCreditOperation -= Payment - procent;
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = i + 1; //номер месяца
                dataGridView1[1, i].Value = Payment.ToString("N2"); //Ежемесячный платеж
                dataGridView1[2, i].Value = (Payment - procent).ToString("N2"); //Платеж за основной долг
                dataGridView1[3, i].Value = procent.ToString("N2"); //Платеж процента
                dataGridView1[4, i].Value = SumCreditOperation.ToString("N2"); //Основной остаток
                ItogCreditSumOperation -= Payment;
                ItogPlus = Convert.ToDouble(dataGridView1[4, i].Value);
            }
            label9.Text = (ItogCreditSum - SumCredit + ItogPlus).ToString("N2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // Очищаем таблицу
            double SumCredit = Convert.ToDouble(textBox1.Text); // Сумма кредита
            double InterestRateYear = Convert.ToDouble(comboBox6.Text); // Процентная ставка, ГОДОВАЯ
            double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
            int CreditPeriod = Convert.ToInt32(textBox3.Text); // Срок кредита, переводим в месяцы, если указан в годах
            CreditPeriod *= 12;
            PaymentScheduleAnnuitet(SumCredit, InterestRateYear, InterestRateMonth, CreditPeriod);
            
        }
    }
    
}
