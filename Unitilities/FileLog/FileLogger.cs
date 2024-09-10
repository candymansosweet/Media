using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Utility.FileLog
{
    public static class FileLogger
    {
        private static string filePath = "log.txt";
        public static void WriteLog(string message)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    string logEntry = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {message}";
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        public static string ReadLog(string filePath)
        {
            string logContent = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    logContent = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading log file: {ex.Message}");
            }
            return logContent;
        }

        public static void ResetLogFile(string filePath)
        {
            try
            {
                File.WriteAllText(filePath, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting log file: {ex.Message}");
            }
        }
        public static void EndSession()
        {
            try
            {
                // Ghi dòng gạch ngang để kết thúc phiên log
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine("**********");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
        private static bool IsLastLineSeparator()
        {
            // Kiểm tra xem dòng cuối cùng có phải là dòng gạch ngang hay không
            try
            {
                string lastLine = string.Empty;
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lastLine = line;
                    }
                }
                return lastLine.Trim() == "**********";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading log file: {ex.Message}");
                return false;
            }
        }
    }
}