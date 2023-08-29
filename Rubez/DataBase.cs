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

        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // перенесла с формы1 внутрь бд эти переменные
        private int startIdxChart = 0;
        private int endIdxChart = 0;

        /*      
            * MethodDB - это название даешь сам какое хочешь
            * emitSendFinishReadPartOfDataForChart - это тоже название даешь какое хочешь
            * я для себя взяла за правило использовать приписки emit - это вызов, Send отправка
            * можешь переделать пот себя как хочешь, можешь в именовании Send убрать или emit убрать
            * тут как бы все на любителя
            * почитай про эвенты в шарпах !!!!!!!!!!!!
            * логика в кратце 1 класс "кричит" 2 класс это "слышит"
            * после чего выполняется нужная тебе логика, это удобно для тех моментов,
            * когда ты точно не знаешь сколько времени будет выполняться 1я часть кода
            * но тебе надо выполнить что-то (2ю часть кода), ТОЛЬКО после завершения 1й
            * в этом случае сигналы отличное решение, т.к. ты подвязываешься на события
            * и просто делаешь то что тебе надо, когда они сработают
        */
        public delegate void MethodDB();
        public event MethodDB emitSendFinishReadPartOfDataForChart;
        public event MethodDB emitSendFinishReadDataForChart;
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

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
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                // определяю начало ид
                startIdxChart = int.Parse(id);
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                // определяю конец ид
                endIdxChart = int.Parse(id);
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

        // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        /*
            * перенесла логику как и говорила, что по сути не нужны на стороне форм1 начало и конец,
            * лучше определить их в бд тк они используются только на этой стороне     
            * + у тебя не верное неравенство, ты ставишь левую точку > поэтому у тебя пропускается 1 точка всегда
            * типа вот пример, начало ид=1 конец ид=20, шаг в 10, в твоем условии ид > 1 и ид < 10 будет давать значения 2,3,4,5,6,7,8,9
            * след. шагом будет ид > 10 и ид < 20 и вернутся значения 11,12,13,14,15,16,17,18,19
            * т.е. значения 1,10,20 всегда пропускаешь
            * поэтому лучше всего изменить 1 из сторон на более четкое неравенство или обе границы, проверь этот момент
            * Дальше, я убрала внутрь класса бд 2 позиции начало и конца и внутри делаю +100к
        */
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public void DataFromBDForChart(int startPos, int endPos, string columnName, string tableName)
        {
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            // эта переменная нужна, чтобы понять мы прочитали все что надо было или только кусок
            // по умолчанию - только кусок
            bool finishRead = false;
            int endIdx = startIdxChart + 100000;

            // ВЫНЕСИ это конструтор БД
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); 

            // заменила переменные начало и конца на новые - можно убрать из параметров эти 2 переменные, тк они по факту и не нужны
            string com = "SELECT id, " + columnName + " FROM public." + tableName + " WHERE id > '" + startIdxChart.ToString() 
                + "' AND id < '" + endIdx.ToString() + "' ORDER BY id asc;";
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


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

                    // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    /*
                     * тут проверяю, дошли ли мы до нужного ид - если оно совпадает с концом
                     * то меняем флаг на на тру, тк нашли конечный ид, значит выкачали ВСЕ что надо
                     * и делаем брейк, что нам даст принудительный выход из цикла                  
                    */
                    if (int.Parse(reader.GetValue(0).ToString()) == endIdxChart)
                    {
                        finishRead = true;
                        break;
                    }
                    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                }
                // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                // переназначаю стартИд для того чтобы читать потом нвоый кусок если считали не все
                startIdxChart = endIdx;
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "ошибка в DataFromBDForChart");
            }


            Console.WriteLine("dataForChartInt count: " + dataForChartInt.Count);
            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            /*
             * тут я проверяю, если флаг в фолс, значит считали часть и отдаю сигнал на форм1, что прочитали часть
             * иначе сигнал о том что закончили читать все что надо 
            */
            if (finishRead)
                emitSendFinishReadDataForChart();
            else
                emitSendFinishReadPartOfDataForChart();
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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



