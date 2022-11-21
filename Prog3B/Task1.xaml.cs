using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Prog3B
{
    /// <summary>
    /// Interaction logic for Task1.xaml
    /// </summary>
    public partial class Task1 : Window
    {
        char[] az;
        char[] iz;
        string[] names = new string[10];

        private Stopwatch _stopwatch;
        private Timer _timer;
        private const string _timerStart = "00:00:00" ;

        public Task1()
        {
            InitializeComponent();
            TimerDisplay.Text = _timerStart;
            _stopwatch = new Stopwatch();
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapse;


            az = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();
            iz = Enumerable.Range('0', '9' - '0' + 1).Select(i => (Char)i).ToArray();
            gener();
            list1.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => TimerDisplay.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = list.SelectedIndex;
            if (currentIndex != -1)
            {
                if (currentIndex > 0)//up
                {
                    string holder = list.Items[currentIndex].ToString();
                    list.Items.RemoveAt(currentIndex);
                    list.Items.Insert(currentIndex - 1, holder);
                }
                else
                {
                    MessageBox.Show("out of bounds");
                }
            }

            else
            {
                MessageBox.Show("please select something");
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            int currentIndex = list.SelectedIndex;

            if (currentIndex != -1)
            {

                if (currentIndex < 9)//down
                {
                    string holder = list.Items[currentIndex].ToString();
                    list.Items.RemoveAt(currentIndex);
                    list.Items.Insert(currentIndex + 1, holder);


                }
                else
                {
                    MessageBox.Show("out of bounds");
                }
            }
            else
            {
                MessageBox.Show("please select something");
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();
            
            list.Items.Clear();
            list1.Items.Clear();
            list1.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            gener();
        }

        public void gener()
        {

            _stopwatch.Start();
            _timer.Start();
            //this generates a random sequence of numbers and letters
            String holder;
            for (int w = 0; w < 10; w++)
            {
                holder = "";

                for (int k = 0; k < 3; k++)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 9);
                    holder = holder + iz[rInt].ToString();
                }
                holder = holder + ".";
                for (int p = 0; p < 2; p++)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 9);
                    holder = holder + rInt.ToString();
                }
                for (int j = 0; j < 3; j++)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 25);
                    holder = holder + az[rInt].ToString();
                }

                // sequence gets saved in an array for comparison
                names[w] = holder;
                // sequence gets displayed in a list thats displayed
                list.Items.Add(holder);

            }
        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            _timer.Stop();
            
            checks();
        }
        public void checks()
        {

            int count = 0;
            // arrays sorts the variables in ascending order
            Array.Sort(names);
            for (int i = 0; i < 10; i++)
            {
                //if the position of a specific variable on the users list is in the same position as the sorted array then the count increases by 1
                if (names[i] == list.Items[i])
                {
                    //count variable keeps score
                    count = count + 1;
                }
            }
            if (count < 10)
            {
                // messagebox shows their score
                MessageBox.Show("you got " + count + " out of 10");
                _stopwatch.Stop();
                _timer.Stop();
                // list view showing the correct answers is displayed
                list1.Visibility = Visibility.Visible;
                label1.Visibility = Visibility.Visible;
                // populates the correct answers list using the sorted array
                foreach (var item in names)
                {

                    list1.Items.Add(item);
                }
            }
            else
            {
                winner w = new winner();
                w.Show();
            }
        }
    }
}