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
        private static object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        /// <param name="repeated"></param>
        public static void Log(string func, string message = null, dynamic parameters = null, bool repeated = false)
        {
            try
            {
                lock (locker)
                {
                    var logPath = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\LOG";

                    PathControl.Create(logPath);

                    logPath = Path.Combine(logPath, (repeated ? "REPEATED_FILES.txt" : "LOG_ERROR.txt"));

                    using (FileStream file = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.None))
                    {
                        using (StreamWriter stream = new StreamWriter(file))
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
                                stream.WriteLine("Mensagem: " + message);
                                stream.WriteLine(new string('-', 100));
                                stream.WriteLine();

                                stream.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n\n\n");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="ex"></param>
        /// <param name="parameters"></param>
        public static void Log(string func, Exception ex, dynamic parameters = null, bool repeated = false)
        {
            try
            {
                lock (locker)
                {
                    var logPath = $"{ Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) }\\LOG";

                    PathControl.Create(logPath);

                    logPath = Path.Combine(logPath, (repeated ? "REPEATED_FILES.txt" : "LOG_ERROR.txt"));

                    lock (ex)
                    {
                        using (FileStream file = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.None))
                        {
                            using (StreamWriter stream = new StreamWriter(file))
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
                                    stream.WriteLine("Mensagem: " + ex?.Message);
                                    stream.WriteLine("Stack Trace: " + ex?.StackTrace);
                                    stream.WriteLine(new string('-', 100));
                                    stream.WriteLine();

                                    stream.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n\n\n");
            }
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
