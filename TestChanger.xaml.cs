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
using System.Net.Sockets;

namespace exam_test
{
    /// <summary>
    /// Interaction logic for TestChanger.xaml
    /// </summary>
    public partial class TestChanger : Window
    {
        public TestChanger()
        {

            InCharger inCharger = new InCharger();


            InitializeComponent();


        }

        public class InCharger
        {

        }

    }
}
