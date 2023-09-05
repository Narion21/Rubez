namespace Rubez
{
    partial class Options
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
            this.ipTb = new System.Windows.Forms.TextBox();
            this.portTb = new System.Windows.Forms.TextBox();
            this.loginTb = new System.Windows.Forms.TextBox();
            this.passwordTb = new System.Windows.Forms.TextBox();
            this.connToDbButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dbCb = new System.Windows.Forms.ComboBox();
            this.tbCb = new System.Windows.Forms.ComboBox();
            this.showTableListButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboDataTb = new System.Windows.Forms.TextBox();
            this.comboTableTb = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ipTb
            // 
            this.ipTb.Location = new System.Drawing.Point(12, 12);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(100, 20);
            this.ipTb.TabIndex = 0;
            // 
            // portTb
            // 
            this.portTb.Location = new System.Drawing.Point(12, 39);
            this.portTb.Name = "portTb";
            this.portTb.Size = new System.Drawing.Size(100, 20);
            this.portTb.TabIndex = 1;
            // 
            // loginTb
            // 
            this.loginTb.Location = new System.Drawing.Point(12, 66);
            this.loginTb.Name = "loginTb";
            this.loginTb.Size = new System.Drawing.Size(100, 20);
            this.loginTb.TabIndex = 2;
            // 
            // passwordTb
            // 
            this.passwordTb.Location = new System.Drawing.Point(12, 93);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.Size = new System.Drawing.Size(100, 20);
            this.passwordTb.TabIndex = 3;
            // 
            // connToDbButton
            // 
            this.connToDbButton.Location = new System.Drawing.Point(12, 120);
            this.connToDbButton.Name = "connToDbButton";
            this.connToDbButton.Size = new System.Drawing.Size(100, 36);
            this.connToDbButton.TabIndex = 4;
            this.connToDbButton.Text = "Подключиться к базе данных";
            this.connToDbButton.UseVisualStyleBackColor = true;
            this.connToDbButton.Click += new System.EventHandler(this.connButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Айпи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Порт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Имя пользователя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Пароль";
            // 
            // dbCb
            // 
            this.dbCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbCb.FormattingEnabled = true;
            this.dbCb.Location = new System.Drawing.Point(12, 162);
            this.dbCb.Name = "dbCb";
            this.dbCb.Size = new System.Drawing.Size(121, 21);
            this.dbCb.TabIndex = 9;
            // 
            // tbCb
            // 
            this.tbCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbCb.FormattingEnabled = true;
            this.tbCb.Location = new System.Drawing.Point(10, 232);
            this.tbCb.Name = "tbCb";
            this.tbCb.Size = new System.Drawing.Size(121, 21);
            this.tbCb.TabIndex = 10;
            // 
            // showTableListButton
            // 
            this.showTableListButton.Location = new System.Drawing.Point(12, 189);
            this.showTableListButton.Name = "showTableListButton";
            this.showTableListButton.Size = new System.Drawing.Size(100, 37);
            this.showTableListButton.TabIndex = 11;
            this.showTableListButton.Text = "Показать список таблиц";
            this.showTableListButton.UseVisualStyleBackColor = true;
            this.showTableListButton.Click += new System.EventHandler(this.showTableListButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 273);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 48);
            this.button1.TabIndex = 15;
            this.button1.Text = "Сохранить настройки";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // comboDataTb
            // 
            this.comboDataTb.Enabled = false;
            this.comboDataTb.Location = new System.Drawing.Point(159, 162);
            this.comboDataTb.Name = "comboDataTb";
            this.comboDataTb.Size = new System.Drawing.Size(100, 20);
            this.comboDataTb.TabIndex = 17;
            // 
            // comboTableTb
            // 
            this.comboTableTb.Enabled = false;
            this.comboTableTb.Location = new System.Drawing.Point(159, 225);
            this.comboTableTb.Name = "comboTableTb";
            this.comboTableTb.Size = new System.Drawing.Size(100, 20);
            this.comboTableTb.TabIndex = 18;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 359);
            this.Controls.Add(this.comboTableTb);
            this.Controls.Add(this.comboDataTb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.showTableListButton);
            this.Controls.Add(this.tbCb);
            this.Controls.Add(this.dbCb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connToDbButton);
            this.Controls.Add(this.passwordTb);
            this.Controls.Add(this.loginTb);
            this.Controls.Add(this.portTb);
            this.Controls.Add(this.ipTb);
            this.Name = "Options";
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_Closed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button connToDbButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button showTableListButton;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox dbCb;
        public System.Windows.Forms.ComboBox tbCb;
        public System.Windows.Forms.TextBox ipTb;
        public System.Windows.Forms.TextBox portTb;
        public System.Windows.Forms.TextBox loginTb;
        public System.Windows.Forms.TextBox passwordTb;
        public System.Windows.Forms.TextBox comboDataTb;
        public System.Windows.Forms.TextBox comboTableTb;
    }
}