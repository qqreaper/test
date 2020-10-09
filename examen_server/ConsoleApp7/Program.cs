using System;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp7
{
    
    class Program
    {
        private static string Ip = "127.0.0.1";
        private static int port = 8888;
        static void Main(string[] args)
        {
            TemplateServer template = new TemplateServer();
            TcpListener server = null;
            try
            {
                server = template.CreateNewTcpListener(template.ParseIpAdress(Ip), port);

                // запуск слушателя
                server.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();
                    template.WriteLog("Подключен клиент. Выполнение запроса...");

                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();

                    // сообщение для отправки клиенту
                    string response = "Привет мир";
                    // преобразуем сообщение в массив байтов
                    byte[] data = Encoding.UTF8.GetBytes(response);

                    // отправка сообщения
                    stream.Write(data, 0, data.Length);
                    template.WriteLog(string.Format("Отправлено сообщение: {0}", response).ToString());
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
            }
            catch (Exception e)
            {
                template.WriteLog(e.Message);
            }
            //finally
            //{
            //    if (server != null)
            //        server.Stop();
            //}

        }
    }
}