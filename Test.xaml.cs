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

            AddAnsversRadio(answerObj, this);


        }
        private void AddAnsversRadio(AnswerClass answerObj, Test test)
        {
            int TopMargin = 0;
            int boxName = 1;
            //test
            answerObj.answers = new List<string>();
            answerObj.answers.Add("asd");
            answerObj.answers.Add("qwe");
            answerObj.answers.Add("zxc");


            //test
            int i = 0;

            if (answerObj.isRadio)
                RadioButton[] radioButtons = new RadioButton[answerObj.questionQuant];
            else
                CheckBox[] chekBoxes = new CheckBox[answerObj.questionQuant];

            foreach (string item in answerObj.answers)
            {

                if (answerObj.isRadio)
                {
                    radioButtons[i] = new RadioButton() { Name = "box" + Convert.ToString(boxName), Content = item, Margin = new Thickness(10, TopMargin, 10, 10) };
                    test.AnsversGrid.Children.Add(radioButtons[i]);
                }
                else
                {
                    chekBoxes[i] = new RadioButton() { Name = "box" + Convert.ToString(boxName), Content = item, Margin = new Thickness(10, TopMargin, 10, 10) };
                    test.AnsversGrid.Children.Add(chekBoxes[i]);
                }


                //foreach (var item in test.AnsversGrid.Children.)
                //{
                //    if (item.Name == "box" + Convert.ToString(1)) ;

                i++;
                TopMargin += 20;
                boxName++;
            }


        }
    }
    public class AnswerClass
    {
        public bool isRadio = true;
        public string question = "qweqwe";
        public List<string> answers;
        public int rightQnum = 2;
        public List<int> rightAnsvers;
        public int questionQuant = 3;

    }
}
