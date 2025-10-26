using NLog;

namespace CoPilodMD.Core
{
    public static class SqlLogger
    {
        private static Logger logger;

        static SqlLogger()
        {
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

    }
}
