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

        NpgsqlConnection npgSqlConnection = null;


        public string ip { set; get; }
        public string port { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string db { set; get; }


        public List<string> listinfo = new List<string>();

        public int MinId(string value1, string value2)
        {

            string com = "SELECT MIN(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            NpgsqlDataReader reader = comDB.ExecuteReader();
            string id = "";
            int id1 = 0;

            while (reader.Read())
            {
                id = reader.GetValue(0).ToString();
                id1 += int.Parse(id);

            }
            reader.Close();

            Console.WriteLine("min===" + id1);
            return id1;
            
        }

        

        public int MaxId(string value1, string value2)
        {
            string com = "SELECT MAX(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            NpgsqlDataReader reader = comDB.ExecuteReader();
            string id = "";
            int id1 = 0;

            while (reader.Read())
            {
                id = reader.GetValue(0).ToString();
                id1 += int.Parse(id);

            }
            reader.Close();
            Console.WriteLine("max===" + id1);
            return id1;






        }


        public int IdCount(string value1, string value2)
        {
            int countId = 0;
            string com = "SELECT MIN(id), MAX(id) FROM devicestable WHERE daytime >= '" + value1 + "' AND daytime <= '" + value2 + "'";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
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
            return countId;
        }


        public List<string> DataFromBD(int startPos, int endPos)
        {
            List<string> listA = new List<string>();
            string com = "SELECT * FROM public.devicestable WHERE id >= '" + startPos.ToString() + "' AND id <= '" + endPos.ToString() + "' ORDER BY id asc;";
            Console.WriteLine(com);
            NpgsqlCommand comDB = new NpgsqlCommand(com, npgSqlConnection);
            NpgsqlDataReader reader = comDB.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string data = reader.GetValue(i).ToString();
                    listA.Add(data);
                }
            }

            return listA;
        }



        public void Conn()
        {
            bool result = true;

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
                result = false;
                Console.WriteLine(ex.Message);

            }

        }
        public void Close()
        {
            bool result = true;
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
                result = false;
                Console.WriteLine(ex.Message);
            }

        }

        public DataTable ShowDbName()
        {
            string comT = "SELECT datname FROM pg_database;";
            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable ShowTbName()
        {
            try
            {
                string connectionString = "Server=" + ip + ";Port=" + port + ";Username=" + username + ";Password=" + password + ";Database=" + db;
                Console.WriteLine(connectionString);
                npgSqlConnection = new NpgsqlConnection(connectionString);
                //npgSqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string comT = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";
            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataSet Chart(string value1)
        {
            string connectionString = "Server=" + Properties.Settings.Default.ipTbC + ";Port=" + Properties.Settings.Default.portTbC + ";Username=" + Properties.Settings.Default.loginTbC + ";Password=" + Properties.Settings.Default.passwordTbC + ";Database=" + Properties.Settings.Default.comboDataTbC;
            Console.WriteLine(connectionString);
            npgSqlConnection = new NpgsqlConnection(connectionString);
            NpgsqlCommand com = new NpgsqlCommand(value1, npgSqlConnection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }





        public void DataToCsv(string value1, DataTable value2)
        {
            //string connectionString = "Server=" + Properties.Settings.Default.ipTbC + ";Port=" + Properties.Settings.Default.portTbC + ";Username=" + Properties.Settings.Default.loginTbC + ";Password=" + Properties.Settings.Default.passwordTbC + ";Database=" + Properties.Settings.Default.comboDataTbC;
            //npgSqlConnection = new NpgsqlConnection(connectionString);
            string comT = (value1);

            NpgsqlCommand com = new NpgsqlCommand(comT, npgSqlConnection);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            da.Fill(value2);


        }



    }
}
