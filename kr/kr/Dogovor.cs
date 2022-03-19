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
using Word = Microsoft.Office.Interop.Word;

namespace kr
{
    public partial class Dogovor : Form
    {
        public Dogovor()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№] AS [№ договора],  Клиенты.Фамилия AS[Клиент], [Целевое назначение кредита].[Название] AS[Цель], " +
                "Договор.[Сумма] AS[Сумма], [Процентная ставка].[%]  AS[%], Договор.[Неустойка] AS[Неустойка], Договор.[дата заключения] AS[Дата заключения]," +
                " Договор.[Срок погашения] AS[Дата погашения], [Вид кредита].[Вид] AS[Вид], Договор.[Ежемесячный платеж] AS[Ежемесячная выплата], [Группа риска].Группа AS[Группа риска]," +
                " Сотрудники.Фамилия AS[Сотрудник], Банк.Название FROM Договор Inner join[Вид кредита] ON[Вид кредита].[Код] = Договор.[№ вида] Inner join[Группа риска] " +
                "ON[Группа риска].[№] = Договор.[№ группы риска] Inner join Клиенты ON Клиенты.ИНН = Договор.[ИНН клиента] Inner join[Целевое назначение кредита] " +
                "ON[Целевое назначение кредита].[№] = Договор.[№ назначения] Inner join(Сотрудники inner join Банк ON Сотрудники.БИК = Банк.БИК) " +
                "ON Сотрудники.[№] = Договор.[№ сотрудника] Inner join[Процентная ставка] ON[Процентная ставка].[№] = Договор.[№ ставки]", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
            int rows = dataGridView1.Rows.Count - 1;
            label1.Text = "Количество записей " + rows.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dogovor__ frm2 = new Dogovor__(); frm2.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Клиенты.* FROM Клиенты INNER JOIN Договор ON Клиенты.ИНН = Договор.[ИНН клиента] WHERE Договор.[№]='" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество, Сотрудники.Телефон, Сотрудники.[Серия паспорта], " +
                "Сотрудники.[Номер паспорта], Должность.Название FROM(Должность INNER JOIN Сотрудники ON Должность.[№] = Сотрудники.Должность) INNER JOIN Договор" +
                " ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[№]='" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Банк.* FROM(Банк INNER JOIN Сотрудники ON Банк.БИК = Сотрудники.БИК) INNER JOIN Договор ON " +
                "Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[№]='" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }
        private readonly string document = @"C:\Users\1652090\OneDrive\Рабочий стол\dogovor-potrebitelskogo-kredita.doc";
        private void button2_Click(object sender, EventArgs e)
        {
             var wordApp = new Word.Application();
             wordApp.Visible=false;
             var wordDocument = wordApp.Documents.Open(document);
             string s = dataGridView1.CurrentCell.Value.ToString();
             Connection.Open();
             string sqlExpression = "SELECT Банк.Название, Банк.[Кор. счет], Банк.БИК, Банк.Город, Банк.Улица, Банк.Дом, Сотрудники.Фамилия, " +
                "Сотрудники.Имя, Сотрудники.Отчество, Клиенты.Фамилия, Клиенты.Имя, Клиенты.Отчество, Клиенты.[Серия паспорта], Клиенты.[Номер паспорта], " +
                "Клиенты.[Дата выдачи], Клиенты.[Кем выдан], Клиенты.[Где выдан], Клиенты.Подразделение, Клиенты.[Ссудный счет], Клиенты.Счет," +
                " Клиенты.[Город прописки], Клиенты.[Улица прописки], Клиенты.[Дом прописки], Договор.[№], Договор.[Срок погашения] " +
                "FROM Договор Inner join Клиенты ON Клиенты.ИНН = Договор.[ИНН клиента] Inner join(Сотрудники inner join Банк ON Сотрудники.БИК= Банк.БИК) ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE  Договор.[№]= '" + s + "' AND Сотрудники.[Должность] = 1";
            SqlCommand command = new SqlCommand(sqlExpression, Connection);
             SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) //данные из банка и договора
                {
                var name = reader.GetValue(0);
                var korS = reader.GetValue(1);
                var BIK = reader.GetValue(2);
                var city = reader.GetValue(3);
                var street = reader.GetValue(4);
                var hn = reader.GetValue(5);
                string summa = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string naznach = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string percent = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string adres = "г. "+city.ToString() + ", ул. "+ street.ToString() + ", "+ hn.ToString();
                string date = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                date = date.Substring(0, date.IndexOf(' ') + 1);
                string[] words = date.Split('.');
                string ch = words[0];
                string month = words[1];
                string year = words[2];
                if (month == "01") month = "января";
                if (month == "02") month = "февраля";
                if (month == "03") month = "марта";
                if (month == "04") month = "апреля";
                if (month == "05") month = "мая";
                if (month == "06") month = "июня";
                if (month == "07") month = "июля";
                if (month == "08") month = "августа";
                if (month == "09") month = "сентября";
                if (month == "10") month = "октября";
                if (month == "11") month = "ноября";
                if (month == "12") month = "декабря";
                var ln = reader.GetValue(6).ToString(); //сотрудник
                var fn = reader.GetValue(7).ToString();
                var ot = reader.GetValue(8).ToString();
                string dir = ln + " " + fn.Substring(0, 1) + ". " + ot.Substring(0, 1) + ".";
                string fio = ln + " " + fn + " " + ot;
               //клиенты
                var kLn = reader.GetValue(9).ToString();
                var kFn = reader.GetValue(10).ToString();
                var kOt = reader.GetValue(11).ToString();
                var seriya = reader.GetValue(12).ToString();
                var nomer = reader.GetValue(13).ToString();
                var dateP = reader.GetValue(14).ToString();
                var kemP = reader.GetValue(15).ToString();
                var where = reader.GetValue(16).ToString();
                var podrazdel = reader.GetValue(17).ToString();
                var ssuda = reader.GetValue(18).ToString();
                ssuda = ssuda.Substring(0, ssuda.IndexOf(' '));
                var schet = reader.GetValue(19).ToString();
                schet = schet.Substring(0, schet.IndexOf(' '));
                var kcity = reader.GetValue(20).ToString();
                var kstreet = reader.GetValue(21).ToString();
                var khn = reader.GetValue(22).ToString();
                var dogovornumber = reader.GetValue(23).ToString();
                var pogashen = reader.GetValue(24).ToString();

                pogashen=pogashen.Substring(0, pogashen.IndexOf(' ') + 1);
                string[] srok = pogashen.Split('.');
                string chZ = srok[0];
                string monthZ = srok[1];
                string yearZ = srok[2];
                if (monthZ == "01") monthZ = "января";
                if (monthZ == "02") monthZ = "февраля";
                if (monthZ == "03") monthZ = "марта";
                if (monthZ == "04") monthZ = "апреля";
                if (monthZ == "05") monthZ = "мая";
                if (monthZ == "06") monthZ = "июня";
                if (monthZ == "07") monthZ = "июля";
                if (monthZ == "08") monthZ = "августа";
                if (monthZ == "09") monthZ = "сентября";
                if (monthZ == "10") monthZ = "октября";
                if (monthZ == "11") monthZ = "ноября";
                if (monthZ == "12") monthZ = "декабря";
                ReplaceWordStub("{dv}", chZ, wordDocument);
                ReplaceWordStub("{monthv}", monthZ, wordDocument);
                ReplaceWordStub("{yearv}", yearZ, wordDocument);
                string adresK = "г. " + kcity.ToString() + ", ул. " + kstreet.ToString() + ", " + khn.ToString();
                dateP = dateP.Substring(0, dateP.IndexOf(' ') + 1);
                string[] pass = dateP.Split('.');
                string chP = pass[0];
                string monthP = pass[1];
                string yearP = pass[2];
                if (monthP == "01") monthP = "января";
                if (monthP == "02") monthP = "февраля";
                if (monthP == "03") monthP = "марта";
                if (monthP == "04") monthP = "апреля";
                if (monthP == "05") monthP = "мая";
                if (monthP == "06") monthP = "июня";
                if (monthP == "07") monthP = "июля";
                if (monthP == "08") monthP = "августа";
                if (monthP == "09") monthP = "сентября";
                if (monthP == "10") monthP= "октября";
                if (monthP == "11") monthP = "ноября";
                if (monthP == "12") monthP = "декабря";
                string klient = kLn + " " + kFn.Substring(0, 1) + "." + kOt.Substring(0, 1) + ".";
                string kfio = kLn + " " + kFn + kOt;
                string pasport = seriya + " " + nomer;
                ReplaceWordStub("{pd}", chP, wordDocument);
                ReplaceWordStub("{pmon}", monthP, wordDocument);
                ReplaceWordStub("{pyear}", yearP, wordDocument);
                ReplaceWordStub("{kem}", kemP, wordDocument);
                ReplaceWordStub("{where}", where, wordDocument);
                ReplaceWordStub("{raz}", podrazdel, wordDocument);
                ReplaceWordStub("{klientAdres}", adresK, wordDocument);
                ReplaceWordStub("{pasport}", pasport, wordDocument);
                ReplaceWordStub("{klientFIO}", klient, wordDocument);
                ReplaceWordStub("{FIOklienta}", kfio, wordDocument);
                ReplaceWordStub("{pasport}", pasport, wordDocument);

                ReplaceWordStub("{dir}", dir, wordDocument);
                ReplaceWordStub("{FIOdira}", fio, wordDocument);
                ReplaceWordStub("{number}", dogovornumber, wordDocument);



                ReplaceWordStub("{ssudSchet}", ssuda, wordDocument);
                ReplaceWordStub("{schet}", schet, wordDocument);
                ReplaceWordStub("{city}", city.ToString(), wordDocument);
                ReplaceWordStub("{bank}", name.ToString(), wordDocument);
                ReplaceWordStub("{bank2}", name.ToString(), wordDocument);
                ReplaceWordStub("{korS}", korS.ToString(), wordDocument);
                ReplaceWordStub("{BIK}", BIK.ToString(), wordDocument);
                ReplaceWordStub("{adresBanka}", adres.ToString(), wordDocument);
                ReplaceWordStub("{number}", s, wordDocument);
                ReplaceWordStub("{summa}", summa, wordDocument);
                ReplaceWordStub("{naznach}", naznach, wordDocument);
                ReplaceWordStub("{percent}", percent, wordDocument);
                ReplaceWordStub("{d}", ch, wordDocument);
                ReplaceWordStub("{month}", month, wordDocument);
                ReplaceWordStub("{year}", year, wordDocument);
                Connection.Close();
            } 
            wordDocument.SaveAs(@"C:\Users\1652090\OneDrive\Рабочий стол\" + s + "");
            wordApp.Visible = true;
            Connection.Close();

        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№] AS [№ договора],  Клиенты.Фамилия AS[Клиент], [Целевое назначение кредита].[Название] AS[Цель], " +
               "Договор.[Сумма] AS[Сумма], [Процентная ставка].[%]  AS[%], Договор.[Неустойка] AS[Неустойка], Договор.[дата заключения] AS[Дата заключения]," +
               " Договор.[Срок погашения] AS[Дата погашения], [Вид кредита].[Вид] AS[Вид], Договор.[Ежемесячный платеж] AS[Ежемесячная выплата], [Группа риска].Группа AS[Группа риска]," +
               " Сотрудники.Фамилия AS[Сотрудник], Банк.Название FROM Договор Inner join[Вид кредита] ON[Вид кредита].[Код] = Договор.[№ вида] Inner join[Группа риска] " +
               "ON[Группа риска].[№] = Договор.[№ группы риска] Inner join Клиенты ON Клиенты.ИНН = Договор.[ИНН клиента] Inner join[Целевое назначение кредита] " +
               "ON[Целевое назначение кредита].[№] = Договор.[№ назначения] Inner join(Сотрудники inner join Банк ON Сотрудники.БИК = Банк.БИК) " +
               "ON Сотрудники.[№] = Договор.[№ сотрудника] Inner join[Процентная ставка] ON[Процентная ставка].[№] = Договор.[№ ставки] where Договор.[№] like " +
               "'%" + textBox1.Text + "%' or Клиенты.Фамилия like '%" + textBox1.Text + "%' or [Целевое назначение кредита].[Название] like '%" + textBox1.Text + "%' or Договор.[Сумма] like '%" + textBox1.Text + "%'" +
               " or [Процентная ставка].[%] like '%" + textBox1.Text + "%'or Договор.[Неустойка] like '%" + textBox1.Text + "%'" +
                "or  Договор.[дата заключения] like '%" + textBox1.Text + "%'or Договор.[Срок погашения] like '%" + textBox1.Text + "%'" +
               "or  Сотрудники.Фамилия like '%" + textBox1.Text + "%'or  Банк.Название like '%" + textBox1.Text + "%'" +
               "or  Договор.[Ежемесячный платеж] like '%" + textBox1.Text + "%'or  [Группа риска].Группа like '%" + textBox1.Text + "%'" +
               "or [Вид кредита].[Вид] like '%" + textBox1.Text + "%';", Connection);
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

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }

}


