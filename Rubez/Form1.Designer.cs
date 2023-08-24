namespace Rubez
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.optionButton = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.doChartButton = new System.Windows.Forms.Button();
            this.minTb = new System.Windows.Forms.TextBox();
            this.maxTb = new System.Windows.Forms.TextBox();
            this.filterBt = new System.Windows.Forms.Button();
            this.errorFilterLb = new System.Windows.Forms.Label();
            this.dTStart = new System.Windows.Forms.DateTimePicker();
            this.dTFinish = new System.Windows.Forms.DateTimePicker();
            this.timeErrorLb = new System.Windows.Forms.Label();
            this.sf = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.errorDataLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // optionButton
            // 
            this.optionButton.Location = new System.Drawing.Point(21, 6);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(75, 23);
            this.optionButton.TabIndex = 33;
            this.optionButton.Text = "Настройки";
            this.optionButton.UseVisualStyleBackColor = true;
            this.optionButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(12, 54);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.YValuesPerPoint = 2;
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(804, 463);
            this.chart1.TabIndex = 41;
            this.chart1.Text = "chart1";
            // 
            // doChartButton
            // 
            this.doChartButton.Location = new System.Drawing.Point(130, 5);
            this.doChartButton.Name = "doChartButton";
            this.doChartButton.Size = new System.Drawing.Size(92, 37);
            this.doChartButton.TabIndex = 43;
            this.doChartButton.Text = "Сформировать график";
            this.doChartButton.UseVisualStyleBackColor = true;
            this.doChartButton.Click += new System.EventHandler(this.doChartButton_Click);
            // 
            // minTb
            // 
            this.minTb.Location = new System.Drawing.Point(552, 5);
            this.minTb.Name = "minTb";
            this.minTb.Size = new System.Drawing.Size(40, 20);
            this.minTb.TabIndex = 44;
            this.minTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.minTb_KeyPress);
            // 
            // maxTb
            // 
            this.maxTb.Location = new System.Drawing.Point(598, 5);
            this.maxTb.Name = "maxTb";
            this.maxTb.Size = new System.Drawing.Size(39, 20);
            this.maxTb.TabIndex = 45;
            this.maxTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxTb_KeyPress);
            // 
            // filterBt
            // 
            this.filterBt.Location = new System.Drawing.Point(640, 4);
            this.filterBt.Name = "filterBt";
            this.filterBt.Size = new System.Drawing.Size(75, 23);
            this.filterBt.TabIndex = 46;
            this.filterBt.Text = "Фильтр";
            this.filterBt.UseVisualStyleBackColor = true;
            this.filterBt.Click += new System.EventHandler(this.filterBt_Click);
            // 
            // errorFilterLb
            // 
            this.errorFilterLb.AutoSize = true;
            this.errorFilterLb.Location = new System.Drawing.Point(533, 29);
            this.errorFilterLb.Name = "errorFilterLb";
            this.errorFilterLb.Size = new System.Drawing.Size(29, 13);
            this.errorFilterLb.TabIndex = 47;
            this.errorFilterLb.Text = "label";
            // 
            // dTStart
            // 
            this.dTStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTStart.Location = new System.Drawing.Point(228, 6);
            this.dTStart.Name = "dTStart";
            this.dTStart.Size = new System.Drawing.Size(131, 20);
            this.dTStart.TabIndex = 48;
            // 
            // dTFinish
            // 
            this.dTFinish.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTFinish.Location = new System.Drawing.Point(377, 6);
            this.dTFinish.Name = "dTFinish";
            this.dTFinish.Size = new System.Drawing.Size(131, 20);
            this.dTFinish.TabIndex = 49;
            // 
            // timeErrorLb
            // 
            this.timeErrorLb.AutoSize = true;
            this.timeErrorLb.Location = new System.Drawing.Point(273, 29);
            this.timeErrorLb.Name = "timeErrorLb";
            this.timeErrorLb.Size = new System.Drawing.Size(29, 13);
            this.timeErrorLb.TabIndex = 50;
            this.timeErrorLb.Text = "label";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(714, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 52;
            this.button2.Text = "сколько id";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(739, 6);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(77, 32);
            this.reportButton.TabIndex = 54;
            this.reportButton.Text = "Отчет";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(714, 190);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 55;
            this.button3.Text = "тест кнопка";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // errorDataLb
            // 
            this.errorDataLb.AutoSize = true;
            this.errorDataLb.Location = new System.Drawing.Point(668, 38);
            this.errorDataLb.Name = "errorDataLb";
            this.errorDataLb.Size = new System.Drawing.Size(29, 13);
            this.errorDataLb.TabIndex = 56;
            this.errorDataLb.Text = "label";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 524);
            this.Controls.Add(this.errorDataLb);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.timeErrorLb);
            this.Controls.Add(this.dTFinish);
            this.Controls.Add(this.dTStart);
            this.Controls.Add(this.errorFilterLb);
            this.Controls.Add(this.filterBt);
            this.Controls.Add(this.maxTb);
            this.Controls.Add(this.minTb);
            this.Controls.Add(this.doChartButton);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.optionButton);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button optionButton;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button doChartButton;
        private System.Windows.Forms.TextBox minTb;
        private System.Windows.Forms.TextBox maxTb;
        private System.Windows.Forms.Button filterBt;
        private System.Windows.Forms.Label errorFilterLb;
        private System.Windows.Forms.Label timeErrorLb;
        private System.Windows.Forms.SaveFileDialog sf;
        public System.Windows.Forms.DateTimePicker dTStart;
        public System.Windows.Forms.DateTimePicker dTFinish;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label errorDataLb;
    }
}

