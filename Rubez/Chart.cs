using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using System.Timers;
using System.IO;

namespace Rubez
{
    internal class Chart
    {
        DataBase dataBase = new DataBase();

        public int step = 10000;
        public int startIdxChart = 0;
        public int endIdxChart = 0;
        int countChart = 0;
        public int repeatChart = 0;
        public int remainderIdChart = 0;

        System.Timers.Timer timerOfProcessesChart = new System.Timers.Timer();

        public Chart()
        {
            timerOfProcessesChart.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses2);
            timerOfProcessesChart.Interval = 1000;
        }

        private void GetDataByReaderForChart()
        {
            dataBase.Conn();
            //dataBase.DataFromBDForChart(startIdxChart, endIdxChart);
            dataBase.Close();
        }

        public void TimeoutProcesses2(object sender, ElapsedEventArgs e)
        {
            GetDataByReaderForChart();
            if (repeatChart == 0)
            {
                Console.WriteLine("МЫ В ИФ =0");
                Console.WriteLine(repeatChart + "repeatReport");
                Console.WriteLine(remainderIdChart + "remainderIdReport");
                Console.WriteLine(countChart + "===ИЗ===" + repeatChart + " ОТЧЕТ ");
                Console.WriteLine("СТАРТ====" + startIdxChart.ToString(), "ЭНД====" + endIdxChart.ToString());
                timerOfProcessesChart.Stop();
                GetDataByReaderForChart();
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
                //Action action = () => chart1.Series[0].Points.DataBindXY(dataBase.dataForChart.Keys, dataBase.dataForChart.Values);
                //Invoke(action);
                //dataBase.dataForChart.Clear();
                startIdxChart = endIdxChart;
                countChart++;
            }

            else
            {
                Form1 form1 = new Form1();
                Console.WriteLine(countChart + "===ИЗ===" + repeatChart + " ГРАФИК ");
                timerOfProcessesChart.Stop();
                startIdxChart = endIdxChart;
                endIdxChart = startIdxChart + remainderIdChart;
                GetDataByReaderForChart();
                form1.chart1.Series[0].Points.DataBindXY(dataBase.dataForChartInt.Keys, dataBase.dataForChartInt.Values);
                //Action action = () => form1.chart1.Series[0].Points.DataBindXY(dataBase.dataForChart.Keys, dataBase.dataForChart.Values);
                //Invoke(action);
                dataBase.dataForChartInt.Clear();
                countChart = 0;
                remainderIdChart = 0;
                startIdxChart = 0;
                endIdxChart = 0;
            }
        }
        public void StartTimer()
        {
            timerOfProcessesChart.Start();
        }

        
    }
}
