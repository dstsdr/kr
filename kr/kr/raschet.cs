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
using System.Data.SqlClient;

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
            switch (comboBox6.SelectedIndex)
            {
                case 0: percent frm2 = new percent(); frm2.Show(); break;
            }
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");

        private void raschet_Load(object sender, EventArgs e)
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
                comboBox6.Items.Add(dr4["%"].ToString());
            }
        }

        private void PaymentScheduleAnnuitet(double SumCredit, double InterestRateYear, double InterestRateMonth, int CreditPeriod) // Метод расчета Аннуитетного платежа
        {
            double Payment = SumCredit * (InterestRateMonth / (1 - Math.Pow(1 + InterestRateMonth, -CreditPeriod))); // Ежемесячный платеж
            double ItogCreditSum = Payment * CreditPeriod; // Итоговая сумма кредита

            label5.Text = Payment.ToString("N2") + " руб"; ; // Выводим в результаты ежемесячный платёж
            label4.Text = (ItogCreditSum).ToString("N2") + " руб"; ; // Выводим в результаты итоговую сумму кредита

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
            label6.Text = (ItogCreditSum - SumCredit + ItogPlus).ToString("N2") + " руб";
            int rows = dataGridView1.Rows.Count - 1;
            label9.Text = "Количество записей " + rows.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // Очищаем таблицу
            double SumCredit = Convert.ToDouble(maskedTextBox1.Text); // Сумма кредита
            double InterestRateYear = Convert.ToDouble(comboBox6.Text); // Процентная ставка, ГОДОВАЯ
            double InterestRateMonth = InterestRateYear / 100 / 12; // Процентная ставка, МЕСЯЧНАЯ
            int CreditPeriod = Convert.ToInt32(maskedTextBox2.Text); // Срок кредита, переводим в месяцы, если указан в годах
            PaymentScheduleAnnuitet(SumCredit, InterestRateYear, InterestRateMonth, CreditPeriod);
            
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void raschet_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
    
}
