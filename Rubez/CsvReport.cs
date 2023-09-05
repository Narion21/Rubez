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

namespace Rubez
{
    internal class CsvReport
    {
        DataBase dataBase = new DataBase();
        NpgsqlConnection npgSqlConnection = null;

        public void Csv(string value1, string value2)
        {

            
            string com = "SELECT * FROM public.devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "' ORDER BY id asc;";


            DataTable dt = new DataTable();
            //dataBase.DataFromBD(com, dt);
            var csv = new StringBuilder();
            foreach (DataRow row in dt.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    csv.Append(item.ToString() + ",");
                }
                csv.AppendLine();
            }
            SaveFileDialog sf = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            sf.Filter = filter;
            StreamWriter writer = null;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                filter = sf.FileName;
                writer = new StreamWriter(filter);
                writer.WriteLine(csv);
                writer.Close();
            }







            //return (dt);
        }

        public void TestCSV()
        {
            Form1 form1 = new Form1();
            string connectionString = "Server=" + Properties.Settings.Default.ipTbC + ";Port=" + Properties.Settings.Default.portTbC + ";Username=" + Properties.Settings.Default.loginTbC + ";Password=" + Properties.Settings.Default.passwordTbC + ";Database=" + Properties.Settings.Default.comboDataTbC;
            Console.WriteLine(connectionString);
            npgSqlConnection = new NpgsqlConnection(connectionString);
            NpgsqlCommand com2 = new NpgsqlCommand("SELECT * FROM public.devicestable WHERE daytime >= '" + form1.dTStart.Text + "' AND daytime <= '" + form1.dTFinish.Text + "' ORDER BY id asc;");
            NpgsqlTransaction transaction = null;
            npgSqlConnection.Open();
            transaction = npgSqlConnection.BeginTransaction();
            com2.Transaction = transaction;
            com2.ExecuteReader();

            transaction.Commit();
            dataBase.Close();
        }

    }
}
