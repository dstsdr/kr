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

namespace kr
{
    public partial class vid : Form
    {
        public vid()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");

        private void vid_Load(object sender, EventArgs e)
        {
            this.вид_кредитаTableAdapter.Fill(this.dataSet1.Вид_кредита);           
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  Connection.Open();
            string sql = "insert into [Вид кредита](name) Values ('" + namebox.Text + "')";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            command.ExecuteNonQuery();
            Connection.Close();*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT Код AS [№], Вид AS [вид] FROM [Вид кредита]", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                вид_кредитаDataGridView.DataSource = ds.Tables[0];
                Connection.Close();
            }
            else
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select Код AS [№], Вид AS [вид] from [Вид кредита] where Код AS [№] like'" + textBox1.Text + "' or Вид  AS [вид] like '" + textBox1.Text + "';", Connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "info");
                вид_кредитаDataGridView.DataSource = ds.Tables[0];
                Connection.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void namebox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void вид_кредитаBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.вид_кредитаBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {

        }
    }
}
