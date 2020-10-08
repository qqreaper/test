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
        public static DataGrid datagrid;
        
       
        public TestChanger()
        {
            InitializeComponent();

            InCharger inCharger = new InCharger();
           
            Load();

        }

        private void Load()
        {
            myDataGrid.ItemsSource = InCharger.Test.ToList();
            datagrid = myDataGrid;

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
          
           
            
            myDataGrid.ItemsSource = InCharger.Test.ToList();
        }

        public class InCharger
        {

            int Test1 = 8;

            public void Test()
            {

            }


        }



    }
}
