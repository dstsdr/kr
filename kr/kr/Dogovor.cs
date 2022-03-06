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
using Word = Microsoft.Office.Interop.Word;

namespace kr
{
    public partial class Dogovor : Form
    {
        public Dogovor()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection(@"Data Source=C:\Users\1652090\OneDrive\Рабочий стол\kredit.db");
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Договор.id_dogovora AS [№ договора],  Клиенты.LastName AS[Клиент], [Целевое назначение кредита].name AS[Цель], Договор.cost_naznach AS [Сумма], " +
                "[Процентная ставка].percent  AS [%] ,Договор.Neustoyka AS [Неустойка], Договор.data_zakluch AS [Дата заключения], Договор.srok_pogasheniya AS[Срок], [Вид кредита].name AS[Вид], " +
                "Договор.vuplata AS[Ежемесячная выплата], [Группа риска].name AS[Группа риска], Сотрудники.LastName AS[Сотрудник], Банк.name AS[Название банка] " +
                              "FROM(Банк INNER JOIN[Целевое назначение кредита] INNER JOIN Сотрудники INNER JOIN[Процентная ставка] ON Банк.BIK = Сотрудники.BIK) INNER JOIN(Клиенты INNER JOIN([Группа риска] " +
                "INNER JOIN([Вид кредита] INNER JOIN Договор ON[Вид кредита].id_vida = Договор.id_vida) ON[Группа риска].id_group = Договор.id_group) ON Клиенты.INN = Договор.INN_klienta) " +
                "ON([Целевое назначение кредита].id_naznacheniya = Договор.id_naznach) AND(Сотрудники.id = Договор.id_sotrudnik) AND([Процентная ставка].id_stavki = Договор.id_stavki); ", Connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "info");
            dataGridView1.DataSource = ds.Tables[0];
            Connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Клиенты.* FROM Клиенты INNER JOIN Договор ON Клиенты.INN = Договор.INN_klienta WHERE Договор.id_dogovora='" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Сотрудники.LastName, Сотрудники.firstName, Сотрудники.otchestvo, Сотрудники.phone, Сотрудники.seriya, " +
                "Сотрудники.nomer, Должность.name FROM(Должность INNER JOIN Сотрудники ON Должность.id_dolznosti = Сотрудники.id_dolznosti) INNER JOIN Договор" +
                " ON Сотрудники.id = Договор.id_sotrudnik WHERE Договор.id_dogovora = '" + s + "'", Connection);
            DataSet ds2 = new DataSet();
            adapter.Fill(ds2, "info");
            dataGridView2.DataSource = ds2.Tables[0];
            Connection.Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString();
            Connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Банк.* FROM(Банк INNER JOIN Сотрудники ON Банк.BIK = Сотрудники.BIK) INNER JOIN Договор ON " +
                "Сотрудники.id = Договор.id_sotrudnik WHERE Договор.id_dogovora='" + s + "'", Connection);
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
             string sqlExpression = "SELECT Банк.name, Банк.korr_shet, Банк.BIK, Банк.city, Банк.street, Банк.homeNumber, Сотрудники.LastName, Сотрудники.firstName, Сотрудники.otchestvo, Клиенты.LastName, Клиенты.FirstName, Клиенты.Otchestvo," +
                " Клиенты.seriya, Клиенты.nomer, Клиенты.dateP, Клиенты.kemP, Клиенты.whereP, Клиенты.podrazdelPas, Клиенты.ssudnyi_schet, Клиенты.schet, Клиенты.home_city, Клиенты.home_street, Клиенты.home_number FROM(Банк INNER JOIN[Целевое назначение кредита] " +
                "INNER JOIN Сотрудники INNER JOIN[Процентная ставка] ON Банк.BIK = Сотрудники.BIK) INNER JOIN(Клиенты INNER JOIN([Группа риска] INNER JOIN([Вид кредита] INNER JOIN Договор ON[Вид кредита].id_vida = Договор.id_vida) " +
                "ON[Группа риска].id_group = Договор.id_group) ON Клиенты.INN = Договор.INN_klienta) ON([Целевое назначение кредита].id_naznacheniya = Договор.id_naznach) AND(Сотрудники.id = Договор.id_sotrudnik)" +
                " AND([Процентная ставка].id_stavki = Договор.id_stavki) WHERE Договор.id_dogovora= '" + s + "' AND Сотрудники.id_dolznosti = 1";
            SQLiteCommand command = new SQLiteCommand(sqlExpression, Connection);
             SQLiteDataReader reader = command.ExecuteReader();
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
                string[] words = date.Split('/');
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
                var schet = reader.GetValue(19).ToString();
                var kcity = reader.GetValue(20).ToString();
                var kstreet = reader.GetValue(21).ToString();
                var khn = reader.GetValue(22).ToString();
                string adresK = "г. " + kcity.ToString() + ", ул. " + kstreet.ToString() + ", " + khn.ToString();
                string[] pass = date.Split('/');
                string chP = pass[0];
                string monthP = words[1];
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
                string klient = kLn + " " + kFn.Substring(0, 1) + ". " + kOt.Substring(0, 1) + ".";
                string kfio = kLn + " " + kFn + " " + kOt;
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
    }

}


