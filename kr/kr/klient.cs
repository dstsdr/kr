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
        public bool check = true;

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
            string a = "";
            if (where.Text != "") { where.BackColor = Color.White; }
            else { where.BackColor = Color.DarkGray; a += "где выдан паспорт\n"; check = false; }
            if (city.Text != "") { city.BackColor = Color.White; }
            else { city.BackColor = Color.DarkGray; a += "город прописки\n"; check = false; }
            if (street.Text != "") { street.BackColor = Color.White; }
            else { street.BackColor = Color.DarkGray; a += "улица прописки\n"; check = false; }
            if (nomer.Text != "") { nomer.BackColor = Color.White; }
            else { nomer.BackColor = Color.DarkGray; a += "номер дома прописки\n"; check = false; }

            if (job.Text != "") { job.BackColor = Color.White; }
            else { job.BackColor = Color.DarkGray; a += "работа\n"; check = false; }
            if (stash.Text != "") { stash.BackColor = Color.White; }
            else { stash.BackColor = Color.DarkGray; a += "стаж\n"; check = false; }
            if (dohod.Text != "") { dohod.BackColor = Color.White; }
            else { dohod.BackColor = Color.DarkGray; a += "доход\n"; check = false; }

            if (schet.Text != "") { schet.BackColor = Color.White; }
            else { schet.BackColor = Color.DarkGray; a += "счет\n"; check = false; }
            if (ssuda.Text != "") { ssuda.BackColor = Color.White; }
            else { ssuda.BackColor = Color.DarkGray; a += "ссудный счет\n"; check = false; }

            if (INN.Text != "") { INN.BackColor = Color.White; }
            else { INN.BackColor = Color.DarkGray; a += "ИНН\n"; check = false; }
            if (phone.Text != "") { phone.BackColor = Color.White; }
            else { phone.BackColor = Color.DarkGray; a += "телефон\n"; check = false; }

            if (LastName.Text != "") { LastName.BackColor = Color.White; }
            else { LastName.BackColor = Color.DarkGray; a += "фамилия\n"; check = false; }
            if (nomer.Text != "") { nomer.BackColor = Color.White; }
            else { nomer.BackColor = Color.DarkGray; a += "номер паспорта\n"; check = false; }
            if (FirstName.Text != "") { FirstName.BackColor = Color.White; }
            else { FirstName.BackColor = Color.DarkGray; a += "имя\n"; check = false; }
            if (otchestvo.Text != "") { otchestvo.BackColor = Color.White; }
            else { otchestvo.BackColor = Color.DarkGray; a += "отчество\n"; check = false; }
            if (seriya.Text != "") { seriya.BackColor = Color.White; }
            else { seriya.BackColor = Color.DarkGray; a += "серия паспорта\n"; check = false; }

            if (kem.Text != "") { kem.BackColor = Color.White; }
            else { kem.BackColor = Color.DarkGray; a += "кем выдан паспорт\n"; check = false; }
            if (podrazdel.Text != "") { podrazdel.BackColor = Color.White; }
            else { podrazdel.BackColor = Color.DarkGray; a += "подразделение паспорта\n"; check = false; }
            if (check == false)
            {
                MessageBox.Show("Для обновления записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
            {
                string date = dateTimePicker1.Text;
                Connection.Open();

                string sql = "insert into Клиенты([Серия паспорта],[Номер паспорта],Фамилия, Имя, Отчество, Телефон, ИНН, Доход, Счет, Работа, Стаж, [Улица прописки], " +
                    "[Город прописки], [Дом прописки], [Ссудный счет], Подразделение, [Кем выдан], [Где выдан], [Дата выдачи]) Values" +
                    " ('" + seriya.Text + "','" + nomer.Text + "','" + LastName.Text + "','" + FirstName.Text + "','" + otchestvo.Text + "','" + phone.Text + "','" + INN.Text + "','" + dohod.Text + "','" + schet.Text + "','" + job.Text + "','" + stash.Text + "','" + street.Text + "','" + city.Text + "','"
                    + home.Text + "','" + ssuda.Text + "','" + podrazdel.Text + "','" + kem.Text + "','" + where.Text + "','" + date + "')";
                SqlCommand command = new SqlCommand(sql, Connection);
                command.ExecuteNonQuery();
                if (command.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Возникла ошибка при добавлении клиента");
                }
                else
                {
                    MessageBox.Show("Клиент добавлен");
                }
                Connection.Close();
            }
        }

        private void klient_Load(object sender, EventArgs e)
        {
         //   dateTimePicker1.CustomFormat = "MM/dd/yyyy";
         //   dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }

        private void save_Click(object sender, EventArgs e)
        {
            string a = "";
            if (where.Text != "") { where.BackColor = Color.White; }
            else { where.BackColor = Color.DarkGray; a += "где выдан паспорт\n"; check = false; }
            if (city.Text != "") { city.BackColor = Color.White; }
            else { city.BackColor = Color.DarkGray; a += "город прописки\n"; check = false; }
            if (street.Text != "") { street.BackColor = Color.White; }
            else { street.BackColor = Color.DarkGray; a += "улица прописки\n"; check = false; }
            if (nomer.Text != "") { nomer.BackColor = Color.White; }
            else { nomer.BackColor = Color.DarkGray; a += "номер дома прописки\n"; check = false; }

            if (job.Text != "") { job.BackColor = Color.White; }
            else { job.BackColor = Color.DarkGray; a += "работа\n"; check = false; }
            if (stash.Text != "") { stash.BackColor = Color.White; }
            else { stash.BackColor = Color.DarkGray; a += "стаж\n"; check = false; }
            if (dohod.Text != "") { dohod.BackColor = Color.White; }
            else { dohod.BackColor = Color.DarkGray; a += "доход\n"; check = false; }

            if (schet.Text != "") { schet.BackColor = Color.White; }
            else { schet.BackColor = Color.DarkGray; a += "счет\n"; check = false; }
            if (ssuda.Text != "") { ssuda.BackColor = Color.White; }
            else { ssuda.BackColor = Color.DarkGray; a += "ссудный счет\n"; check = false; }

            if (INN.Text != "") { INN.BackColor = Color.White; }
            else { INN.BackColor = Color.DarkGray; a += "ИНН\n"; check = false; }
            if (phone.Text != "") { phone.BackColor = Color.White; }
            else { phone.BackColor = Color.DarkGray; a += "телефон\n"; check = false; }

            if (LastName.Text != "") { LastName.BackColor = Color.White; }
            else { LastName.BackColor = Color.DarkGray; a += "фамилия\n"; check = false; }
             if (nomer.Text!="") { nomer.BackColor = Color.White; }
            else { nomer.BackColor = Color.DarkGray; a += "номер паспорта\n"; check = false; }
            if (FirstName.Text != "") { FirstName.BackColor = Color.White; }
            else { FirstName.BackColor = Color.DarkGray; a += "имя\n"; check = false; }
            if (otchestvo.Text != "") { otchestvo.BackColor = Color.White; }
            else { otchestvo.BackColor = Color.DarkGray; a += "отчество\n"; check = false; }
            if (seriya.Text != "") { seriya.BackColor = Color.White; }
            else { seriya.BackColor = Color.DarkGray; a += "серия паспорта\n"; check = false; }
           
            if (kem.Text != "") { kem.BackColor = Color.White; }
            else { kem.BackColor = Color.DarkGray; a += "кем выдан паспорт\n"; check = false; }
            if (podrazdel.Text != "") { podrazdel.BackColor = Color.White; }
            else { podrazdel.BackColor = Color.DarkGray; a += "подразделение паспорта\n"; check = false; }
            if (check == false)
            {
                MessageBox.Show("Для обновления записи заполните/выберите следующие поля:" + a);
            }
            if (check == true)
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
                else
                {
                    MessageBox.Show("Данные изменены");
                }
                Connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void LastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (LastName.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }

        }

        private void FirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (FirstName.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void otchestvo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (otchestvo.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void city_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (city.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void street_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void street_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (street.Text.Length == 0)
            {
                string BigFirstLetter = e.KeyChar.ToString().ToUpper();
                e.KeyChar = BigFirstLetter[0];
            }
        }

        private void INN_KeyPress(object sender, KeyPressEventArgs e)
        {
            INN.MaxLength = 12;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void seriya_KeyPress(object sender, KeyPressEventArgs e)
        {
            seriya.MaxLength = 4;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void nomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            nomer.MaxLength = 6;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void schet_KeyPress(object sender, KeyPressEventArgs e)
        {
            schet.MaxLength = 20;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void ssuda_KeyPress(object sender, KeyPressEventArgs e)
        {
            ssuda.MaxLength = 10;
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void stash_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void dohod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;

            }
        }

        private void podrazdel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;

            }
        }
    }
}
