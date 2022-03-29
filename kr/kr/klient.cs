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
    public partial class klient : Form
    {
        public klient()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Text;
            Connection.Open();
            
            string sql = "insert into Клиенты([Серия паспорта],[Номер паспорта],Фамилия, Имя, Отчество, Телефон, ИНН, Доход, Счет, Работа, Стаж, [Улица прописки], " +
                "[Город прописки], [Дом прописки], [Ссудный счет], Подразделение, [Кем выдан], [Где выдан], [Дата выдачи]) Values" +
                " ('" + seriya.Text + "','" + nomer.Text + "','" + LastName.Text + "','" + FirstName.Text + "','" + otchestvo.Text + "','" + phone.Text + "','" + INN.Text + "','" + dohod.Text + "','" + schet.Text + "','" + job.Text + "','" + stash.Text + "','" + street.Text + "','" + city.Text + "','" 
                + home.Text + "','" + ssuda.Text + "','" + podrazdel.Text + "','" + kem.Text + "','" + where.Text + "','" + date + "')";
            SqlCommand command = new SqlCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void klient_Load(object sender, EventArgs e)
        {
         //   dateTimePicker1.CustomFormat = "MM/dd/yyyy";
         //   dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void save_Click(object sender, EventArgs e)
        {
            Connection.Open();
            /*
             * update Клиенты SET Фамилия, Имя, Отчество, [Серия паспорта], [Номер паспорта], [Кем выдан], [Где выдан], [Дата выдачи], Телефон, 
ИНН, Работа, Стаж, Доход, [Город прописки], [Улица прописки], [Дом прописки], Счет, [Ссудный счет], Подразделение WHERE*/

            SqlCommand command = new SqlCommand("update Клиенты SET Фамилия=@fio, Имя=@name, Отчество=@otchestvo, [Серия паспорта]=@seria, [Номер паспорта]=@nomer, " +
                "[Кем выдан]=@kem, [Где выдан]=@where, [Дата выдачи]=@date, Телефон=@phone, Работа=@job, Стаж=@stash, Доход=@doxod, [Город прописки]=@city, " +
                "[Улица прописки]=@street, [Дом прописки]=@home, Счет=@schet, [Ссудный счет]=@ssuda, Подразделение=@podrazdel" +
                " WHERE  ИНН=@INN", Connection);
            command.Parameters.AddWithValue("@fio", LastName.Text);
            command.Parameters.AddWithValue("@name", FirstName.Text);
            command.Parameters.AddWithValue("@otchestvo", otchestvo.Text);
            command.Parameters.AddWithValue("@seria", seriya.Text);
            command.Parameters.AddWithValue("@nomer", nomer.Text);
            command.Parameters.AddWithValue("@kem", kem.Text);
            command.Parameters.AddWithValue("@where", where.Text);
            command.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString());
            command.Parameters.AddWithValue("@phone", phone.Text);
            command.Parameters.AddWithValue("@INN", INN.Text);
            command.Parameters.AddWithValue("@job", job.Text);
            command.Parameters.AddWithValue("@stash", stash.Text);
            command.Parameters.AddWithValue("@doxod", dohod.Text);
            command.Parameters.AddWithValue("@city", city.Text);
            command.Parameters.AddWithValue("@street", street.Text);
            command.Parameters.AddWithValue("@home", home.Text);
            command.Parameters.AddWithValue("@schet", schet.Text);
            command.Parameters.AddWithValue("@ssuda", ssuda.Text);
            command.Parameters.AddWithValue("@podrazdel", podrazdel.Text);
            if (command.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Возникла ошибка при изменении");
            }
            Connection.Close();
        }
    }
}
