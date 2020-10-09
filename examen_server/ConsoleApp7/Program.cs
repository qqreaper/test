using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

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

        [Key]
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
    public class Test : INotifyPropertyChanged
    {
        private string testName;

        [Key]
        public int Id { get; set; }
        public string TestName
        {
            get { return testName; }
            set
            {
                testName = value;
                OnPropertyChanged("TestName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class Question : INotifyPropertyChanged
    {
        private string questionText;
        private int? testsId;

        [Key]
        public int Id { get; set; }
        public string QuestionText
        {
            get { return questionText; }
            set
            {
                questionText = value;
                OnPropertyChanged("QuestionText");
            }
        }
        public int? TestsId
        {
            get { return testsId; }
            set
            {
                testsId = value;
                OnPropertyChanged("TestsId");
            }
        }
        [ForeignKey("TestsId")]
        public Test test { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
    public class Ansver : INotifyPropertyChanged
    {
        private string ansverText;
        private int isRight;
        private int? questionsId;

        [Key]
        public int Id { get; set; }
        public string AnsverText
        {
            get { return ansverText; }
            set
            {
                ansverText = value;
                OnPropertyChanged("AnsverText");
            }
        }
        public int IsRight
        {
            get { return isRight; }
            set
            {
                isRight = value;
                OnPropertyChanged("IsRight");
            }
        }
        public int? QuestionsId
        {
            get { return questionsId; }
            set
            {
                questionsId = value;
                OnPropertyChanged("QuestionsId");
            }
        }
        [ForeignKey("QuestionsId")]
        Question question { get; set; }

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
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Ansver> Ansvers { get; set; }
    }
    public class DataBaseWork
    {
        private readonly Lazy<DataBaseWork> dataBaseWork = new Lazy<DataBaseWork>(() => new DataBaseWork());
        public DataBaseWork() { }
        #region Work with Users
        public void AddToUsersDb(string login, string passHash, int compliteTests)
        {
            lock (dataBaseWork)
            {
                using(ApplicationContext applicationContext = new ApplicationContext())
                {
                    applicationContext.Users.Add(new User { Login = login, PassHash = passHash, CompliteTests = compliteTests });
                    applicationContext.SaveChangesAsync();
                    Console.WriteLine($"User {login} {passHash} added");
                }
            }
        }
        public void RemoveFromUsersDb(string login, string passHash)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    if (applicationContext.Users.ToList().Where(x => x.Login == login && x.PassHash == passHash).Count() > 0)
                    {
                        User userForRemove = applicationContext.Users.ToList().Where(x => x.Login == login && x.PassHash == passHash).Last();
                        applicationContext.Users.Remove(userForRemove);
                        applicationContext.SaveChangesAsync();
                        Console.WriteLine($"User {login} {passHash} deleted");
                    }
                    else
                    {
                        Console.WriteLine("404(User not found)");
                    }
                }
            }
        }
        #endregion
        #region Work with Test
        public void AddToTestsDb(string testName)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    applicationContext.Tests.Add(new Test { TestName = testName});
                    applicationContext.SaveChangesAsync();
                    Console.WriteLine($"Test {testName} added");
                }
            }
        }
        public void RemoveFromTestsDb(string testName)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    if (applicationContext.Tests.ToList().Where(x => x.TestName == testName).Count() > 0)
                    {
                        Test testForRemove = applicationContext.Tests.ToList().Where(x => x.TestName == testName).Last();
                        applicationContext.Tests.Remove(testForRemove);
                        applicationContext.SaveChangesAsync();
                        Console.WriteLine($"Test {testName} deleted");
                    }
                    else
                    {
                        Console.WriteLine("404(Test not found)");
                    }
                }
            }
        }
        #endregion
        #region Work with Questions
        public void AddToQuestionsDb(string questionText, int testsId)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    applicationContext.Questions.Add(new Question { QuestionText = questionText, TestsId = testsId });
                    applicationContext.SaveChangesAsync();
                    Console.WriteLine($"Question {questionText} {testsId} added");
                }
            }
        }
        public void RemoveFromQuestionsDb(string questionText, int testsId)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    if (applicationContext.Questions.ToList().Where(x => x.QuestionText == questionText && x.TestsId == testsId).Count() > 0)
                    {
                        Question questionForRemove = applicationContext.Questions.ToList().Where(x => x.QuestionText == questionText && x.TestsId == testsId).Last();
                        applicationContext.Questions.Remove(questionForRemove);
                        applicationContext.SaveChangesAsync();
                        Console.WriteLine($"Question {questionText} deleted");
                    }
                    else
                    {
                        Console.WriteLine("404(Question not found)");
                    }
                }
            }
        }
        #endregion
        #region Work with Answer
        public void AddToAnswersDb(string ansverText, int isRight, int questionId)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    applicationContext.Ansvers.Add(new Ansver { AnsverText = ansverText, IsRight = isRight, QuestionsId = questionId});
                    applicationContext.SaveChangesAsync();
                    Console.WriteLine($"Answer {ansverText} {isRight} added");
                }
            }
        }
        public void RemoveFromAnswersDb(string ansverText, int isRight, int questionId)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
                {
                    if (applicationContext.Ansvers.ToList().Where(x => x.AnsverText == ansverText && x.IsRight == isRight && x.QuestionsId == questionId).Count() > 0)
                    {
                        Ansver answerForRemove = applicationContext.Ansvers.ToList().Where(x => x.AnsverText == ansverText && x.IsRight == isRight && x.QuestionsId == questionId).Last();
                        applicationContext.Ansvers.Remove(answerForRemove);
                        applicationContext.SaveChangesAsync();
                        Console.WriteLine($"Ansver {ansverText} deleted");
                    }
                    else
                    {
                        Console.WriteLine("404(Ansver not found)");
                    }
                }
            }
        }
        #endregion
    }
    class Program
    {
        private static string Ip = "127.0.0.1";
        private static int port = 8888;
        static void Main(string[] args)
        {
            using(ApplicationContext applicationContext = new ApplicationContext())
            {
                
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