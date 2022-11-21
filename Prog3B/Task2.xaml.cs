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
    /// Interaction logic for Task2.xaml
    /// </summary>
    public partial class Task2 : Window
    {

        char[] az;
        int count;
        private Stopwatch _stopwatch;
        private Timer _timer;
        private const string _timerStart = "00:00:00";

        Dictionary<string, string> openWith = new Dictionary<string, string>()
            {
                {"000" ,"Gerneral Knowledge"},
                {"100" ,"Philosophy & Psychology"},
                {"200" ,"Religion"},
                {"300" ,"Social Sciences"},
                {"400" ,"Languages"},
                {"500" ,"Science"},
                {"600" ,"Technology"},
                {"700" ,"Arts & Recreation"},
                {"800" ,"Literature"},
                {"900" ,"History & Geography"}
            };
        int[] hold = new int[7];
        public Task2()
        {
            InitializeComponent();
            TimerDisplay.Text = _timerStart;
            _stopwatch = new Stopwatch();
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapse;

            az = Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray();

            Test();
        }

        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => TimerDisplay.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        }

        public void load()
        {
            for (int i = 0; i < 7; i++)
            {
                AnswerA.Items.Add(az[i]);
                AnswerB.Items.Add(az[i]);
                AnswerC.Items.Add(az[i]);
                AnswerD.Items.Add(az[i]);
            }

        }
        public void Test()
        {
            _stopwatch.Start();
            _timer.Start();
            load();
            int counter = 0;
            Random r = new Random();
            while (counter != 7)
            {
                int rInt = r.Next(0, 9);
                if (!hold.Contains(rInt))
                {
                    hold[counter] = rInt;
                    counter++;
                }
            }


            Dictionary<string, string> test = new Dictionary<string, string>();

            foreach (int item in hold)
            {
                test.Add(openWith.ElementAt(item).Key, openWith.ElementAt(item).Value);
            }



            int[] hold1 = new int[4];
            for (int i = 0; i < 4; i++)
            {
                hold1[i] = 100;
            }
            int[] hold2 = new int[7];
            for (int i = 0; i < 7; i++)
            {
                hold2[i] = 100;
            }


            for (int i = 0; i < 4; i++)
            {
                int rInt = r.Next(0, 4);
                if (!hold1.Contains(rInt))
                {

                    QuestionA.Items.Add((1 + i).ToString() + "     -     " + test.ElementAt(rInt).Key);
                    hold1[i] = rInt;
                }

                else
                {
                    while (hold1.Contains(rInt))
                    {
                        rInt = r.Next(0, 4);
                    }
                    QuestionA.Items.Add((1 + i).ToString() + "     -     " + test.ElementAt(rInt).Key);
                    hold1[i] = rInt;
                }

            }


            for (int i = 0; i < 7; i++)
            {
                int rInt = r.Next(0, 7);
                if (!hold2.Contains(rInt))
                {

                    QuestionB.Items.Add(az[i] + "     -     " + test.ElementAt(rInt).Value);
                    hold2[i] = rInt;
                }

                else
                {
                    while (hold2.Contains(rInt))
                    {
                        rInt = r.Next(0, 7);
                    }
                    QuestionB.Items.Add(az[i] + "     -     " + test.ElementAt(rInt).Value);
                    hold2[i] = rInt;
                }

            }



        }
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            _timer.Stop();
            string[] stringhold = new string[4];
            int[] inthold = new int[4];
            //select answers
            inthold[0] = AnswerA.SelectedIndex;
            inthold[1] = AnswerB.SelectedIndex;
            inthold[2] = AnswerC.SelectedIndex;
            inthold[3] = AnswerD.SelectedIndex;
            if (inthold[0] == -1 || inthold[1] == -1 || inthold[2] == -1 || inthold[3] == -1)
            {
                MessageBox.Show("please answer all the questions");
            }
            else
            {
                //question
                string q1 = QuestionA.Items[0].ToString();
                stringhold[0] = q1.Substring(q1.Length - 3, 3);
                string q2 = QuestionA.Items[1].ToString();
                stringhold[1] = q2.Substring(q2.Length - 3, 3);
                string q3 = QuestionA.Items[2].ToString();
                stringhold[2] = q3.Substring(q3.Length - 3, 3);
                string q4 = QuestionA.Items[3].ToString();
                stringhold[3] = q4.Substring(q4.Length - 3, 3);
                //user answers
                count = 0;
                for (int i = 0; i < 4; i++)
                {

                    checkers(inthold[i], stringhold[i]);
                }
                if (count == 4)
                {
                    winner w = new winner();
                    w.Show();
                }
                else
                {
                    MessageBox.Show("you scored " + count.ToString() + " out of 4");
                }

            }


        }
        public void checkers(int a, string q)
        {
            if (QuestionB.Items[a].ToString().Contains(openWith[q]))
            {
                count++;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Restart();
            QuestionA.Items.Clear();
            QuestionB.Items.Clear();
            AnswerA.Items.Clear();
            AnswerB.Items.Clear();
            AnswerC.Items.Clear();
            Test();
        }
    }

}

