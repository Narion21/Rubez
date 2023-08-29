using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Npgsql;
using System.Threading;
using System.Timers;

namespace Rubez
{
    internal class CsvReport
    {
        DataBase dataBase = new DataBase();

        public int step = 10000;
        public int startIdxReport = 0;
        public int endIdxReport = 0;
        int countReport = 0;
        public int repeatReport = 0;
        public int remainderIdReport = 0;
        public string savePath = "";

        System.Timers.Timer timerOfProcessesReport = new System.Timers.Timer();


        public CsvReport()
        {
            timerOfProcessesReport.Elapsed += new System.Timers.ElapsedEventHandler(TimeoutProcesses);
            timerOfProcessesReport.Interval = 1000;
        }


        public void TimeoutProcesses(object sender, ElapsedEventArgs e)
        {
            if (repeatReport == 0)
            {
                Console.WriteLine("МЫ В ИФ =0");
                Console.WriteLine(repeatReport + "repeatReport");
                Console.WriteLine(remainderIdReport + "remainderIdReport");
                Console.WriteLine(countReport + "===ИЗ===" + repeatReport + " ОТЧЕТ ");
                Console.WriteLine("СТАРТ====" + startIdxReport.ToString(), "ЭНД====" + endIdxReport.ToString());
                timerOfProcessesReport.Stop();
                GetDataByReader();
                countReport = 0;
                remainderIdReport = 0;
                startIdxReport = 0;
                endIdxReport = 0;
            }

            else if ((countReport != repeatReport) & (repeatReport != 0))
            {

                Console.WriteLine("МЫ В ИФ");
                Console.WriteLine(repeatReport + "repeatReport");
                Console.WriteLine(remainderIdReport + "remainderIdReport");
                Console.WriteLine(countReport + "===ИЗ===" + repeatReport + " ОТЧЕТ ");
                Console.WriteLine("СТАРТ====" + startIdxReport.ToString(), "ЭНД====" + endIdxReport.ToString());
                countReport++;
                endIdxReport = startIdxReport + step;
                GetDataByReader();
                startIdxReport = endIdxReport;
            }

            else
            {
                Console.WriteLine("МЫ В ЭЛС");
                Console.WriteLine(repeatReport + "repeatReport");
                Console.WriteLine(remainderIdReport + "remainderIdReport");
                Console.WriteLine(countReport + "===ИЗ===" + repeatReport + " ОТЧЕТ ");
                Console.WriteLine("СТАРТ====" + startIdxReport.ToString(), "ЭНД====" + endIdxReport.ToString());
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
        public void GetDataByReader()
        {
            dataBase.Conn();
            List<string> tempList = dataBase.DataFromBD(startIdxReport, endIdxReport, Properties.Settings.Default.comboTableTbC);
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

        public void StartTimer()
        {
            timerOfProcessesReport.Start();
        }
    }
}
