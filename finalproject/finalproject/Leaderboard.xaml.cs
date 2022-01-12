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
using System.Windows.Shapes;

namespace test
{
    /// <summary>
    /// Interaction logic for Leaderboard.xaml
    /// </summary>
    public partial class Leaderboard : Window
    {
        //Marten connection
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Marten\Desktop\ProjectArcadeb08\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security=True";

        //Leon Connection
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eigenaar\Documents\GitHub\ProjectArcadeb08\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security = True";

        //Marc Connection
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marcd\OneDrive\Documenten\DE GAME\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security = True";

        //Marten laptop
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\MGdeJ\\Desktop\\ProjectArcadeb08\\finalproject\\finalproject\\Data\\GameDatabase.mdf;Integrated Security=True";

        //Leon Laptop
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Leon\Documents\ProjectArcadeb08\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security=True";

        public Leaderboard()
        {
            InitializeComponent();

            //roept GetHighScores methode aan
            GetHighScores();
        }

        /// <summary>
        /// maakt het leaderbord schoon en voegt alles weer toe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            //Maakt het user panel schoon
            UserPanel.Children.Clear();

            GetHighScores();
        }

        /// <summary>
        /// Haalt data uit de database om te tonen op het leaderbord
        /// </summary>
        private void GetHighScores()
        {
            string query = "SELECT * FROM dbo.Highscores ORDER BY score ASC;";

            SqlConnection connection = new SqlConnection(connectionString);

            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                //reader zorgt ervoor dat je dingen uit de database kan halen en dit wordt in een array gezet
                while (reader.Read())
                {
                    Label label1 = new Label();
                    Label label2 = new Label();
                    Label label3 = new Label();

                    // voor elk label staat reader[index] gelijk, met opmaak.
                    label1.Content = reader[1];
                    label1.Margin = new Thickness(0, 0, 0, 0);
                    label1.HorizontalAlignment = HorizontalAlignment.Left;
                    label1.FontSize = 36;
                    UserPanel.Children.Add(label1);

                    label2.Content = reader[3];
                    label2.Margin = new Thickness(0, -60, 0, 0);
                    label2.HorizontalAlignment = HorizontalAlignment.Right;               
                    label2.FontSize = 36;
                    UserPanel.Children.Add(label2);

                    label3.Content = reader[2] + "s";
                    label3.Margin = new Thickness(0, -60, 0, 0);
                    label3.HorizontalAlignment = HorizontalAlignment.Center;                   
                    label3.FontSize = 36;
                    UserPanel.Children.Add(label3);
                }
                // sluit reader
                reader.Close();
            }
        }

        /// <summary>
        /// maakt het leaderbord schoon en voegt dat toe wat uit de methode searchHighScore komt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchUsr_Click(object sender, RoutedEventArgs e)
        {
            UserPanel.Children.Clear();

            SearchHighScore();
        }

        /// <summary>
        /// Methode die de naam uit het invoerveld haalt en alleen deze informatie uit de database haalt.
        /// </summary>
        private void SearchHighScore()
        {
            {
                string query = "SELECT * FROM dbo.Highscores WHERE player = @username OR player = @username + '-guest' ORDER BY score ASC;";

                SqlConnection connection = new SqlConnection(connectionString);

                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    // het omzetten van USR.Text naar een parameter die SQL kan herkennen
                    command.Parameters.Add("@username", SqlDbType.NVarChar);
                    command.Parameters["@username"].Value = USR.Text;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Label label1 = new Label();
                        Label label2 = new Label();
                        Label label3 = new Label();


                        label1.Content = reader[1];
                        label1.HorizontalAlignment = HorizontalAlignment.Left;
                        label1.FontSize = 36;
                        UserPanel.Children.Add(label1);

                        label2.Content = reader[3];
                        label2.HorizontalAlignment = HorizontalAlignment.Right;
                        label2.Margin = new Thickness(0, -60, 0, 0);
                        label2.FontSize = 36;
                        UserPanel.Children.Add(label2);

                        label3.Content = reader[2] + "s";
                        label3.HorizontalAlignment = HorizontalAlignment.Center;
                        label3.Margin = new Thickness(0, -60, 0, 0);
                        label3.FontSize = 36;
                        UserPanel.Children.Add(label3);
                    }

                    reader.Close();
                }
            }
        }

        private void ExitL(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
