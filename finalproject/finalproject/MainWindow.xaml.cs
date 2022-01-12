using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace finalproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UsrName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _ = new LoginScreen
            {
                Visibility = Visibility.Visible
            };
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            GameWindow window = new GameWindow();
            window.UsrName = UsrName;
            window.ShowDialog();
        }

        private void Leaderboard_Click(object sender, RoutedEventArgs e)
        {
            _ = new test.Leaderboard
            {
                Visibility = Visibility.Visible
            };
        }

        private void Credits_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textlabel1.Content = ("Welcome, " + UsrName);
        }
    }
}
