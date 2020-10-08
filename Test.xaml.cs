using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace exam_test
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();

            AnswerClass answerObj = new AnswerClass();

            questionField.Content = answerObj.question;

            AddAnsvers(answerObj, this);
        }
        private void AddAnsvers(AnswerClass answerObj, Test test)
        {
            int TopMargin = 0;
            //test
            answerObj.answers = new List<string>();
            answerObj.answers.Add("asd");
            answerObj.answers.Add("qwe");
            answerObj.answers.Add("zxc");
            //test

            foreach (string item in answerObj.answers)
            {
                if(answerObj.isRadio)
                test.AnsversGrid.Children.Add(new RadioButton() { Content = item, Margin = new Thickness(10, TopMargin, 10, 10) });
                else
                    test.AnsversGrid.Children.Add(new CheckBox() { Content = item, Margin = new Thickness(10, TopMargin, 10, 10) });


                TopMargin += 20;
            }
        }
        public class AnswerClass
        {
            public bool isRadio = true;
            public string question = "qweqwe";
            public List<string> answers;
            public int rightQnum = 2;
            public List<int> rightAnsvers;
           
        }
    }
}
