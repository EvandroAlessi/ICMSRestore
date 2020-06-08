using System;
using System.IO;
using System.Reflection;

namespace CrossCutting
{
    /// <summary>
    /// 
    /// </summary>
    public static class LOG
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <param name="parametros"></param>
        public static void Log(string func, string message = null, dynamic[] parameters = null)
        {
            try
            {
                var logPath = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\LOG";

                PathControl.Create(logPath);

                logPath = Path.Combine(logPath, "log.txt");

                using (StreamWriter stream = new StreamWriter(logPath, true))
                {
                    lock (stream)
                    {
                        stream.WriteLine("Data e Hora: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        stream.WriteLine("Função: " + func);
                        if (parameters != null && parameters.Length > 0)
                        {
                            stream.Write("Parametros: ");
                            stream.Write(PropertyReader.GetAllParameters(parameters));
                            stream.WriteLine();
                        }
                        if (!string.IsNullOrEmpty(message))
                        {
                            stream.WriteLine("Mensagem: " + message);
                        }
                        stream.WriteLine(new string('-', 100));
                        stream.WriteLine();

                        stream.Close();
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="ex"></param>
        /// <param name="parametros"></param>
        public static void Log(string func, Exception ex, dynamic parameters = null)
        {
            try
            {
                var logPath = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\LOG";

                PathControl.Create(logPath);

                logPath = Path.Combine(logPath, "log.txt");

                lock (ex)
                {
                    using (StreamWriter stream = new StreamWriter(logPath, true))
                    {
                        lock (stream)
                        {
                            stream.WriteLine("Data e Hora: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                            stream.WriteLine("Função: " + func);
                            if (parameters != null)
                            {
                                stream.Write("Parametros: ");

                                string aux = PropertyReader.GetAllParameters(parameters);

                                stream.Write(aux);
                                stream.WriteLine();
                            }
                            stream.WriteLine("Mensagem: " + ex.Message);
                            stream.WriteLine("Stack Trace: " + ex.StackTrace);
                            stream.WriteLine(new string('-', 100));
                            stream.WriteLine();

                            stream.Close();
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="token"></param>
        /// <param name="body"></param>
        public static void LogRequestFail(string baseUrl, string token, string body, string result = null, int? rtn = null)
        {
            try
            {
                var logPath = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\LOG_REQUEST";

                PathControl.Create(logPath);

                logPath = Path.Combine(logPath, "RequestFail.txt");

                using (StreamWriter stream = new StreamWriter(logPath, true))
                {
                    lock (stream)
                    {
                        stream.WriteLine("Data e Hora da requisição: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        stream.WriteLine("Base Url: " + baseUrl);
                        stream.WriteLine("Token: " + token);
                        stream.WriteLine("Body: " + body);
                        stream.WriteLine("Result: " + result);
                        stream.WriteLine("Return: " + rtn);
                        stream.WriteLine(new string('-', 100));
                        stream.WriteLine();

                        stream.Close();
                    }
                }
            }
            catch { }
        }
    }
}
