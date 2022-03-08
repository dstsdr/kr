namespace kr
{
    partial class bank
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.namebox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.korschetbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bikbox = new System.Windows.Forms.TextBox();
            this.numberbox = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.streetbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.citybox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.podrazdelbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 65);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(762, 247);
            this.dataGridView1.TabIndex = 30;
            // 
            // namebox
            // 
            this.namebox.Location = new System.Drawing.Point(104, 339);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(159, 22);
            this.namebox.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 32;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 342);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "Кор.счет";
            // 
            // korschetbox
            // 
            this.korschetbox.Location = new System.Drawing.Point(363, 339);
            this.korschetbox.Name = "korschetbox";
            this.korschetbox.Size = new System.Drawing.Size(159, 22);
            this.korschetbox.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(540, 342);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 36;
            this.label3.Text = "БИК";
            // 
            // bikbox
            // 
            this.bikbox.Location = new System.Drawing.Point(592, 339);
            this.bikbox.Name = "bikbox";
            this.bikbox.Size = new System.Drawing.Size(159, 22);
            this.bikbox.TabIndex = 35;
            // 
            // numberbox
            // 
            this.numberbox.AutoSize = true;
            this.numberbox.Location = new System.Drawing.Point(540, 382);
            this.numberbox.Name = "numberbox";
            this.numberbox.Size = new System.Drawing.Size(100, 16);
            this.numberbox.TabIndex = 42;
            this.numberbox.Text = "Номер здания";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(662, 379);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(89, 22);
            this.textBox4.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 382);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 40;
            this.label5.Text = "Улица";
            // 
            // streetbox
            // 
            this.streetbox.Location = new System.Drawing.Point(363, 379);
            this.streetbox.Name = "streetbox";
            this.streetbox.Size = new System.Drawing.Size(159, 22);
            this.streetbox.TabIndex = 39;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 382);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "Город";
            // 
            // citybox
            // 
            this.citybox.Location = new System.Drawing.Point(104, 379);
            this.citybox.Name = "citybox";
            this.citybox.Size = new System.Drawing.Size(159, 22);
            this.citybox.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 16);
            this.label7.TabIndex = 44;
            this.label7.Text = "Код подразделения";
            // 
            // podrazdelbox
            // 
            this.podrazdelbox.Location = new System.Drawing.Point(156, 422);
            this.podrazdelbox.Name = "podrazdelbox";
            this.podrazdelbox.Size = new System.Drawing.Size(159, 22);
            this.podrazdelbox.TabIndex = 43;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(662, 435);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 29);
            this.button1.TabIndex = 45;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(4, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(636, 22);
            this.textBox1.TabIndex = 46;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(662, 23);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 25);
            this.button2.TabIndex = 47;
            this.button2.Text = "Поиск";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // bank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 476);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.podrazdelbox);
            this.Controls.Add(this.numberbox);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.streetbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.citybox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bikbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.korschetbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.namebox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "bank";
            this.Text = "bank";
            this.Load += new System.EventHandler(this.bank_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox korschetbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bikbox;
        private System.Windows.Forms.Label numberbox;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox streetbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox citybox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox podrazdelbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
    }
}