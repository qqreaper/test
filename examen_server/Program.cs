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
    //public class Logger
    //{
    //    public Logger() { }
    //    public void Log(string message)
    //    {
    //        Console.WriteLine(message);
    //    }
    //}
    //public class Server
    //{
    //    static int port = 8005;
    //    Logger logger = new Logger();
    //    public void StartServer()
    //    {
    //        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);


    //        Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //        try
    //        {

    //            listenSocket.Bind(ipPoint);


    //            listenSocket.Listen(10);
    //            logger.Log("Сервер запущен. Ожидание подключений...");

    //            while (true)
    //            {
    //                Socket handler = listenSocket.Accept();

    //                StringBuilder builder = new StringBuilder();
    //                int bytes = 0;
    //                byte[] data = new byte[256];

    //                do
    //                {
    //                    bytes = handler.Receive(data);
    //                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
    //                }
    //                while (handler.Available > 0);

    //                logger.Log(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());


    //                string message = "ваше сообщение доставлено";
    //                data = Encoding.Unicode.GetBytes(message);
    //                handler.Send(data);

    //                handler.Shutdown(SocketShutdown.Both);
    //                handler.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Log(ex.Message);
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Server _server = new Server();
    //        Thread server = new Thread(new ThreadStart(_server.StartServer));
    //        server.Start();
    //    }
    //}
    abstract class TemplateAbstract
    {
        public abstract IPAddress ParseIpAdress(string Ip);

        public abstract TcpListener CreateNewTcpListener(IPAddress localAddr, int port);

        public abstract void WriteLog(string log);
    }
    class TemplateServer : TemplateAbstract
    {
        public override IPAddress ParseIpAdress(string Ip)
        {
            IPAddress localAddr = IPAddress.Parse(Ip);

            return localAddr;
        }
        public override TcpListener CreateNewTcpListener(IPAddress localAddr, int port)
        {
            return new TcpListener(localAddr, port);
        }
        public override void WriteLog(string log)
        {
            Console.WriteLine(log);
        }
    }
    class Program
    {
        const int port = 8888; // порт для прослушивания подключений
        static void Main(string[] args)
        {
            TemplateServer template = new TemplateServer();
            TcpListener server = null;
            try
            {
                //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                //server = new TcpListener(localAddr, port);
                server = template.CreateNewTcpListener(template.ParseIpAdress("127.0.0.1"), 8888);

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
                    template.WriteLog( string.Format("Отправлено сообщение: {0}", response).ToString());
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
