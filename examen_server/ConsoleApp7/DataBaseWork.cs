using System;
using System.Linq;

namespace ConsoleApp7
{
    public class DataBaseWork
    {
        private readonly Lazy<DataBaseWork> dataBaseWork = new Lazy<DataBaseWork>(() => new DataBaseWork());
        public DataBaseWork() { }
        #region Work with Users
        public void AddToUsersDb(string login, string passHash, int compliteTests)
        {
            lock (dataBaseWork)
            {
                using (ApplicationContext applicationContext = new ApplicationContext())
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
                    applicationContext.Tests.Add(new Test { TestName = testName });
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
                    applicationContext.Ansvers.Add(new Ansver { AnsverText = ansverText, IsRight = isRight, QuestionsId = questionId });
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
}
