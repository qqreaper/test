using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace examen_server
{
    public class Logger
    {
        public Logger() { }
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
    public class Server
    {
        const int port = 8888; 
        Logger logger = new Logger();
        public void StartServer()
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);

                server.Start();

                while (true)
                {
                    logger.Log("Ожидание подключений... ");
                    TcpClient client = server.AcceptTcpClient();
                    logger.Log("Подключен клиент. Выполнение запроса...");
                    NetworkStream stream = client.GetStream();
                    string response = "тест";
                    byte[] data = Encoding.UTF8.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                    logger.Log($"Отправлено сообщение: {response}");
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception e)
            {
                logger.Log(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Server _server = new Server();
            Thread server = new Thread(new ThreadStart(_server.StartServer));
            server.Start();
        }
    }
}
