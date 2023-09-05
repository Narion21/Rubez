using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.IO;
using System.Threading;

namespace Rubez
{
    internal class DataBase
    {

        public NpgsqlConnection npgSqlConnection = null;
        public List<string> listinfo = new List<string>();

        public Dictionary<int, int> dataForChartInt { set; get; }
        public Dictionary<int, float> dataForChartFloat { set; get; }


        public int startId = 0;
        public int endId = 0;


        public delegate void MethodDB();
        public event MethodDB sendFinishReadPartDataForChart;
        public event MethodDB sendFinishReadDataForChart;
        public event MethodDB sendFinishReadPartDataForReport;
        public event MethodDB sendFinishReadDataForReport;
        public DataBase()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            dataForChartInt = new Dictionary<int, int>();
            dataForChartFloat = new Dictionary<int, float>();
        }
        public string MinId(string value1, string value2, string tableName)
        {
            string id = "";
            string com = "SELECT MIN(id) FROM " + tableName + " WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                startId = int.Parse(id);
                reader.Close();
                Console.WriteLine("min id===" + id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public string MaxId(string value1, string value2, string tableName)
        {
            string id = "";
            string com = "SELECT MAX(id) FROM " + tableName + " WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                endId = int.Parse(id);
                reader.Close();
                Console.WriteLine("end id===" + id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public String DataFromBDColumnName(string tableName)
        {
            string result = string.Empty;
            string com = "SELECT string_agg(column_name, ',') FROM (SELECT column_name FROM information_schema.columns WHERE table_name = '" + tableName + "' ORDER BY ordinal_position) AS columns;";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                reader.Read();
                result = reader.GetString(0);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public List<string> DataFromBD(string tableName)
        {
            bool finishRead = false;
            int endIdx = startId + 100000;
            Console.WriteLine(startId + "startId");
            Console.WriteLine(endId + "endId");
            Console.WriteLine(endIdx + "endIdx");
            List<string> listA = new List<string>();
            string com = "SELECT * FROM public." + tableName + " WHERE id >= '" + startId.ToString() + "' AND id <= '" + endIdx.ToString() + "' ORDER BY id asc;";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string data = reader.GetValue(i).ToString();
                        listA.Add(data);
                    }
                    if (int.Parse(reader.GetValue(0).ToString()) == endId)
                    {
                        finishRead = true;
                        break;
                    }
                }
                startId = endIdx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (finishRead)
            {
                sendFinishReadDataForReport();

            }
            else
            {
                sendFinishReadPartDataForReport();
            }
            return listA;
        }

        public void DataFromBDForChart(string columnName, string tableName)
        {
            bool finishRead = false;
            int endIdx = startId + 100000;
            Console.WriteLine(startId + "startId");
            Console.WriteLine(endId + "endId");
            Console.WriteLine(endIdx + "endIdx");

            string com = "SELECT id, " + columnName + " FROM public." + tableName + " WHERE id > '" + startId.ToString() + "' AND id < '" + endIdx.ToString() + "' ORDER BY id asc;";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                while (reader.Read())
                {
                    if (Properties.Settings.Default.dataTypeSwitchC == false)
                    {

                        if (reader.GetValue(1) != null && reader.GetValue(1).ToString() != string.Empty)
                        {
                            int id = int.Parse(reader.GetValue(0).ToString());
                            int fotoreque = int.Parse(reader.GetValue(1).ToString());
                            dataForChartInt.Add(id, fotoreque);
                        }
                    }
                    if (Properties.Settings.Default.dataTypeSwitchC == true)
                    {

                        if (reader.GetValue(1) != null && reader.GetValue(1).ToString() != string.Empty)
                        {
                            int id1 = int.Parse(reader.GetValue(0).ToString());
                            float fotoreque1 = float.Parse(reader.GetValue(1).ToString());
                            dataForChartFloat.Add(id1, fotoreque1);
                        }
                    }

                    if (int.Parse(reader.GetValue(0).ToString()) == endId)
                    {

                        finishRead = true;
                        break;
                    }
                }
                startId = endIdx;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ошибка в DataFromBDForChart");
            }

            if (finishRead)
            {
                sendFinishReadDataForChart();
            }
            else
            {
                sendFinishReadPartDataForChart();
            }
        }

        public void Conn()
        {
            string dataBaseName = Properties.Settings.Default.comboDataTbC;
            string connectionString = "Server=" + Properties.Settings.Default.ipTbC + ";Port=" + Properties.Settings.Default.portTbC + ";Username=" + Properties.Settings.Default.loginTbC + ";Password=" + Properties.Settings.Default.passwordTbC;
            if (dataBaseName != string.Empty)
            {
                connectionString += ";Database = " + Properties.Settings.Default.comboDataTbC;
            }
            Console.WriteLine(connectionString);
            try
            {
                npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                Console.WriteLine("db conn");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Close()
        {
            try
            {
                if (npgSqlConnection.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("db close");
                    npgSqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DataTable ShowDbName()
        {
            string comT = "SELECT datname FROM pg_database;";
            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            DataTable dt = new DataTable();
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
        public DataTable ShowTbName()
        {
            Conn();
            string comT = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";
            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            DataTable dt = new DataTable();
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public DataTable ShowColumnName(string tableName)
        {
            Conn();
            string comT = "SELECT column_name FROM information_schema.columns WHERE table_name = '" + tableName + "';";
            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            DataTable dt = new DataTable();
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }
    }
}



