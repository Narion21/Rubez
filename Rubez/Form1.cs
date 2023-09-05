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
        CsvReport csvReport = new CsvReport();
        DataBase dataBase = new DataBase();
        Chart chart = new Chart();
        //System.Timers.Timer timerOfProcessesReport = new System.Timers.Timer();
        System.Timers.Timer timerOfProcessesChart = new System.Timers.Timer();


        int step = 100000;
        int startIdxReport = 0;
        int startIdxChart = 0;
        int endIdxReport = 0;
        int endIdxChart = 0;
        int countReport = 0;
        int countChart = 0;
        int repeatReport = 0;
        int repeatChart = 0;
        int remainderIdReport = 0;
        int remainderIdChart = 0;
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
            //timerOfProcessesReport.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses);
            //timerOfProcessesReport.Interval = 1000;
            // переименуй пожалуйста все переменные по целовечески, что за таймер 2
            // и убери не используемые перемнные
            timerOfProcessesChart.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses2);
            timerOfProcessesChart.Interval = 100;

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            /*
             * Это вторая часть работы с делегатами (эвентами), тут мы определяем в какой метод
             * перейдет действие, как только мы получим сигнал с другой стороны
             * я сделала логику из 2х частей
             * 1 - мы считали кусок из базы, и надо запустить чтение след куска, для этого
             * таймер уменьшила до 100мс чтобы просто не затоптать себя же
             * 2 часть - мы закончили читать все и в этом случае можем приступать к отрисовке чарта
             */
            dataBase.emitSendFinishReadPartOfDataForChart += this.ReceiveFinishReadPartOfDataForChart;
            dataBase.emitSendFinishReadDataForChart += this.ReceiveFinishReadDataForChart;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }

        private void ReceiveFinishReadPartOfDataForChart()
        {           
            Console.WriteLine(">>> We finish part");
            timerOfProcessesChart.Start();
        }

        private void ReceiveFinishReadDataForChart()
        {            
            Console.WriteLine("We finish ALL READ");
            this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values); });
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

        /*private void chart1_MouseWheel(object sender, MouseEventArgs e)
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
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            // добавила очистку чарта - его не было из-за чегос памился мусор раз за разом !!!!
            chart1.Series[0].Points.Clear();            

            string month = dTStart.Text[5].ToString() + dTStart.Text[6].ToString();
            if (DaysIntervalTest(int.Parse(month)) == true)
            {
                dataBase.Conn();
                // вызываю просто методы на определение начала и конца ид для чтения
                dataBase.MinId(dTStart.Text, dTFinish.Text);
                dataBase.MaxId(dTStart.Text, dTFinish.Text);
               //bilo - startIdxChart = int.Parse(dataBase.MinId(dTStart.Text, dTFinish.Text));
               //bilo - endIdxChart = int.Parse(dataBase.MaxId(dTStart.Text, dTFinish.Text));
                dataBase.Close();
                //bilo - int numberId = endIdxChart - startIdxChart;
                //bilo - repeatChart = numberId / step;
                //bilo - remainderIdChart = numberId - repeatChart * step;
                
                // добавила очистку словарей, иначе спапился мусор !!!!!
                dataBase.dataForChartInt.Clear();
                dataBase.dataForChartFloat.Clear();

                timerOfProcessesChart.Start();
                timeErrorLb.Visible = false;
            }
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
                        csvReport.savePath = sf.FileName;
                        dataBase.Conn();
                        csvReport.startIdxReport = int.Parse(dataBase.MinId(dTStart.Text, dTFinish.Text));
                        csvReport.endIdxReport = int.Parse(dataBase.MaxId(dTStart.Text, dTFinish.Text));
                        dataBase.Close();
                        int numberId = csvReport.endIdxReport - csvReport.startIdxReport;
                        csvReport.repeatReport = numberId / csvReport.step;
                        csvReport.remainderIdReport = numberId - csvReport.repeatReport * csvReport.step;
                        //csvReport.GetDataByReader();
                        Console.WriteLine(repeatReport + "repeatReport form");
                        Console.WriteLine(remainderIdReport + "remainderIdReport form");

                        csvReport.StartTimer();
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

        /*private void TimeoutProcesses(object sender, ElapsedEventArgs e)
        {
            if (countReport != repeatReport)
            {
                Console.WriteLine(countReport + "===ИЗ===" + repeatReport + " ОТЧЕТ ");
                countReport++;
                endIdxReport = startIdxReport + step;
                GetDataByReader();
                startIdxReport = endIdxReport;
            }
            else
            {
                Console.WriteLine(countReport + "===ИЗ===" + repeatReport + " ОТЧЕТ ");
                timerOfProcessesReport.Stop();
                startIdxReport = endIdxReport;
                endIdxReport = startIdxReport + remainderIdReport;
                GetDataByReader();
                countReport = 0;
                remainderIdReport = 0;
                startIdxReport = 0;
                endIdxReport = 0;
            }
        }
        private void GetDataByReader()
        {
            dataBase.Conn();
            List<string> tempList = dataBase.DataFromBD(startIdxReport, endIdxReport);
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
        }  */

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine(countChart);
            Console.WriteLine(repeatChart);
            Console.WriteLine(remainderIdChart);
            dataBase.Conn();
            dataBase.IdCount(dTStart.Text, dTFinish.Text, Properties.Settings.Default.comboTableTbC);
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

        }

        private void GetDataByReaderForChart()
        {
            dataBase.Conn();
            dataBase.DataFromBDForChart(startIdxChart, endIdxChart, Properties.Settings.Default.comboColumnTbC, Properties.Settings.Default.comboTableTbC);
            dataBase.Close();
        }

        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        /* 
         * Старайся не плодить логику там где ее не должно быть
         * Если хочешь выполнение логики в момент таймаута - выноси ее в отдельную функцию
         * Это раз
         * 
         * Два - отрисовка чарта на каждый таймаут это не верное дейсвтие с точки зрения логики в данном случае
         * Да, можно сделать в реалтайме отрисовку, но для этого надо использовать чистые потоки
         * В текущем случае будет правильнее сделать логику - собираем все - потом рисуем все
         * Из-за переноса начала и конца ид внутрь бд и изменение логики запроса в функции 
         * многие переменные теперь потеряли актуальность + мы облегчили сами фукнции
         * Убери лишнии параметры и переменные ))
         */
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private void TimeoutProcesses2(object sender, ElapsedEventArgs e)
        {
            timerOfProcessesChart.Stop();
            GetDataByReaderForChart();

/*
 вот это весь твой код, который я просто законментила целиком и вынесла в 2 строчки выше в сигналы
            if (repeatChart == 0)
            {
                Console.WriteLine("МЫ В ИФ =0");
                Console.WriteLine(countChart + "===ИЗ===" + repeatChart + " ОТЧЕТ ");
                timerOfProcessesChart.Stop();
                GetDataByReaderForChart();
                if (Properties.Settings.Default.dataTypeSwitchC == false)
                {
                    // Action action = () => chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values);
                    //  Invoke(action);

                 //>   this.Invoke((MethodInvoker)delegate () {  });

                    this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values); });
                }
                else
                {
                  //  Action action = () => chart1.Series[0].Points.DataBindXY(dataBase.dataForChartFloat.Keys, dataBase.dataForChartFloat.Values);
                  //  Invoke(action);

                    this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartFloat.Keys, dataBase.dataForChartFloat.Values); });
                }
                countChart = 0;
                remainderIdChart = 0;
                startIdxChart = 0;
                endIdxChart = 0;
            }

            else if ((countChart != repeatChart) & (repeatChart != 0))
            {
                Console.WriteLine(countChart + "===ИЗ===" + repeatChart + " ГРАФИК ");
                endIdxChart = startIdxChart + step;
                GetDataByReaderForChart();
                startIdxChart = endIdxChart;
                countChart++;
            }

            else
            {
                Console.WriteLine(countChart + "===ИЗ===" + repeatChart + " ГРАФИК ");
                timerOfProcessesChart.Stop();
                startIdxChart = endIdxChart;
                endIdxChart = startIdxChart + remainderIdChart;
                GetDataByReaderForChart();
                if (Properties.Settings.Default.dataTypeSwitchC == false)
                {
                  //  Action action = () => chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values);
                 //   Invoke(action);

                    this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values); });
                }
                else if (Properties.Settings.Default.dataTypeSwitchC == true)
                {
                  //  Action action = () => chart1.Series[0].Points.DataBindXY(dataBase.dataForChartFloat.Keys, dataBase.dataForChartFloat.Values);
                  //  Invoke(action);
                    this.Invoke((MethodInvoker)delegate () { chart1.Series[0].Points.DataBindXY(dataBase.dataForChartFloat.Keys, dataBase.dataForChartFloat.Values); });

                }
                    
                dataBase.dataForChartInt.Clear();
                countChart = 0;
                remainderIdChart = 0;
                startIdxChart = 0;
                endIdxChart = 0;
            }
        
        */
        
        }


    }

}
