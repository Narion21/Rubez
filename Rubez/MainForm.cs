using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Threading;
using System.Timers;

namespace Rubez
{
    public partial class MainForm : Form
    {
        DataBase dataBase = new DataBase();
        System.Timers.Timer timerOfProcessesChart = new System.Timers.Timer();
        System.Timers.Timer timerOfProcessesReport = new System.Timers.Timer();
        int ColumnNameCount;
        string savePath = "";
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load_1(object sender, EventArgs e)
        { 
            this.Text = "Graphics for Data Base v.2.0.0";
            minTb.Text = "0";
            maxTb.Text = "110";
            lineNumberMin.Text = "32";
            lineNumberMax.Text = "86";
            axisYLb.Text = Properties.Settings.Default.comboColumnTbC;
            Console.WriteLine("FORM LOADDDDD");
            dTStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTStart.ShowUpDown = true;
            dTFinish.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTFinish.ShowUpDown = true;
            errorDataLb.Visible = false;
            errorFilterLb.Visible = false;
            timeErrorLb.Visible = false;
            timerOfProcessesChart.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses2);
            timerOfProcessesChart.Interval = 100;
            dataBase.sendFinishReadPartDataForChart += this.ReceiveFinishReadPartDataForChart;
            dataBase.sendFinishReadDataForChart += this.ReceiveFinishReadDataForChart;
            timerOfProcessesReport.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses);
            timerOfProcessesReport.Interval = 100;
            dataBase.sendFinishReadPartDataForReport += this.ReceiveFinishReadPartDataForReport;
            dataBase.sendFinishReadDataForReport += this.ReceiveFinishReadDataForReport;
        }

        private void ReceiveFinishReadPartDataForChart()
        {
            timerOfProcessesChart.Start();
        }

        private void ReceiveFinishReadDataForChart()
        {
            BlockPanel(true);
            Console.WriteLine("We finish ALL READ");
            if (Properties.Settings.Default.dataTypeSwitchC == false)
            {
                this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values); });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartFloat.Keys, dataBase.dataForChartFloat.Values); });
            }
        }
        private int DataInterval()
        {
            TimeSpan interval = dTFinish.Value - dTStart.Value;
            return interval.Days;
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            Options form2 = new Options();
            form2.Show();
        }

        private void doChartButton_Click(object sender, EventArgs e)
        {
            BlockPanel(false);           
            axisYLb.Text = Properties.Settings.Default.comboColumnTbC;
            dataBase.startId = 0;
            dataBase.endId = 0;
            chart1.Annotations.Clear();
            chart1.Series[0].Points.Clear();

            string month = dTStart.Text[5].ToString() + dTStart.Text[6].ToString();
            if (DaysIntervalTest(int.Parse(month)) == true)
            {
                dataBase.Conn();
                dataBase.MinId(dTStart.Text, dTFinish.Text, Properties.Settings.Default.comboTableTbC);
                dataBase.MaxId(dTStart.Text, dTFinish.Text, Properties.Settings.Default.comboTableTbC);
                dataBase.Close();
                dataBase.dataForChartInt.Clear();
                dataBase.dataForChartFloat.Clear();

                bool isLineOk = CreateLineForChart();
                bool isFilterOk = CreateFilter();
                if (dataBase.startId > 0 & dataBase.endId > 0 & isLineOk & isFilterOk)
                {
                    timerOfProcessesChart.Start();
                    timeErrorLb.Visible = false;
                }
                else
                {
                    BlockPanel(true);
                    timeErrorLb.Visible = true;
                    timeErrorLb.Text = "Нет данных в это время";
                }
            }
            else
            {
                BlockPanel(true);
            }
        }

        private void filterBt_Click(object sender, EventArgs e)
        {
            CreateFilter();
        }

        private bool CreateFilter()
        {
            bool isOk = false;
            try
            {
                if ((minTb.Text == string.Empty) || (maxTb.Text == string.Empty))
                {
                    errorFilterLb.Visible = true;
                    errorFilterLb.Text = "Введите минимальное и максимальное значение";
                }
                else
                {
                    if ((maxTb.Text.Length > 6) || (minTb.Text.Length > 6))
                    {
                        errorFilterLb.Visible = true;
                        errorFilterLb.Text = "Масимальное значение не может быть больше 999.999";
                    }
                    else if (Double.Parse(maxTb.Text) > Double.Parse(minTb.Text))
                    {
                        isOk = true;
                        chart1.ChartAreas[0].AxisY.Minimum = Double.Parse(minTb.Text);
                        chart1.ChartAreas[0].AxisY.Maximum = Double.Parse(maxTb.Text);
                        errorFilterLb.Visible = false;
                    }
                    else
                    {
                        errorFilterLb.Visible = true;
                        errorFilterLb.Text = "Нижний порог не может быть больше верхнего";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isOk;
        }

        private void minTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) & e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void maxTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) & e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void lineNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) & e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
        private void lineNumberMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) & e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private bool DaysIntervalTest(int value)
        {
            bool res = false;
            try
            {

                int time = DataInterval();
                if ((dTStart.Value.Date) > (dTFinish.Value.Date))
                {
                    timeErrorLb.Text = "Первая дата не может быть больше второй";
                    timeErrorLb.Visible = true;
                }

                else if ((dTStart.Value.Date) == (dTFinish.Value.Date) & (dTStart.Value.TimeOfDay >= dTFinish.Value.TimeOfDay))
                {
                    timeErrorLb.Text = "Первая дата не может быть больше второй";
                    timeErrorLb.Visible = true;
                }
                else if (time > 31)
                {
                    timeErrorLb.Text = "Можно выбрать не больше 31 дня, вы выбрали" + time;
                    timeErrorLb.Visible = true;
                }
                else
                {
                    res = true;
                    timeErrorLb.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;
        }

        private void GetDataByReaderForChart()
        {
            dataBase.Conn();
            dataBase.DataFromBDForChart(Properties.Settings.Default.comboColumnTbC, Properties.Settings.Default.comboTableTbC);
            dataBase.Close();
        }

        private void TimeoutProcesses2(object sender, ElapsedEventArgs e)
        {
            timerOfProcessesChart.Stop();
            GetDataByReaderForChart();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            BlockPanel(false);
            try
            {
                string month = dTStart.Text[5].ToString() + dTStart.Text[6].ToString();
                if (DaysIntervalTest(int.Parse(month)) == true)
                {
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
                    sf.FilterIndex = 1;
                    sf.RestoreDirectory = true;
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sw = new StreamWriter(sf.FileName);
                        sw.Close();
                        savePath = sf.FileName;
                        dataBase.Conn();
                        dataBase.MinId(dTStart.Text, dTFinish.Text, Properties.Settings.Default.comboTableTbC);
                        dataBase.MaxId(dTStart.Text, dTFinish.Text, Properties.Settings.Default.comboTableTbC);
                        dataBase.Close();
                        ColumnName();
                        if ((dataBase.startId > 0) & (dataBase.endId > 0))
                        {

                            timerOfProcessesReport.Start();
                            timeErrorLb.Visible = false;
                        }
                        else
                        {
                            BlockPanel(true);
                            timeErrorLb.Visible = true;
                            timeErrorLb.Text = "Нет данных в это время";
                        }
                    }
                    else
                    {
                        BlockPanel(true);
                        savePath = "";
                    }
                }
            }
            catch (Exception ex)
            {
                BlockPanel(true);
                Console.WriteLine(ex.Message);
            }
        }

        private void ReceiveFinishReadPartDataForReport()
        {
            timerOfProcessesReport.Start();
        }

        private void ReceiveFinishReadDataForReport()
        {
            BlockPanel(true);
            timerOfProcessesReport.Stop();
            Console.WriteLine("We finish ALL READ");
        }

        public void TimeoutProcesses(object sender, ElapsedEventArgs e)
        {
            timerOfProcessesReport.Stop();
            GetDataByReader();
        }

        public void GetDataByReader()
        {
            dataBase.Conn();
            List<string> tempList = dataBase.DataFromBD(Properties.Settings.Default.comboTableTbC);
            dataBase.Close();

            if (tempList.Count > 0)
            {
                WriteDataToCSV(tempList);
            }
        }

        public void WriteDataToCSV(List<string> listInfo)
        {
            try
            {
                int a = 0;
                var csv = new StringBuilder();
                foreach (string i in listInfo)
                {
                    csv.Append(i + ";");
                    a++;
                    if (a == ColumnNameCount)
                    {
                        csv.AppendLine();
                        a = 0;
                    }
                }
                File.AppendAllText(savePath, csv.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void makeLineButton_Click(object sender, EventArgs e)
        {
            CreateLineForChart();
        }

        private bool CreateLineForChart()
        {
            bool isOk = false;
            if ((lineNumberMin.Text == string.Empty) || (lineNumberMax.Text == string.Empty))
            {               
                errorDataLb.Visible = true;
                errorDataLb.Text = "Введите минимальное и максимальное значение";
            }
            else
            {
                errorDataLb.Visible = false;
                chart1.Annotations.Clear();
                if ((lineNumberMin.Text.Length > 5) || (lineNumberMax.Text.Length > 5))
                {                    
                    errorDataLb.Visible = true;
                    errorDataLb.Text = "Масимальное значение не может быть больше 99.999";
                }
                else if (int.Parse(lineNumberMin.Text) > int.Parse(lineNumberMax.Text))
                {                   
                    errorDataLb.Visible = true;
                    errorDataLb.Text = "Первое значение не может быть больше второго";
                }
                else
                {
                    isOk = true;
                    Make_Line(Double.Parse(lineNumberMin.Text));
                    Make_Line(Double.Parse(lineNumberMax.Text));
                }
            }
            return isOk;
        }
        private void Make_Line(Double start)
        {
            double lineHeigt = start;
            HorizontalLineAnnotation ann = new HorizontalLineAnnotation();
            ann.AxisX = chart1.ChartAreas[0].AxisX;
            ann.AxisY = chart1.ChartAreas[0].AxisY;
            ann.IsSizeAlwaysRelative = false;
            ann.AnchorY = lineHeigt;
            ann.IsInfinitive = true;
            ann.ClipToChartArea = chart1.ChartAreas[0].Name;
            ann.LineColor = Color.Red; ann.LineWidth = 2;
            chart1.Annotations.Add(ann);
        }

        public void ColumnName()
        {
            dataBase.Conn();
            String columnNames = dataBase.DataFromBDColumnName(Properties.Settings.Default.comboTableTbC);
            dataBase.Close();
            columnNames = columnNames.Replace(",", ";");
            columnNames += ";\n";
            string[] ColumnNameL = columnNames.Split(';');
            ColumnNameCount = ColumnNameL.Length - 1;
            Console.WriteLine(ColumnNameCount + "CCCCCCCCCCCCCCCCCCC");
            File.AppendAllText(savePath, columnNames);
        }

        public void BlockPanel(bool isEnable)
        {
            this.Invoke((MethodInvoker)delegate () { mainPanel.Enabled = isEnable; });
        }
    }
}
