using System;
using System.IO;

namespace Fluxo.Utils
{
    public static class Logger
    {
        private static readonly string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static readonly string logFile = Path.Combine(logPath, $"fluxo_{DateTime.Now:yyyyMMdd}.log");

        static Logger()
        {
            // Cria o diretório de logs se não existir
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            var errorMessage = ex != null ? $"{message} - Exceção: {ex.Message}" : message;
            WriteLog("ERROR", errorMessage);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARNING", message);
        }

        private static void WriteLog(string level, string message)
        {
            try
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(logFile, logEntry + Environment.NewLine);
                
                // Também escreve no console para debug
                Console.WriteLine(logEntry);
            }
            catch
            {
                // Se não conseguir escrever no log, apenas ignora para não quebrar a aplicação
            }
        }
    }
}