using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Net.Mail;

namespace AddNodeProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var outPutDirectory = Environment.CurrentDirectory;
                string filePath = "./Numbers (homework task - desktop developer .NET).txt";
                string[] numbers = ReadFile(filePath);
                StringBuilder logBuilder = new StringBuilder();
                if (numbers != null && numbers.Length > 0)
                {
                    SumNumbers(numbers, logBuilder);
                }

                if (logBuilder != null && logBuilder.Length > 0)
                {
                    PrintLogs(logBuilder.ToString());
                    //SendMail(logBuilder.ToString());
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred"+ ex.Message);
            }
        }

        /// <summary>
        /// This method is used to read txt file contents and return string array of numbers & alphabets
        /// </summary>
        /// <param name="filePath"></param>
        private static string[] ReadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string values = File.ReadAllText(filePath);
                    string[] numbers = values.Split(',');
                    return numbers;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in ReadFile method" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// This method is used to sum numbers and ignore alphabets
        /// It is also extended to sum of even numbers
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="logBuilder"></param>
        public static void SumNumbers(string[] numbers, StringBuilder logBuilder)
        {
            try
            {
                int sum = 0;
                int evenSum = 0;
                logBuilder.Append("Log Session Started at :").Append(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")).Append(Environment.NewLine);
                foreach (var number in numbers)
                {
                    if (Int32.TryParse(number, out var result))
                    {
                        sum += result;
                        if (result % 2 == 0)
                        {
                            evenSum += result;
                        }
                    }
                    else
                    {
                        string message = number + " is not number and was ignored";
                        Console.WriteLine(message);
                        logBuilder.Append(message).Append(Environment.NewLine);
                    }
                }
                Console.WriteLine("Sum of numbers: " + sum);
                logBuilder.Append("Sum of numbers: ").Append(sum).Append(Environment.NewLine);
                Console.WriteLine("Sum of even numbers: " + evenSum);
                logBuilder.Append("Sum of even numbers: ").Append(evenSum).Append(Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in sum method " + ex.Message);

            }
        }

        private static void PrintLogs(string logs)
        {
            try
            {
                string Path = "./Log.txt";
                using (StreamWriter file = File.AppendText(Path))
                {
                    file.Write(logs);
                    file.Close();
                    file.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in PrintLogs method " + ex.Message);
            }
        }

        private static void SendMail(string logs)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("sender mail");

                mail.From = new MailAddress("receiver mail");
                mail.To.Add("vinaysvits31@gmail.com ");
                mail.Subject = "Log Mail at" + DateTime.Now.ToShortDateString();
                mail.Body = "This mail contain logs for Sum application" + "/n" + logs;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in SendMail method " + ex.Message);
            }
        }
    }
}
