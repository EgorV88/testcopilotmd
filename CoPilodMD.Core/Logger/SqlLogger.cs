using NLog;
using System.Data.SQLite;

namespace CoPilodMD.Core
{
    public static class SqlLogger
    {
        private static Logger logger;

        static SqlLogger()
        {
            CreateLogDbIfNeed();
        }

        public static void InitLogger(Logger log)
        {
            logger = log;

        }

        public static void Info(string message)
        {
            logger?.Info(message);
        }

        public static void Error(string message, Exception ex = null)
        {
            if (ex != null)
                logger?.Error(ex, message);
            else
                logger?.Error(message);
        }

        public static void CreateLogDbIfNeed()
        {
            if (File.Exists("../Log.db3"))
                return;

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=../Log.db3;Version=3;"))
            using (SQLiteCommand command = new SQLiteCommand(
             "CREATE TABLE Log (Timestamp TEXT, Loglevel TEXT, Logger TEXT, Message TEXT)",
             connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
            }

        }

    }
}
