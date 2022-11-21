using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Prog3B
{
    /// <summary>
    /// Interaction logic for callNum.xaml
    /// </summary>
    public partial class callNum : Window
    {
        private Stopwatch _stopwatch;
        private Timer _timer;
        private const string _timerStart = "00:00:00";
        Tree<CallClass> tree;
        String textFile = "Call.txt";
        int LevelOneIndex = 0;
        int LevelTwoIndex = 0;
        int questionHolder1 = 0;
        int questionHolder2 = 0;
        int questionHolder3 = 0;
        int count = 0;
        String answerNumber = "";
        public callNum()
        {
            InitializeComponent();
            TimerDisplay.Text = _timerStart;
            _stopwatch = new Stopwatch();
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapse;

            loader();
            overloader();
        }
        private void OnTimerElapse(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => TimerDisplay.Text = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));
        }
        public void overloader()
        {

            int[] randomNum = new int[4];
            //int[] randomNumHolder = new int[3];
            //String[] randomString = new String[3];


            Array.Clear(randomNum, 0, randomNum.Length);

            if (count == 0)
            {
                int counter = 0;
                Boolean loopCheck = false;
                while (loopCheck == false)
                {
                    Random j = new Random();
                    questionHolder1 = j.Next(0, 9);
                    questionHolder2 = j.Next(0, 9);
                    questionHolder3 = j.Next(0, 9);
                    try
                    {
                        string holder = tree.Root.Children[questionHolder1].Children[questionHolder2].Children[questionHolder3].Data.CallNum;
                        loopCheck = true;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


                while (counter != 3)
                {

                    Random r = new Random();
                    int holder = r.Next(0, 9);
                    if (!randomNum.Contains(holder) && holder != questionHolder1)
                    {
                        randomNum[counter] = holder;
                        counter++;


                    }
                }
                question.Content = tree.Root.Children[questionHolder1].Children[questionHolder2].Children[questionHolder3].Data.CallName;

                randomNum[3] = questionHolder1;
                Array.Sort(randomNum);
                Array.Reverse(randomNum);
                answer1.Content = tree.Root.Children[randomNum[0]].Data.CallNum + "-" + tree.Root.Children[randomNum[0]].Data.CallName;
                answer2.Content = tree.Root.Children[randomNum[1]].Data.CallNum + "-" + tree.Root.Children[randomNum[1]].Data.CallName;
                answer3.Content = tree.Root.Children[randomNum[2]].Data.CallNum + "-" + tree.Root.Children[randomNum[2]].Data.CallName;
                answer4.Content = tree.Root.Children[randomNum[3]].Data.CallNum + "-" + tree.Root.Children[randomNum[3]].Data.CallName;
            }
            else
            {

                int counter1 = 0;
                while (counter1 != 3)
                {

                    Random B = new Random();
                    int holder = B.Next(0, 9);
                    if (!randomNum.Contains(holder) && holder != questionHolder2)
                    {

                        try
                        {
                            string tester1 = tree.Root.Children[questionHolder1].Children[holder].Data.CallNum;
                            randomNum[counter1] = holder;
                            counter1++;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                question.Content = tree.Root.Children[questionHolder1].Children[questionHolder2].Children[questionHolder3].Data.CallName;

                randomNum[3] = questionHolder2;
                Array.Sort(randomNum);
                Array.Reverse(randomNum);

                answer1.Content = tree.Root.Children[questionHolder1].Children[randomNum[0]].Data.CallNum + "-" + tree.Root.Children[questionHolder1].Children[randomNum[0]].Data.CallName;
                answer2.Content = tree.Root.Children[questionHolder1].Children[randomNum[1]].Data.CallNum + "-" + tree.Root.Children[questionHolder1].Children[randomNum[1]].Data.CallName;
                answer3.Content = tree.Root.Children[questionHolder1].Children[randomNum[2]].Data.CallNum + "-" + tree.Root.Children[questionHolder1].Children[randomNum[2]].Data.CallName;
                answer4.Content = tree.Root.Children[questionHolder1].Children[randomNum[3]].Data.CallNum + "-" + tree.Root.Children[questionHolder1].Children[randomNum[3]].Data.CallName;

            }


        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        public void loader()
        {
            tree = new Tree<CallClass>();
            CallClass holder = new CallClass();
            tree.Root = new TreeNode<CallClass>() { Data = holder };
            _stopwatch.Start();
            _timer.Start();

            string[] lines = File.ReadAllLines(textFile);

            foreach (String item in lines)
            {
                CallClass holder1 = new CallClass();
                //000 Generalities
                string number = item.Substring(0, 3);//get first 3 
                string word = item.Substring(4);//get from 4 to last               
                holder1.CallNum = number;
                holder1.CallName = word;
                if (number.Substring(1).Equals("00"))//get last 2
                {
                    LevelOne(holder1);
                    LevelOneIndex++;
                    LevelTwo(holder1);
                    LevelTwoIndex = 0;
                }
                else if (number.Substring(2).Equals("0"))//get last one
                {
                    LevelTwoIndex++;
                    LevelTwo(holder1);

                }
                else
                {
                    LevelThree(holder1);

                }
            }
        }
        private void LevelOne(CallClass holder)
        {
            if (tree.Root.Children == null)
            {
                tree.Root.Children = new List<TreeNode<CallClass>>()
                {
                    new TreeNode<CallClass>()
                    {
                        Data = holder
                    }
                };
            }
            else
            {
                tree.Root.Children.Add(new TreeNode<CallClass>()
                {
                    Data = holder
                }
                );
            }
        }
        private void LevelTwo(CallClass holder)
        {
            if (tree.Root.Children[(LevelOneIndex - 1)].Children == null)
            {
                tree.Root.Children[(LevelOneIndex - 1)].Children = new List<TreeNode<CallClass>>()
                {
                    new TreeNode<CallClass>()
                    {
                        Data = holder,
                        Parent = tree.Root.Children[(LevelOneIndex - 1)].Data
                    }
                };
            }
            else
            {
                tree.Root.Children[(LevelOneIndex - 1)].Children.Add(new TreeNode<CallClass>()
                {
                    Data = holder,
                    Parent = tree.Root.Children[(LevelOneIndex - 1)].Data
                }
                );
            }
        }
        private void LevelThree(CallClass holder)
        {
            if (tree.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children == null)
            {
                tree.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children = new List<TreeNode<CallClass>>()
                {
                    new TreeNode<CallClass>()
                    {
                        Data = holder,
                        Parent = tree.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Data
                    }
                };
            }
            else
            {
                tree.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children.Add(new TreeNode<CallClass>()
                {
                    Data = holder,
                    Parent = tree.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Data
                }
                );
            }
        }

        private void answer1_Checked(object sender, RoutedEventArgs e)
        {
            answerNumber = (answer1.Content.ToString()).Substring(0, 3); //get first 3 
            answer2.IsChecked = false;
            answer3.IsChecked = false;
            answer4.IsChecked = false;

        }

        private void answer2_Checked(object sender, RoutedEventArgs e)
        {
            answerNumber = (answer2.Content.ToString()).Substring(0, 3); //get first 3 
            answer1.IsChecked = false;
            answer3.IsChecked = false;
            answer4.IsChecked = false;
        }

        private void answer3_Checked(object sender, RoutedEventArgs e)
        {
            answerNumber = (answer3.Content.ToString()).Substring(0, 3); //get first 3 
            answer1.IsChecked = false;
            answer2.IsChecked = false;
            answer4.IsChecked = false;
        }

        private void answer4_Checked(object sender, RoutedEventArgs e)
        {
            answerNumber = (answer4.Content.ToString()).Substring(0, 3); //get first 3 
            answer1.IsChecked = false;
            answer2.IsChecked = false;
            answer3.IsChecked = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (answerNumber == "")
            {
                MessageBox.Show("select an answer.");
            }
            else
            {
                if (count == 0)
                {
                    if (answerNumber.Equals(tree.Root.Children[questionHolder1].Data.CallNum))
                    {

                        answerNumber = "";
                        count++;
                        MessageBox.Show("congrats you move on to the next level");
                    }
                    else
                    {
                        MessageBox.Show("sorry you got the answer wrong plz try again");
                        _stopwatch.Restart();
                    }
                    overloader();
                }
                else
                {

                    if (answerNumber.Equals(tree.Root.Children[questionHolder1].Children[questionHolder2].Data.CallNum))
                    {
                        answerNumber = "";

                        winner w = new winner();
                        w.Show();
                        _stopwatch.Stop();
                        _timer.Stop();
                    }
                    else
                    {
                        MessageBox.Show("sorry you got the answer wrong plz try again");
                        _stopwatch.Restart();

                    }
                    count = 0;

                    answerNumber = "";
                    overloader();
                }

                answer1.IsChecked = false;
                answer2.IsChecked = false;
                answer3.IsChecked = false;
                answer4.IsChecked = false;
            }
        }
    }
}
