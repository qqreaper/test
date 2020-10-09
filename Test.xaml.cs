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

            if (answerObj.isRadio)
                AddAnsversRadio(answerObj, this);
            else
                AddAnsversCheck(answerObj, this);


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
            answerObj.answers.Add("z111xc");


            //test
            int i = 0;

            RadioButton[] radioButtons = new RadioButton[answerObj.questionQuant];


            foreach (string item in answerObj.answers)
            {
                radioButtons[i] = new RadioButton() { Name = "box" + Convert.ToString(boxName), Content = item, Margin = new Thickness(10, TopMargin, 10, 10) };
                test.AnsversGrid.Children.Add(radioButtons[i]);
                radioButtons[i].Unchecked += radioButton_Unchecked;
                radioButtons[i].Checked += radioButton_Checked;

                i++;
                TopMargin += 20;
                boxName++;
            }
        }

        private void AddAnsversCheck(AnswerClass answerObj, Test test)
        {
            int TopMargin = 0;
            int boxName = 1;
            //test
            answerObj.answers = new List<string>();
            answerObj.answers.Add("asd");
            answerObj.answers.Add("qwe");
            answerObj.answers.Add("zxc");
            answerObj.answers.Add("z111xc");


            int i = 0;


            CheckBox[] checkBoxes = new CheckBox[answerObj.questionQuant];

            foreach (string item in answerObj.answers)
            {
                checkBoxes[i] = new CheckBox() { Name = "box" + Convert.ToString(boxName), Content = item, Margin = new Thickness(10, TopMargin, 10, 10) };
                test.AnsversGrid.Children.Add(checkBoxes[i]);
                checkBoxes[i].Unchecked += checkBox_Unchecked;
                checkBoxes[i].Checked += checkBox_Checked;

                i++;
                TopMargin += 20;
                boxName++;
            }



        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            AnswerClass answerObj = new AnswerClass();

            CheckBox cb = sender as CheckBox;
            isCheckBoxRight(answerObj, cb.Name);
        }
        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            MessageBox.Show(cb.Name + " снят");
        }
        //radio
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {

            AnswerClass answerObj = new AnswerClass();

            RadioButton rb = sender as RadioButton;
            isRadioRight(answerObj, rb.Name);
        }
        private void radioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            MessageBox.Show(rb.Name + " снят");
        }

        public void isRadioRight(AnswerClass answerObj, string userAnswer)
        {
            if (userAnswer == "box" + Convert.ToString(answerObj.rightQnum))
                MessageBox.Show(Convert.ToString(userAnswer) + " is right");
            else
                MessageBox.Show(Convert.ToString(userAnswer) + " is not right");
        }

        public void isCheckBoxRight(AnswerClass answerObj, string userAnswer)
        {
            answerObj.rightAnsvers = new List<int>();
            answerObj.rightAnsvers.Add(2);
            answerObj.rightAnsvers.Add(4);
            //test

            foreach (var item in answerObj.rightAnsvers)
            {
                if (userAnswer == "box" + Convert.ToString(item))
                    MessageBox.Show(Convert.ToString(userAnswer) + " is right");
            }
        }



    }
    public class AnswerClass
    {
        public bool isRadio = false;
        public string question = "qweqwe";
        public List<string> answers;
        public int rightQnum = 2;
        public List<int> rightAnsvers;
        public int questionQuant = 4;
    }
}