using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{


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

    public class User : INotifyPropertyChanged
    {
        private string login;
        private string passHash;
        private int compliteTests;

        public int Id { get; set; }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string PassHash
        {
            get { return passHash; }
            set
            {
                passHash = value;
                OnPropertyChanged("PassHash");
            }
        }
        public int CompliteTests
        {
            get { return compliteTests; }
            set
            {
                compliteTests = value;
                OnPropertyChanged("CompliteTests");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
    }

    class Program
    {
        private static string Ip = "127.0.0.1";
        private static int port = 8888;
        static void Main(string[] args)
        {
            using(ApplicationContext applicationContext = new ApplicationContext())
            {

                Console.WriteLine(applicationContext.Users.ToList().Where(x => x.Login == "asd").Last().Login);
                Console.WriteLine("Ok");
            }

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