using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace kr
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Connection = new SqlConnection(@"Data Source=LAPTOP-862V88EF\SQLEXPRESS;Initial Catalog=kredit;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim()=="" || textBox2.Text.Trim()=="")
            {
                MessageBox.Show("Введите данные для входа", "Erorr");
            }
            else
            {
                string query = "SELECT * FROM Роли WHERE Роль='"+textBox1.Text +"' AND Пароль='"+textBox2.Text+"'";
                Connection.Open();
                SqlCommand cmd = new SqlCommand(query, Connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    if (textBox1.Text=="director")
                    {
                        GUI frm = new GUI();
                        frm.toolStripMenuItem1.Visible= false;  
                        frm.toolStripMenuItem2.Visible=false;   
                        frm.toolStripMenuItem3.Visible=false;   
                        frm.toolStripMenuItem4.Visible=false;
                        frm.toolStripMenuItem6.Visible=false;
                        frm.Show();
                        refinans help = new refinans();
                      //  help.sotrud.Visible = true;
                        help.admin.Visible = false;
                    }
                    if (textBox1.Text == "sotrudnik")
                    {
                        GUI frm = new GUI();
                        frm.toolStripMenuItem5.Visible = false;                        
                        frm.Show();
                        refinans help = new refinans();
                      //  help.sotrud.Visible = true;
                        help.admin.Visible = false;
                    }
                    if (textBox1.Text == "admin")
                    {
                        GUI frm = new GUI();
                        frm.Show();
                        refinans help = new refinans();
                      //  help.sotrud.Visible = false;
                        help.admin.Visible = true;
                    }
                    this.Hide();
                }
                else MessageBox.Show("Неверные данные", "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
