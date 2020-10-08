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

            TestTestClass test = new TestTestClass();

            questionField.Content = test.question;

            AddAnsvers(test.answers, this);
        }
        private void AddAnsvers(List<string> ans, Test test)
        {
            int TopMargin = 0;
            ans = new List<string>();
            ans.Add("asd");
            ans.Add("qwe");
            ans.Add("zxc");
            foreach (string item in ans)
            {

                test.AnsversGrid.Children.Add(new RadioButton() { Content = item, Margin = new Thickness(10, TopMargin, 10, 10) });
                TopMargin += 20;
            }
        }
        public class TestTestClass
        {
            public string question = "qweqwe";
            public List<string> answers;
            public int rightQnum = 2;
            bool isRadio;
        }
    }
}
