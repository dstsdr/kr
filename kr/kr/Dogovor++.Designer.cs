namespace kr
{
    partial class Dogovor__
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.klientcmb = new System.Windows.Forms.ComboBox();
            this.nazn = new System.Windows.Forms.ComboBox();
            this.vid = new System.Windows.Forms.ComboBox();
            this.procent = new System.Windows.Forms.ComboBox();
            this.risk = new System.Windows.Forms.ComboBox();
            this.sotr = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(455, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Сотрудник";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(267, 303);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(288, 22);
            this.textBox1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "ИНН клиента";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 476);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Назначение";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Вид";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Процентная ставка";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Неустойка";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 364);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Дата возврата";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 9;
            this.label8.Text = "Сумма";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 420);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Группа риска";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(268, 137);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(288, 22);
            this.textBox3.TabIndex = 12;
            // 
            // klientcmb
            // 
            this.klientcmb.FormattingEnabled = true;
            this.klientcmb.ImeMode = System.Windows.Forms.ImeMode.On;
            this.klientcmb.Items.AddRange(new object[] {
            "Добавить клиента"});
            this.klientcmb.Location = new System.Drawing.Point(266, 81);
            this.klientcmb.Name = "klientcmb";
            this.klientcmb.Size = new System.Drawing.Size(289, 24);
            this.klientcmb.TabIndex = 13;
            this.klientcmb.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // nazn
            // 
            this.nazn.FormattingEnabled = true;
            this.nazn.Items.AddRange(new object[] {
            "Добавить назначение"});
            this.nazn.Location = new System.Drawing.Point(198, 467);
            this.nazn.Name = "nazn";
            this.nazn.Size = new System.Drawing.Size(357, 24);
            this.nazn.TabIndex = 14;
            this.nazn.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // vid
            // 
            this.vid.FormattingEnabled = true;
            this.vid.Location = new System.Drawing.Point(267, 191);
            this.vid.Name = "vid";
            this.vid.Size = new System.Drawing.Size(288, 24);
            this.vid.TabIndex = 15;
            // 
            // procent
            // 
            this.procent.FormattingEnabled = true;
            this.procent.Items.AddRange(new object[] {
            "Добавить процентную ставку"});
            this.procent.Location = new System.Drawing.Point(267, 247);
            this.procent.Name = "procent";
            this.procent.Size = new System.Drawing.Size(288, 24);
            this.procent.TabIndex = 16;
            this.procent.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // risk
            // 
            this.risk.FormattingEnabled = true;
            this.risk.Location = new System.Drawing.Point(266, 411);
            this.risk.Name = "risk";
            this.risk.Size = new System.Drawing.Size(289, 24);
            this.risk.TabIndex = 17;
            this.risk.SelectedIndexChanged += new System.EventHandler(this.comboBox5_SelectedIndexChanged);
            // 
            // sotr
            // 
            this.sotr.FormattingEnabled = true;
            this.sotr.Items.AddRange(new object[] {
            "Добавить сотрудника"});
            this.sotr.Location = new System.Drawing.Point(267, 25);
            this.sotr.Name = "sotr";
            this.sotr.Size = new System.Drawing.Size(288, 24);
            this.sotr.TabIndex = 18;
            this.sotr.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(266, 358);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(289, 22);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // Dogovor__
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 572);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.sotr);
            this.Controls.Add(this.risk);
            this.Controls.Add(this.procent);
            this.Controls.Add(this.vid);
            this.Controls.Add(this.nazn);
            this.Controls.Add(this.klientcmb);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Dogovor__";
            this.Text = "Dogovor__";
            this.Load += new System.EventHandler(this.Dogovor___Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox klientcmb;
        private System.Windows.Forms.ComboBox nazn;
        private System.Windows.Forms.ComboBox vid;
        private System.Windows.Forms.ComboBox procent;
        private System.Windows.Forms.ComboBox risk;
        private System.Windows.Forms.ComboBox sotr;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}