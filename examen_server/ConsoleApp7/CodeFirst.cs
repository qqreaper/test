using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace ConsoleApp7
{
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
}
