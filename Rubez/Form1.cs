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

        int chek = 0;
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
            errorFilterLb.Visible = false;
            timeErrorLb.Visible = false;
            //startIdx = dataBase.MinId(dTStart.Text, dTFinish.Text);
            //endIdx = dataBase.MaxId(dTStart.Text, dTFinish.Text);
            timerOfProcesses.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses);
            timerOfProcesses.Interval = 1000;
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


        private void reportButton_Click(object sender, EventArgs e)
        {
            { // зачем эти скобки и к чему они относятся?
                csvreport.Csv(dTStart.Text, dTFinish.Text);
            }
        }



        /* private void doChartButton_Click(object sender, EventArgs e)
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

                 chart1.DataSource = chart.dataToChart(dTStart.Text, dTFinish.Text);
                 chart1.Series[0].XValueMember = "id";
                 chart1.Series[0].YValueMembers = "fotoreque";
                 chart1.ChartAreas[0].AxisY.Minimum = 0;
                 chart1.ChartAreas[0].AxisY.Maximum = 100;
                 timeErrorLb.Visible = false;


             }

         }
        */

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

                // chart1.DataSource = chart.dataToChart(dTStart.Text, dTFinish.Text);
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
            catch { } //лучше не оставлять пустой catch а дописать его нормально, чтобы видеть ошибку в случае возникновения

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dTStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTStart.ShowUpDown = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dTFinish.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dTFinish.ShowUpDown = true;
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


        private void button1_Click(object sender, EventArgs e)
        {

            GetDataByReader();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sf.Filter = filter;
            StreamWriter writer = null; // не используемая переменная
            sf.ShowDialog(); // если я не выберу путь \ файл программа крашит

            GetDataByReader();
            // 1 логическое действие - запрос начала и конца лучше делать через транзакцию
            // это позволит избежать внезапной коллизии (возможно ты не будешь ловить проблем но они могут возникнуть когда не надо и исправлять потом будет сложнее, просто совет)
            // открыть соединение к бд - начать транзакцию - отправить запросы друг за другом (можно дажже в 1 функции) - закрыть транзакцию - закрыть соединение 
            // + добавить проверку что значения начала и конца не 0
            dataBase.Conn();
            startIdx = dataBase.MinId(dTStart.Text, dTFinish.Text);
            endIdx = dataBase.MaxId(dTStart.Text, dTFinish.Text);
            dataBase.Close();
            int numberId = endIdx - startIdx;
            repeat = numberId / step;
            remainderId = numberId - repeat * step;
            timerOfProcesses.Start();
            savePath = Path.GetDirectoryName(sf.FileName);
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
                // формат кода - лишний пробел
            else
            {
                // правильно будет делать сначала остановку таймера, только после этоговсе остальное, иначе можешь поймать коллизию
                // при работе с вычислениями лучше использовать переменные не глобальные а локальные и прокидывать их как параметры
                startIdx = endIdx;
                endIdx = startIdx + remainderId;
                GetDataByReader();
                count = 0;
                remainderId = 0;
                startIdx = 0;
                endIdx = 0;
                timerOfProcesses.Stop();
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

            File.AppendAllText(savePath+".csv", csv.ToString());
            //File.AppendAllText("F:/myCSV/myCSV.csv", csv.ToString());




            //filter = sf.FileName;

            //"F:/myCSV/myCSV.csv"
        }

        private void button2_Click(object sender, EventArgs e)
        {

            dataBase.Conn();
            dataBase.IdCount(dTStart.Text, dTFinish.Text);
            dataBase.Close();
        }

        private void MyTestButton_Click(object sender, EventArgs e)
        {

            SaveFileDialog sf = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sf.Filter = filter;
            StreamWriter writer = null;
            //sf.ShowDialog();
            if (sf.ShowDialog() == DialogResult.OK)
            {
                int count = 0;
                dataBase.Conn();
                int startIdx = dataBase.MinId(dTStart.Text, dTFinish.Text);
                int endIdx = dataBase.MaxId(dTStart.Text, dTFinish.Text);
                int numberId = endIdx - startIdx;
                dataBase.Close();
                int repeat = numberId / 10000;
                int remainderId = numberId - repeat * 10000;
                Console.WriteLine("startId===" + startIdx);
                Console.WriteLine("endId===" + endIdx);
                Console.WriteLine("numberId===" + numberId);
                Console.WriteLine("repeat===" + repeat);
                Console.WriteLine("remainderId===" + remainderId);

                while (count != repeat)
                {
                    count++;
                    endIdx = startIdx + 10000;
                    Console.WriteLine("Подкачало из startId== " + startIdx + " endId=== " + endIdx + " вот столько=== " + count + " раз");
                    dataBase.Conn();
                    List<string> tempList = dataBase.DataFromBD(startIdx, endIdx);
                    dataBase.Close();
                    //WriteDataToCSV(tempList, filter, writer, sf);
                    startIdx = endIdx;
                    if (count == repeat)
                    {
                        startIdx = endIdx;
                        endIdx += remainderId;
                        Console.WriteLine("ОСТАТОК!!!Подкачало из startId== " + startIdx + " endId=== " + endIdx + " вот столько=== " + count + " раз");
                        dataBase.Conn();
                        List<string> tempList1 = dataBase.DataFromBD(startIdx, endIdx);
                        dataBase.Close();
                        //WriteDataToCSV(tempList1, filter, writer, sf);
                    }
                }
            }



        }


    }

}
