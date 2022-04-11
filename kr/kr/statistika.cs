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
            //chart1.Series.Add("Количество заказов"); 
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
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], Сотрудники.Фамилия AS Фамилия, count(Договор.[№ сотрудника]) " +
                    "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                    "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);
                
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
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], Сотрудники.Фамилия AS Фамилия, count(Договор.[№ сотрудника]) " +
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
                 SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], Сотрудники.Фамилия AS [Фамилия], count(Договор.[№ сотрудника]) " +
                    "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                    "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);
                /* SqlDataAdapter adapter = new SqlDataAdapter("SELECT Договор.[№ сотрудника] AS [№], CONCAT (Сотрудники.Фамилия,' ', Сотрудники.Имя, ' ', Сотрудники.Отчество) AS ФИО, count(Договор.[№ сотрудника]) " +
                     "AS [Количество договоров] FROM Сотрудники INNER JOIN Договор ON Сотрудники.[№] = Договор.[№ сотрудника] WHERE Договор.[дата заключения] > '" + months + "' " +
                     "group by Договор.[№ сотрудника], Сотрудники.Фамилия, Сотрудники.Имя, Сотрудники.Отчество", Connection);*/
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                dataGridView1.DataSource = ds.Tables[0];
                Connection.Close();
            }
          //  chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
          //  chart1.ChartAreas[0].AxisY.Minimum = Double.NaN;    
            chart1.Series.Add("Количество договоров");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Количество договоров"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Количество договоров"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Количество договоров"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Количество договоров"].Points.AddXY(dataGridView1.Rows[i].Cells[0].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value);
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
                chart1.Series["Количество договоров"].Points[i].Label = dataGridView1.Rows[i].Cells[2].Value.ToString();
                // chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);

            }
            chart1.ChartAreas[0].AxisY.Maximum = (from DataGridViewRow row in dataGridView1.Rows
             where row.Cells[2].FormattedValue.ToString() != string.Empty
             select Convert.ToInt32(row.Cells[2].FormattedValue)).Max();
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
            chart1.Series.Add("Сумма");
            switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Сумма"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(0, 10), dataGridView1.Rows[i].Cells[2].Value);
                chart1.Series["Сумма"].Points[i].Label = dataGridView1.Rows[i].Cells[0].Value.ToString();
                chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
             //   chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);

            }
            chart1.ChartAreas[0].AxisY.Maximum = (from DataGridViewRow row in dataGridView1.Rows
                                                  where row.Cells[2].FormattedValue.ToString() != string.Empty
                                                  select Convert.ToInt32(row.Cells[2].FormattedValue)).Max();
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
            chart1.Series.Add("Сумма");
          /*  switch (comboBox3.SelectedIndex)
            {
                case 0: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie; break;
                case 1: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; break;
                case 3: chart1.Series["Сумма"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; break;
            }*/
            int rows = dataGridView1.Rows.Count - 1;
            for (int i = 0; i != rows; i++)
            {
                chart1.Series["Сумма"].Points.AddXY(dataGridView1.Rows[i].Cells[1].Value.ToString().Substring(0, 10), dataGridView1.Rows[i].Cells[0].Value);
                chart1.Series["Сумма"].Points[i].Label = dataGridView1.Rows[i].Cells[0].Value.ToString();
               chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;
               // chart1.ChartAreas[0].AxisY.Maximum = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
            }
            chart1.ChartAreas[0].AxisY.Maximum = (from DataGridViewRow row in dataGridView1.Rows
             where row.Cells[2].FormattedValue.ToString() != string.Empty
             select Convert.ToInt32(row.Cells[0].FormattedValue)).Max();
        }

        private void statistika_Load(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells[0];
        }
    }
}
