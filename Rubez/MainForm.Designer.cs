namespace Rubez
{
    partial class MainForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.reportButton = new System.Windows.Forms.Button();
            this.errorDataLb = new System.Windows.Forms.Label();
            this.lineNumberMin = new System.Windows.Forms.TextBox();
            this.makeLineButton = new System.Windows.Forms.Button();
            this.lineNumberMax = new System.Windows.Forms.TextBox();
            this.axisYLb = new System.Windows.Forms.Label();
            this.axisXLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // optionButton
            // 
            this.optionButton.Location = new System.Drawing.Point(33, 6);
            this.optionButton.Name = "optionButton";
            this.optionButton.Size = new System.Drawing.Size(75, 43);
            this.optionButton.TabIndex = 33;
            this.optionButton.Text = "Настройки";
            this.optionButton.UseVisualStyleBackColor = true;
            this.optionButton.Click += new System.EventHandler(this.OptionsButton_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 55);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 2;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(804, 463);
            this.chart1.TabIndex = 41;
            this.chart1.Text = "chart1";
            // 
            // doChartButton
            // 
            this.doChartButton.Location = new System.Drawing.Point(126, 6);
            this.doChartButton.Name = "doChartButton";
            this.doChartButton.Size = new System.Drawing.Size(75, 43);
            this.doChartButton.TabIndex = 43;
            this.doChartButton.Text = "Построить график";
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
            this.maxTb.Location = new System.Drawing.Point(595, 5);
            this.maxTb.Name = "maxTb";
            this.maxTb.Size = new System.Drawing.Size(39, 20);
            this.maxTb.TabIndex = 45;
            this.maxTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maxTb_KeyPress);
            // 
            // filterBt
            // 
            this.filterBt.Location = new System.Drawing.Point(640, 2);
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
            this.errorFilterLb.Location = new System.Drawing.Point(528, 51);
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
            this.timeErrorLb.Location = new System.Drawing.Point(252, 34);
            this.timeErrorLb.Name = "timeErrorLb";
            this.timeErrorLb.Size = new System.Drawing.Size(29, 13);
            this.timeErrorLb.TabIndex = 50;
            this.timeErrorLb.Text = "label";
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(739, 2);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(77, 47);
            this.reportButton.TabIndex = 54;
            this.reportButton.Text = "Отчет";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // errorDataLb
            // 
            this.errorDataLb.AutoSize = true;
            this.errorDataLb.Location = new System.Drawing.Point(528, 64);
            this.errorDataLb.Name = "errorDataLb";
            this.errorDataLb.Size = new System.Drawing.Size(29, 13);
            this.errorDataLb.TabIndex = 56;
            this.errorDataLb.Text = "label";
            // 
            // lineNumberMin
            // 
            this.lineNumberMin.Location = new System.Drawing.Point(552, 28);
            this.lineNumberMin.Name = "lineNumberMin";
            this.lineNumberMin.Size = new System.Drawing.Size(41, 20);
            this.lineNumberMin.TabIndex = 57;
            this.lineNumberMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lineNumber_KeyPress);
            // 
            // makeLineButton
            // 
            this.makeLineButton.Location = new System.Drawing.Point(640, 26);
            this.makeLineButton.Name = "makeLineButton";
            this.makeLineButton.Size = new System.Drawing.Size(75, 23);
            this.makeLineButton.TabIndex = 58;
            this.makeLineButton.Text = "Граница";
            this.makeLineButton.UseVisualStyleBackColor = true;
            this.makeLineButton.Click += new System.EventHandler(this.makeLineButton_Click);
            // 
            // lineNumberMax
            // 
            this.lineNumberMax.Location = new System.Drawing.Point(595, 28);
            this.lineNumberMax.Name = "lineNumberMax";
            this.lineNumberMax.Size = new System.Drawing.Size(39, 20);
            this.lineNumberMax.TabIndex = 59;
            this.lineNumberMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lineNumberMax_KeyPress);
            // 
            // axisYLb
            // 
            this.axisYLb.AutoSize = true;
            this.axisYLb.Location = new System.Drawing.Point(12, 96);
            this.axisYLb.Name = "axisYLb";
            this.axisYLb.Size = new System.Drawing.Size(0, 13);
            this.axisYLb.TabIndex = 60;
            // 
            // axisXLb
            // 
            this.axisXLb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axisXLb.AutoSize = true;
            this.axisXLb.Location = new System.Drawing.Point(95, 496);
            this.axisXLb.Name = "axisXLb";
            this.axisXLb.Size = new System.Drawing.Size(16, 13);
            this.axisXLb.TabIndex = 61;
            this.axisXLb.Text = "Id";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 518);
            this.Controls.Add(this.axisXLb);
            this.Controls.Add(this.axisYLb);
            this.Controls.Add(this.lineNumberMax);
            this.Controls.Add(this.makeLineButton);
            this.Controls.Add(this.lineNumberMin);
            this.Controls.Add(this.errorDataLb);
            this.Controls.Add(this.reportButton);
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
            this.Name = "MainForm";
            this.Text = " ";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
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
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.Label errorDataLb;
        private System.Windows.Forms.TextBox lineNumberMin;
        private System.Windows.Forms.Button makeLineButton;
        private System.Windows.Forms.TextBox lineNumberMax;
        private System.Windows.Forms.Label axisYLb;
        private System.Windows.Forms.Label axisXLb;
    }
}

