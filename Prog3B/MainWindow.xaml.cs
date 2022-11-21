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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prog3B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnReplaceBook_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Task1 task = new Task1();
            task.Show();
        }

        private void btnIDArea_Click(object sender, RoutedEventArgs e)
        {
            
            this.Hide();
            Task2 task = new Task2();
            task.Show();
        }

        private void btnFCN_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("COMING SOON!!!");
            this.Hide();
            callNum task = new callNum();
            task.Show();
        }
    }
}
