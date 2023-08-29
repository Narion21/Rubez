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

        public DataBase()
        {
            dataForChartInt = new Dictionary<int, int>();
            dataForChartFloat = new Dictionary<int, float>();
        }
        public string MinId(string value1, string value2)
        {
            string id = "";
            string com = "SELECT MIN(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
                Console.WriteLine("min===" + id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public string MaxId(string value1, string value2)
        {
            string id = "";
            string com = "SELECT MAX(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetValue(0).ToString();
                }
                reader.Close();
                Console.WriteLine("max===" + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return id;
        }

        public int IdCount(string value1, string value2, string tableName)
        {
            int countId = 0;
            string com = "SELECT MIN(id), MAX(id) FROM " + tableName + " WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                string id1 = "";
                string id2 = "";
                while (reader.Read())
                {
                    id1 = reader.GetValue(0).ToString();
                    id2 = reader.GetValue(1).ToString();
                }
                countId = int.Parse(id2) - int.Parse(id1);
                Console.WriteLine("min===" + id1);
                Console.WriteLine("max===" + id2);
                Console.WriteLine("count===" + countId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return countId;
        }

        public List<string> DataFromBD(int startPos, int endPos, string tableName)
        {
            List<string> listA = new List<string>();
            string com = "SELECT * FROM public." + tableName + " WHERE id >= '" + startPos.ToString() + "' AND id <= '" + endPos.ToString() + "' ORDER BY id asc;";
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return listA;
        }

        public void DataFromBDForChart(int startPos, int endPos, string columnName, string tableName)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string com = "SELECT id, " + columnName + " FROM public." + tableName + " WHERE id > '" + startPos.ToString() + "' AND id < '" + endPos.ToString() + "' ORDER BY id asc;";
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
                    else if (Properties.Settings.Default.dataTypeSwitchC == true)
                    {
                        if (reader.GetValue(1) != null && reader.GetValue(1).ToString() != string.Empty)
                        {
                            int id1 = int.Parse(reader.GetValue(0).ToString());
                            float fotoreque1 = float.Parse(reader.GetValue(1).ToString());
                            Console.WriteLine(id1.ToString());
                            Console.WriteLine(fotoreque1.ToString());
                            dataForChartFloat.Add(id1, fotoreque1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ошибка в DataFromBDForChart");
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



