using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kr
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel9.Controls.Clear();
            Dogovor dogovor = new Dogovor() { Dock=DockStyle.Fill, TopLevel=false, TopMost=true};
            dogovor.FormBorderStyle=FormBorderStyle.None;
            this.panel9.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel9.Controls.Clear();
            raschet dogovor = new raschet() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel9.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel9.Controls.Clear();
            kalendar dogovor = new kalendar() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel9.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.panel9.Controls.Clear();
            statistika dogovor = new statistika() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            dogovor.FormBorderStyle = FormBorderStyle.None;
            this.panel9.Controls.Add(dogovor);
            dogovor.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           vid frm2 = new vid(); frm2.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            percent frm2 = new percent(); frm2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            naznach frm2 = new naznach(); frm2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            risk frm2 = new risk(); frm2.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dolznost frm2 = new dolznost(); frm2.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            klientwiew frm2 = new klientwiew(); frm2.Show();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            sotridnik frm2 = new sotridnik(); frm2.Show();

        }
    }
}
