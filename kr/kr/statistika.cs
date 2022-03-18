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
    public partial class statistika : Form
    {
        public statistika()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) dogovor();
            if (comboBox1.SelectedIndex == 1) platesh();
            if (comboBox1.SelectedIndex == 2) sotrud();


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void sotrud()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], CONCAT (Сотрудники.Фамилия,' ', Сотрудники.Имя, ' ', Сотрудники.Отчество) AS ФИО, count(Договор.[№ сотрудника]) " +
                    "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                    "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);
                /* SELECT CONCAT (Договор.[№ сотрудника], ' ',Сотрудники.Фамилия,' ', Сотрудники.Имя, ' ', Сотрудники.Отчество), count(Договор.[№ сотрудника]) AS [Количество договоров] 
FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения]>'01-01-2021'
group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество
                 */
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], CONCAT (Сотрудники.Фамилия,' ', Сотрудники.Имя, ' ', Сотрудники.Отчество) AS ФИО, count(Договор.[№ сотрудника]) " +
                    "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                    "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], CONCAT (Сотрудники.Фамилия,' ', Сотрудники.Имя, ' ', Сотрудники.Отчество) AS ФИО, count(Договор.[№ сотрудника]) " +
                    "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                    "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
          //  chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
          //  chart1.ChartAreas[0].AxisY.Minimum = Double.NaN;    
            chart1.Series.Add("Series1");
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Series1"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value);
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);

            }
        }
        private void platesh ()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
                var months = DateTime.Today;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Календарь.[Сумма оплаты]) as [Сумма], Календарь.[Дата фактическая], count(Календарь.[Дата фактическая]) as [Количество платежей] FROM Календарь" +
                    " WHERE Календарь.[Дата фактическая]>'" + months + "'group by Календарь.[Дата фактическая]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Календарь.[Сумма оплаты]) as [Сумма], Календарь.[Дата фактическая], count(Календарь.[Дата фактическая]) as [Количество платежей] FROM Календарь" +
                    " WHERE Календарь.[Дата фактическая]>'" + months + "'group by Календарь.[Дата фактическая]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Календарь.[Сумма оплаты]) as [Сумма], Календарь.[Дата фактическая], count(Календарь.[Дата фактическая]) as [Количество платежей] FROM Календарь" +
                    " WHERE Календарь.[Дата фактическая]>'" + months + "'group by Календарь.[Дата фактическая]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
           // chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
           // chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Series1");
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Series1"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value);
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
            }
        }

        private void dogovor ()
        {
            chart1.Series.Clear();
            if (comboBox2.SelectedIndex == 0)
            {
               var months = DateTime.Today;                 
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Договор.Сумма) AS Сумма, Договор.[дата заключения], count(Договор.[дата заключения]) as [Количество заказов] FROM Договор WHERE [дата заключения]>'" + months + "' group by Договор.[дата заключения]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 1)
            {
                var months = DateTime.Today;
                months = months.AddYears(-1);
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Договор.Сумма) AS Сумма, Договор.[дата заключения], count(Договор.[дата заключения]) as [Количество заказов] FROM Договор WHERE [дата заключения]>'" + months.ToString("dd'/'MM'/'yyyy") + "' group by Договор.[дата заключения]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
            if (comboBox2.SelectedIndex == 2)
            {
                var months = DateTime.MinValue;
                Connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT SUM(Договор.Сумма) AS Сумма, Договор.[дата заключения], count(Договор.[дата заключения]) as [Количество заказов] FROM Договор WHERE [дата заключения]>'" + months.ToString("dd'/'MM'/'yyyy") + "' group by Договор.[дата заключения]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
           // chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
           // chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.Series.Add("Series1");
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Series1"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(0, 10), dataGridView1.Rows[i].Cells[0].Value);
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
            }
        }

        private void statistika_Load(object sender, EventArgs e)
        {

        }
    }
}
