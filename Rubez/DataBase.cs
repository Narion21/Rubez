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
        Chart chart = new Chart();

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

        public int IdCount(string value1, string value2)
        {
            int countId = 0;
            string com = "SELECT MIN(id), MAX(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
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

        public List<string> DataFromBD(int startPos, int endPos)
        {
            List<string> listA = new List<string>();
            string com = "SELECT * FROM public.devicestable WHERE id >= '" + startPos.ToString() + "' AND id <= '" + endPos.ToString() + "' ORDER BY id asc;";
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

        public void DataFromBDForChart(int startPos, int endPos, Dictionary<string, int> value1)
        {
            string com = "SELECT id, fotoreque FROM public.devicestable WHERE id >= '" + startPos.ToString() + "' AND id <= '" + endPos.ToString() + "' ORDER BY id asc;";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);

            try
            {
                NpgsqlDataReader reader = comDB.ExecuteReader();
                while (reader.Read())
                {
                    // не имеет смысла инициализировать переменную и 2й строкой пихать в нее данные
                    // это не красиво и путает + лишнии строки, лучше не привыкай так писать
                    // string id = reader.GetValue(0).ToString(); - так лучше
                    string id;
                    int fotoreque;
                    int test; // не используемая переменная
                    id = reader.GetValue(0).ToString(); //Console.WriteLine(id + "   id");
                    fotoreque = Convert.ToInt32(reader.GetValue(1).ToString()); //Console.WriteLine(fotoreque + "   fotoreque");
                    
                    value1.Add(id, fotoreque);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Ошибка в DataFromBDForChart");
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

        public DataSet Chart(string value1)
        {
            DataSet ds = new DataSet();
            Conn();
            NpgsqlCommand com = new NpgsqlCommand(value1, npgSqlConnection);
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;

        }
    }
}



