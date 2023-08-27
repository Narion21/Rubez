using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

namespace Rubez
{
    internal class Chart
    {
        //DataBase dataBase = new DataBase();
        

        /*
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
                string com = "SELECT daytime, fotoreque, id FROM public.devicestable WHERE daytime >= '" + dTStart.Text + "' AND daytime <= '" + dTFinish.Text + "' ORDER BY id asc;";
                DataSet ds = dataBase.Chart(com);
                Console.WriteLine(com);
                dataBase.Close();


            }
            
            

        }
        */

        public DataSet dataToChart(string value1, string value2)
        {
            DataBase dataBase = new DataBase();
            Form1 form1 = new Form1();
            string com = "SELECT daytime, fotoreque, id FROM public.devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "' ORDER BY id asc;";
            DataSet ds = dataBase.Chart(com);
            return (dataBase.Chart(com));

        }
       
    }
}
