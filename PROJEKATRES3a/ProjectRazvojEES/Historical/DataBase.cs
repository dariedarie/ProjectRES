using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using InterfaceLibrary1;
using Logger;
using System.Diagnostics.CodeAnalysis;

namespace Historical
{
    [ExcludeFromCodeCoverage]
    public class DataBase
    {
        public SQLiteConnection myConnection;
        LoggerClass l = new LoggerClass();
        List<HistoricalProperty> historicalProperties = new List<HistoricalProperty>();

        public DataBase()
        {
            myConnection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                System.Console.WriteLine("Created...");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }

        public ECode ConvertToEnum(string cod)
        {
            ECode code = (ECode)Enum.Parse(typeof(ECode), cod);
            return code;
        }
        public void CreateBase(string dataset)
        {
            OpenConnection();
            string sql = "CREATE TABLE IF NOT EXISTS tabela" + dataset + " (code VARCHAR NOT NULL, value INT NOT NULL, timestamp DATETIME)";
            SQLiteCommand myCommand = new SQLiteCommand(sql, myConnection);
            myCommand.ExecuteNonQuery();
            CloseConnection();
            l.LogEvent("Historical", "Kreirana baza.");
        }
        public void WriteToBase(ECode code, int value, string dataset)
        {
            HistoricalClass cl = new HistoricalClass();
            OpenConnection();
            string upis = "INSERT INTO tabela" + dataset + " ('code', 'value', 'timestamp') VALUES (@code, @value, @timestamp)";
            SQLiteCommand myCommand = new SQLiteCommand(upis, myConnection);
            myCommand.Parameters.AddWithValue("@code", code);
            myCommand.Parameters.AddWithValue("@value", value);
            myCommand.Parameters.AddWithValue("@timestamp", cl.timestamp);
            myCommand.ExecuteNonQuery();
            CloseConnection();
            l.LogEvent("Historical", "Upisano u bazu...");
        }

        public List<HistoricalProperty> ReturnFromBase2(string dataset)
        {
            if (Int32.Parse(dataset) < 1 || Int32.Parse(dataset) > 5)
            {
                throw new ArgumentOutOfRangeException("Moze biti u rangu [1, 5].");
            }

            List<HistoricalProperty> lista = new List<HistoricalProperty>();
            string upis = "";
            try
            {
                upis = "SELECT * FROM tabela" + dataset;
                SQLiteCommand myCommand = new SQLiteCommand(upis, myConnection);
                OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        string code = result.GetString(0);

                        ECode kod = ConvertToEnum(code);
                        int value = result.GetInt32(1);
                        HistoricalProperty hist = new HistoricalProperty(kod, value);

                        lista.Add(hist);


                        //    Console.WriteLine("Code : {0} - Value : {1} Timestamp {2}", result["Code"], result["Value"], result["timestamp"]);
                    }
                }
                CloseConnection();
                l.LogEvent("Historical", "Preuzeta vrednost iz baze..");
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ova tabela ne postoji 2!");
            }
        }

        
        public List<HistoricalProperty> ReturnFromBase(DateTime pocetak, DateTime kraj, string dataset)
        {
            if (Int32.Parse(dataset) < 1 || Int32.Parse(dataset) > 5)
            {
                throw new ArgumentOutOfRangeException("Moze biti u rangu [1, 5].");
            }

            List<HistoricalProperty> lista = new List<HistoricalProperty>();
            string upis = "";
            try
            {
                upis = "SELECT * FROM tabela" + dataset + " where timestamp >= @pocetak and timestamp <= @kraj";
                SQLiteCommand myCommand = new SQLiteCommand(upis, myConnection);
                myCommand.Parameters.AddWithValue("@pocetak", pocetak);
                myCommand.Parameters.AddWithValue("@kraj", kraj);
                OpenConnection();
                SQLiteDataReader result = myCommand.ExecuteReader();
                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        string code = result.GetString(0);

                        ECode kod = ConvertToEnum(code);
                        int value = result.GetInt32(1);
                        HistoricalProperty hist = new HistoricalProperty(kod, value);
                        lista.Add(hist);
                        //    Console.WriteLine("Code : {0} - Value : {1} Timestamp {2}", result["Code"], result["Value"], result["timestamp"]);
                    }
                }
                CloseConnection();
                l.LogEvent("Historical", "Preuzeta vrednost iz baze..");
                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Ova tabela ne postoji 2!");
            }
        }

    }
}
