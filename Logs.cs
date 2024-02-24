using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alpha4
{
    public class Logs
    {
        /// <summary>
        /// This method is used when something happens successfully in the program and then it is written to the log
        /// </summary>
        private static string logFilePath = "logs/logs.json";
        public static void LogSuccess(String s)
        {
            var successLog = new
            {
                Timestamp = DateTime.Now,
                SuccessMessage = s
            };

            string jsonSuccessLog = JsonSerializer.Serialize(successLog, new JsonSerializerOptions { WriteIndented = true });

            string existingContent = File.Exists(logFilePath) ? File.ReadAllText(logFilePath) : "";

            string updatedContent = existingContent + Environment.NewLine + jsonSuccessLog;

            File.WriteAllText(logFilePath, updatedContent);
        }

        /// <summary>
        /// This method is used when an error occurs in the program and then it is written to the log
        /// </summary>
        /// <param name="ex"></param>
        public static void LogError(Exception ex)
        {
            var errorLog = new
            {
                Timestamp = DateTime.Now,
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace
            };

            string jsonErrorLog = JsonSerializer.Serialize(errorLog, new JsonSerializerOptions { WriteIndented = true });

            string existingContent = File.Exists(logFilePath) ? File.ReadAllText(logFilePath) : "";

            string updatedContent = existingContent + Environment.NewLine + jsonErrorLog;

            File.WriteAllText(logFilePath, updatedContent);
        }
    }
}
