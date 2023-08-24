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
    public partial class Form1 : Form
    {
        Chart chart = new Chart();
        CsvReport csvreport = new CsvReport();
        DataBase dataBase = new DataBase();
        System.Timers.Timer timerOfProcesses = new System.Timers.Timer();
        System.Timers.Timer timerOfProcesses2 = new System.Timers.Timer();


        int step = 10000;
        int startIdx = 0;
        int endIdx = 0;
        int count = 0;
        int repeat = 0;
        int remainderId = 0; 
        string savePath = "";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Console.WriteLine("FORM LOADDDDD");
            dTStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTStart.ShowUpDown = true;
            dTFinish.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTFinish.ShowUpDown = true;
            errorDataLb.Visible = false;
            errorFilterLb.Visible = false;
            timeErrorLb.Visible = false;
            //startIdx = dataBase.MinId(dTStart.Text, dTFinish.Text);
            //endIdx = dataBase.MaxId(dTStart.Text, dTFinish.Text);
            timerOfProcesses.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses);
            timerOfProcesses.Interval = 1000;
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

        /* private void chart1_MouseWheel(object sender, MouseEventArgs e)
         {
             var chart = (Chart)sender;
             var xAxis = chart.ChartAreas[0].AxisX;
             var yAxis = chart.ChartAreas[0].AxisY;
             try
             {
                 if (e.Delta < 0)
                 {
                     xAxis.ScaleView.ZoomReset();
                     yAxis.ScaleView.ZoomReset();
                 }
                 else if (e.Delta > 0)
                 {
                     var xMin = xAxis.ScaleView.ViewMinimum;
                     var xMax = xAxis.ScaleView.ViewMaximum;
                     var yMin = yAxis.ScaleView.ViewMinimum;
                     var yMax = yAxis.ScaleView.ViewMaximum;
                     var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                     var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                     var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                     var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - xMin) / 4;
                     xAxis.ScaleView.Zoom(posXStart, posXFinish);
                     yAxis.ScaleView.Zoom(posYStart, posYFinish);
                 }

             }
             catch
             {

             }

         } */


        private void doChartButton_Click(object sender, EventArgs e)
        {

            if ((dTStart.Value.Date) > (dTFinish.Value.Date))
            {
                timeErrorLb.Text = "Проверьте дату";
                timeErrorLb.Visible = true;
            }

            else if ((dTStart.Value.Date) == (dTFinish.Value.Date) & (dTStart.Value.TimeOfDay >= dTFinish.Value.TimeOfDay))
            {
                timeErrorLb.Text = "Проверьте время";
                timeErrorLb.Visible = true;
            }

            else
            {
                dataBase.Conn();
                chart1.DataSource = chart.dataToChart(dTStart.Text, dTFinish.Text);
                dataBase.Close();

                chart1.Series[0].XValueMember = "id";
                chart1.Series[0].YValueMembers = "fotoreque";
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 100;
                timeErrorLb.Visible = false;

            }
        }


        private void filterBt_Click(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(maxTb.Text) > Double.Parse(minTb.Text))
                {
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
            catch { }

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

        private void reportButton_Click(object sender, EventArgs e)
        {
            
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
                        GetDataByReader();
                        dataBase.Conn();
                        startIdx = int.Parse(dataBase.MinId(dTStart.Text, dTFinish.Text));
                        endIdx = int.Parse(dataBase.MaxId(dTStart.Text, dTFinish.Text));
                        dataBase.Close();
                        int numberId = endIdx - startIdx;
                        repeat = numberId / step;
                        remainderId = numberId - repeat * step;
                        timerOfProcesses.Start();
                    }
                    else
                    {
                        savePath = "";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void TimeoutProcesses(object sender, ElapsedEventArgs e)
        {
            

            if (count != repeat)
            {
                count++;
                endIdx = startIdx + step;
                GetDataByReader();
                startIdx = endIdx;
            }

            else
            {
                timerOfProcesses.Stop();
                startIdx = endIdx;
                endIdx = startIdx + remainderId;
                GetDataByReader();
                count = 0;
                remainderId = 0;
                startIdx = 0;
                endIdx = 0;
            }
        }
        private void GetDataByReader()
        {
            dataBase.Conn();
            List<string> tempList = dataBase.DataFromBD(startIdx, endIdx);
            dataBase.Close();

            if (tempList.Count > 0)
            {
                WriteDataToCSV(tempList);
            }
        }

        private void WriteDataToCSV(List<string> listInfo)
        {
            try
            {
                int a = 0;
                var csv = new StringBuilder();

                foreach (string i in listInfo)
                {
                    csv.Append(i + ";");
                    a++;
                    if (a == 37)
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

        private void button2_Click(object sender, EventArgs e)
        {
            dataBase.Conn();
            dataBase.IdCount(dTStart.Text, dTFinish.Text);
            dataBase.Close();
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
                else
                {
                    timeErrorLb.Visible = false;
                    switch (value)
                    {
                        case 1:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В январе 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 2:
                            if (time <= 29)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В феврале 29 дней, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 3:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В марте 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 4:
                            if (time <= 30)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В апреле 30 дней, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 5:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В мае 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 6:
                            if (time <= 30)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В июне 30 дней, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 7:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В июле 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 8:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В августе 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 9:
                            if (time <= 30)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В сентябре 30 дней, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 10:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В октябре 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 11:
                            if (time <= 30)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В ноябре 30 дней, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        case 12:
                            if (time <= 31)
                            {
                                res = true;
                                errorDataLb.Visible = false;
                            }
                            else
                            {
                                errorDataLb.Text = "В декабре 31 день, выделено" + DataInterval();
                                errorDataLb.Visible = true;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return res;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataBase.Conn();
            List<string> tempList = dataBase.DataFromBD(startIdx, endIdx);
            dataBase.Close();
        }

    }

}
