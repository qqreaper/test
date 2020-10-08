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
        static int port = 8005;
        Logger logger = new Logger();
        public void StartServer()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);


            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {

                listenSocket.Bind(ipPoint);


                listenSocket.Listen(10);
                logger.Log("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();

                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    logger.Log(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());


                    string message = "ваше сообщение доставлено";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.Send(data);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
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
