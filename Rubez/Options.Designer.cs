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
            this.saveButton = new System.Windows.Forms.Button();
            this.comboDataTb = new System.Windows.Forms.TextBox();
            this.comboTableTb = new System.Windows.Forms.TextBox();
            this.columnNameCb = new System.Windows.Forms.ComboBox();
            this.comboColumnTb = new System.Windows.Forms.TextBox();
            this.showColumnListButton = new System.Windows.Forms.Button();
            this.dataTypeCheckBox = new System.Windows.Forms.CheckBox();
            this.passTb = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.passErrLb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipTb
            // 
            this.ipTb.Location = new System.Drawing.Point(12, 12);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(100, 20);
            this.ipTb.TabIndex = 0;
            this.ipTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipTb_KeyPress);
            // 
            // portTb
            // 
            this.portTb.Location = new System.Drawing.Point(12, 39);
            this.portTb.Name = "portTb";
            this.portTb.Size = new System.Drawing.Size(100, 20);
            this.portTb.TabIndex = 1;
            this.portTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.portTb_KeyPress);
            // 
            // loginTb
            // 
            this.loginTb.Location = new System.Drawing.Point(12, 66);
            this.loginTb.Name = "loginTb";
            this.loginTb.Size = new System.Drawing.Size(100, 20);
            this.loginTb.TabIndex = 2;
            this.loginTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.loginTb_KeyPress);
            // 
            // passwordTb
            // 
            this.passwordTb.Location = new System.Drawing.Point(12, 93);
            this.passwordTb.Name = "passwordTb";
            this.passwordTb.PasswordChar = '*';
            this.passwordTb.Size = new System.Drawing.Size(100, 20);
            this.passwordTb.TabIndex = 3;
            this.passwordTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passwordTb_KeyPress);
            // 
            // connToDbButton
            // 
            this.connToDbButton.Location = new System.Drawing.Point(12, 135);
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
            this.label3.Location = new System.Drawing.Point(115, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Имя пользователя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Пароль";
            // 
            // dbCb
            // 
            this.dbCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbCb.FormattingEnabled = true;
            this.dbCb.Location = new System.Drawing.Point(12, 177);
            this.dbCb.Name = "dbCb";
            this.dbCb.Size = new System.Drawing.Size(121, 21);
            this.dbCb.TabIndex = 9;
            // 
            // tbCb
            // 
            this.tbCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbCb.FormattingEnabled = true;
            this.tbCb.Location = new System.Drawing.Point(12, 247);
            this.tbCb.Name = "tbCb";
            this.tbCb.Size = new System.Drawing.Size(121, 21);
            this.tbCb.TabIndex = 10;
            // 
            // showTableListButton
            // 
            this.showTableListButton.Location = new System.Drawing.Point(12, 204);
            this.showTableListButton.Name = "showTableListButton";
            this.showTableListButton.Size = new System.Drawing.Size(100, 37);
            this.showTableListButton.TabIndex = 11;
            this.showTableListButton.Text = "Показать список таблиц";
            this.showTableListButton.UseVisualStyleBackColor = true;
            this.showTableListButton.Click += new System.EventHandler(this.showTableListButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(10, 372);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(123, 55);
            this.saveButton.TabIndex = 15;
            this.saveButton.Text = "Сохранить настройки";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // comboDataTb
            // 
            this.comboDataTb.Enabled = false;
            this.comboDataTb.Location = new System.Drawing.Point(159, 177);
            this.comboDataTb.Name = "comboDataTb";
            this.comboDataTb.Size = new System.Drawing.Size(100, 20);
            this.comboDataTb.TabIndex = 17;
            // 
            // comboTableTb
            // 
            this.comboTableTb.Enabled = false;
            this.comboTableTb.Location = new System.Drawing.Point(159, 248);
            this.comboTableTb.Name = "comboTableTb";
            this.comboTableTb.Size = new System.Drawing.Size(100, 20);
            this.comboTableTb.TabIndex = 18;
            // 
            // columnNameCb
            // 
            this.columnNameCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.columnNameCb.FormattingEnabled = true;
            this.columnNameCb.Location = new System.Drawing.Point(12, 319);
            this.columnNameCb.Name = "columnNameCb";
            this.columnNameCb.Size = new System.Drawing.Size(121, 21);
            this.columnNameCb.TabIndex = 19;
            // 
            // comboColumnTb
            // 
            this.comboColumnTb.Enabled = false;
            this.comboColumnTb.Location = new System.Drawing.Point(159, 320);
            this.comboColumnTb.Name = "comboColumnTb";
            this.comboColumnTb.Size = new System.Drawing.Size(100, 20);
            this.comboColumnTb.TabIndex = 20;
            // 
            // showColumnListButton
            // 
            this.showColumnListButton.Location = new System.Drawing.Point(12, 274);
            this.showColumnListButton.Name = "showColumnListButton";
            this.showColumnListButton.Size = new System.Drawing.Size(99, 39);
            this.showColumnListButton.TabIndex = 21;
            this.showColumnListButton.Text = "Выбрать столбец";
            this.showColumnListButton.UseVisualStyleBackColor = true;
            this.showColumnListButton.Click += new System.EventHandler(this.showColumnButton_Click);
            // 
            // dataTypeCheckBox
            // 
            this.dataTypeCheckBox.AutoSize = true;
            this.dataTypeCheckBox.Location = new System.Drawing.Point(159, 389);
            this.dataTypeCheckBox.Name = "dataTypeCheckBox";
            this.dataTypeCheckBox.Size = new System.Drawing.Size(70, 17);
            this.dataTypeCheckBox.TabIndex = 24;
            this.dataTypeCheckBox.Text = "дробные";
            this.dataTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // passTb
            // 
            this.passTb.Location = new System.Drawing.Point(223, 12);
            this.passTb.Name = "passTb";
            this.passTb.PasswordChar = '*';
            this.passTb.Size = new System.Drawing.Size(100, 20);
            this.passTb.TabIndex = 25;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(234, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 34);
            this.button3.TabIndex = 26;
            this.button3.Text = "Войти";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.enterPassButton_Click);
            // 
            // passErrLb
            // 
            this.passErrLb.AutoSize = true;
            this.passErrLb.Location = new System.Drawing.Point(224, 93);
            this.passErrLb.Name = "passErrLb";
            this.passErrLb.Size = new System.Drawing.Size(35, 13);
            this.passErrLb.TabIndex = 27;
            this.passErrLb.Text = "label5";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 473);
            this.Controls.Add(this.passErrLb);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.passTb);
            this.Controls.Add(this.dataTypeCheckBox);
            this.Controls.Add(this.showColumnListButton);
            this.Controls.Add(this.comboColumnTb);
            this.Controls.Add(this.columnNameCb);
            this.Controls.Add(this.comboTableTb);
            this.Controls.Add(this.comboDataTb);
            this.Controls.Add(this.saveButton);
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
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Options_Load);
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
        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.ComboBox dbCb;
        public System.Windows.Forms.ComboBox tbCb;
        public System.Windows.Forms.TextBox ipTb;
        public System.Windows.Forms.TextBox portTb;
        public System.Windows.Forms.TextBox loginTb;
        public System.Windows.Forms.TextBox passwordTb;
        public System.Windows.Forms.TextBox comboDataTb;
        public System.Windows.Forms.TextBox comboTableTb;
        private System.Windows.Forms.ComboBox columnNameCb;
        public System.Windows.Forms.TextBox comboColumnTb;
        private System.Windows.Forms.Button showColumnListButton;
        private System.Windows.Forms.CheckBox dataTypeCheckBox;
        private System.Windows.Forms.TextBox passTb;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label passErrLb;
    }
}