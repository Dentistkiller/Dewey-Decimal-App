using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for winner.xaml
    /// </summary>
    public partial class winner : Window
    {
        public winner()
        {
            InitializeComponent();
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (sender.GetType() != typeof(Hyperlink))
                return;
            string link = ((Hyperlink)sender).NavigateUri.ToString();
            Process.Start(link);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "https://www.youtube.com/watch?v=q-Y0bnx6Ndw";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            this.Close();
        }
    }
}
