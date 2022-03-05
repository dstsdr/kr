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
    public partial class Dogovor : Form
    {
        public Dogovor()
        {
            InitializeComponent();
        }
        SQLiteConnection Connection = new SQLiteConnection("Data Source=kredit.db");
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
            var city = 
          /*var d=
          var month =
          var year =
          var dir = 
          var pd =
          var pmon =
          var pyear =
          var klientFIO =
          var where = 
          var kem =
          var raz=
          var dv=
          var monthv=
          var yearv=
          var naznach=
          var ssudSchet=
          var summa = 
          var schet =
          var adresBanka=
          ver korS=
          var BIK=
          var klentAdres=
          var pasport=
          var FIOdira=
          var FIOklienta=
          var bank=
            */
        }
    }
}
