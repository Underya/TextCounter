using System;
using System.Collections.Generic;

using System.Data.SQLite;

namespace TextCounter
{
    class SaveWordCount :
        IRecipientWord
    {
        int InsertRowTime = 50;

        public void SetResult(string URL, Dictionary<string, int> CountWord)
        {
            using (SQLiteConnection connect = new SQLiteConnection("Data Source=WordCount.db;"))
            {
                connect.Open();
                int IdSiteRow = AddSiteRowAndReturnID(URL, connect);
                AddCountWordResult(connect, CountWord, IdSiteRow);
            }
        }
        int AddSiteRowAndReturnID(string URL, SQLiteConnection connect)
        {
            AddSite(URL, connect);
            int result = ReturnLastInsertID(connect);
            return result;
        }
        void AddSite(string URL, SQLiteConnection connect)
        {
            string time = DateTime.Now.ToString();
            string commandText = string.Format("INSERT INTO site (url, time) values (\"{0}\", \"{1}\")", URL, time);
            SQLiteCommand command = new SQLiteCommand(commandText, connect);
            command.ExecuteNonQuery();
        }
        int ReturnLastInsertID(SQLiteConnection connect)
        {
            string GetIdCommand = "SELECT last_insert_rowid()";
            SQLiteCommand command2 = new SQLiteCommand(GetIdCommand, connect);
            object result = command2.ExecuteScalar();
            return (int)(long)result;
        }
        
        void AddCountWordResult(SQLiteConnection connect, Dictionary<string, int> CountWord, int IDSite)
        {
            Console.WriteLine("Запись данных в локальную БД");
            SQLiteCommand command = new SQLiteCommand(connect);
            AddWord(CountWord, IDSite, command);
        }
        //Если добавлять строки по 1, то 
        void AddWord(Dictionary<string, int> CountWord, int IDSite, SQLiteCommand command)
        {
            List<KeyValuePair<string, int>> wordToAdd = new List<KeyValuePair<string, int>>();
            int currentWordNumber = 0; int SummaryCount = CountWord.Count, i = 0;
            foreach (var pair in CountWord)
            {
                i++;
                wordToAdd.Add(pair);
                currentWordNumber++;
                if (currentWordNumber >= InsertRowTime)
                {
                    AddRowBuffer(IDSite, command, wordToAdd);

                    wordToAdd.Clear();
                    currentWordNumber = 0;
                    Console.WriteLine("Строка номер:{0} из {1}", i, SummaryCount);
                }
            }
            AddRowBuffer(IDSite, command, wordToAdd);
        }
        void AddRowBuffer(int IDSite, SQLiteCommand command, List<KeyValuePair<string, int>> wordToAdd)
        {
            string queryBase = "INSERT INTO wordcount(word, count, idsite) VALUES ";
            foreach (var word in wordToAdd)
            {
                //необходимо экранировать некоторые строки, что бы не было проблем
                queryBase += string.Format("(\"{0}\", \"{1}\", \"{2}\") ,", word.Key, word.Value, IDSite);
            }
            //Надо убрать последнюю запятую в опросе
            queryBase = queryBase.Substring(0, queryBase.Length - 2);
            command.CommandText = queryBase;
                command.ExecuteNonQuery();
        }
    }
}
