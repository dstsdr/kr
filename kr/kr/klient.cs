using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");
        private void button1_Click(object sender, EventArgs e)
        {
            string date = dateTimePicker1.Text;
            Connection.Open();
            
            string sql = "insert into Клиенты(seriya,nomer,LastName, firstName, otchestvo, phone, INN, Doxod, schet, rabota, stash, home_street, " +
                "home_city, home_number, ssudnyi_schet, podrazdelPas, kemP, whereP, dateP) Values" +
                " ('" + seriya.Text + "','" + nomer.Text + "','" + LastName.Text + "','" + FirstName.Text + "','" + otchestvo.Text + "','" + phone.Text + "','" + INN.Text + "','" + dohod.Text + "','" + schet.Text + "','" + job.Text + "','" + stash.Text + "','" + street.Text + "','" + city.Text + "','" 
                + home.Text + "','" + ssuda.Text + "','" + podrazdel.Text + "','" + kem.Text + "','" + where.Text + "','" + date + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        private void klient_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
        }
    }
}
