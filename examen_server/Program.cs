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
        public void GetInfo() { 
        //обращение к бд которое не запилил калинин
        //return(data);
        }
        public void SetInfo(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            //обращение к бд которое не запилил калинин
            string response = /*результат из бд*/ "";
            byte[] data = Encoding.UTF8.GetBytes(response);
            stream.Write(data, 0, data.Length);
            logger.Log($"Отправлены данные клиенту: {response}");
            stream.Close();
        }
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
                    logger.Log("Подключен клиент.");
                    
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
