using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using LakeLabLib;
using SQLitePCL;

namespace LakeLabPiApp
{
    class SQLiteDbReader
    {
        public List<ValueItemModel> ReadDb(string databasePath)
        {
            if (string.IsNullOrEmpty(databasePath))
                throw new Exception("The databasePath parameter is null or emtpy!");

            List<ValueItemModel> items = new List<ValueItemModel>();

            using (var connection = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = databasePath }))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var selectCommand = connection.CreateCommand();
                    selectCommand.Transaction = transaction;
                    if(databasePath.Contains("DO"))
                    {
                        selectCommand.CommandText = "SELECT timestamp, val1 FROM DO";
                    }
                    else
                    {
                        selectCommand.CommandText = "SELECT timestamp, val1 FROM RTD";
                    }
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new ValueItemModel(reader.GetDateTime(0), reader.GetFloat(1)));
                        }
                    }
                    transaction.Commit();
                }
            }
            return items;
        }

        private bool CheckIfColumnExists(string databasePath, string tableName, string columnName)
        {
            using (var conn = new SqliteConnection("" + new SqliteConnectionStringBuilder { DataSource = databasePath }))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("PRAGMA table_info({0})", tableName);

                var reader = cmd.ExecuteReader();
                int nameIndex = reader.GetOrdinal("Name");
                while (reader.Read())
                {
                    if (reader.GetString(nameIndex).Equals(columnName))
                    {
                        conn.Close();
                        return true;
                    }
                }
                conn.Close();
            }
            return false;
        }

    }
}
