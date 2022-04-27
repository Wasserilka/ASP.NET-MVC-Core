using System.Data.SQLite;

namespace Lesson_8.Data
{
    interface IConnectionManager
    {
        public string ConnectionString { get; }
        public SQLiteConnection connection { get; set; }

        public SQLiteConnection GetOpenedConnection();
    }

    class ConnectionManager : IConnectionManager
    {
        public string ConnectionString { get; }
        public SQLiteConnection connection { get; set; }

        public ConnectionManager()
        {
            ConnectionString = "Data Source=worksheet.db;Version=3;Pooling=true;Max Pool Size=100;";
        }

        public SQLiteConnection GetOpenedConnection()
        {
            connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
