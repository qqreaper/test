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

        TestChanger db;
       
        public TestChanger()
        {
            InitializeComponent();

            InCharger inCharger = new InCharger();

            db = new TestChanger();
            db.SpisokTestov.Load();  // в душе не гребу как там называется таблица тестов в БД
            Load();

        }

        private void Load()
        {
            myDataGrid.ItemsSource = db.SpisokTestov.Local.ToBindingList();
            datagrid = myDataGrid;

        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

            db.SaveChanges();
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {

            if (myDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < myDataGrid.SelectedItems.Count; i++)
                {
                    InCharger inCharger = myDataGrid.SelectedItems[i] as InCharger;
                    if (inCharger != null)
                    {
                        db.SpisokTestov.Remove(inCharger);
                    }
                }
            }
            db.SaveChanges();
        }

        public class InCharger
        {

            public int Id { get; set; }
            public int Question { get; set; }

            public int Ansver { get; set; }


        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            db.Dispose();
            this.Close();
        }
    }
}
