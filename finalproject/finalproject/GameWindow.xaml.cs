using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace finalproject
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
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

        // Maakt nieuwe Dispatchertimer aan
        DispatcherTimer gameTimer = new();

        // Maakt nieuwe rectangles aan
        Rect playerHitBox1;
        Rect playerHitBox2;
        Rect groundHitBox1;
        Rect groundHitBox2;
        Rect obstacleHitBox;
        Rect finishHitBox;

        // String voor playername
        string player1naam = "";
        string player2naam = "";

        // Toevoegen van bools en ints
        bool jumping1, jumping2;
        int force1, force2 = 20;
        int speed1, speed2 = 5;
        bool TimerActive;

        //DateTime's voor het penalty systeem
        DateTime Time = new();
        DateTime Timeout = new();
        DateTime Time2 = new();
        DateTime Timeout2 = new();

        //Int's voor het penalty systeem
        int count1 = 0; 
        int count2 = 0;
        int count1final = 0;
        int count2final = 0;

        // Wat willekeureige variabelen
        double spriteInt;
        Random rand = new();
        Stopwatch stopwatch = new();

        // Toevoegen van de ImageBrushes
        ImageBrush playerSprite = new();
        ImageBrush backgroundSprite = new();
        ImageBrush obstacleSprite = new();
        ImageBrush buffSprite = new();
        ImageBrush finishSprite = new();

        // Int's voor de obstacles
        int[] obstacleLeftPosition = { 300, 400, 500, 600, 700 };
        int[] obstacleHeight = { 20, 30, 40, 50, 60, 70, 80};

        // Simpele null string voor timer
        string TimeSpanOutput = null;

        // String voor Username
        public string UsrName { get; set; }

        /// <summary>
        /// Zorgt voor het inladen van level 1
        /// </summary>
        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            Level1();
            myCanvas.Focus();
        }

        /// <summary>
        /// Zorgt voor het inladen van level 2
        /// </summary>
        private void Level2_Click(object sender, RoutedEventArgs e)
        {
            Level2();
            myCanvas.Focus();
        }

        private void Level3_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public GameWindow()
        {
            InitializeComponent();

            myCanvas.Focus();

            //gamTimer voor de FPS
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            //Sprites voor de gamewindow
            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.jpg"));
            finishSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/finishvlag.png"));
            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));

            //Simepele background
            background1.Fill = backgroundSprite;
            background1_2.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;
            background2_2.Fill = backgroundSprite;
        }

        /// <summary>
        /// Private void gemaakt voor het maken van de obstacles voor level 1
        /// </summary>
        private void Level1()
        {
            Rectangle obstacle1_1 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle1_2 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_1 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_2 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle3_1 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle3_2 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle4_1 = new()
            {
                Tag = "obstacle",
                Height = 70,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle4_2 = new()
            {
                Tag = "obstacle",
                Height = 70,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle5_1 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle5_2 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle6_1 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle6_2 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle7_1 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle7_2 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle8_1 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle8_2 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Canvas.SetTop(obstacle1_1, 350);
            Canvas.SetLeft(obstacle1_1, 1000);
            Canvas.SetTop(obstacle1_2, 850);
            Canvas.SetLeft(obstacle1_2, 1000);

            Canvas.SetTop(obstacle2_1, 370);
            Canvas.SetLeft(obstacle2_1, 1300);
            Canvas.SetTop(obstacle2_2, 870);
            Canvas.SetLeft(obstacle2_2, 1300);

            Canvas.SetTop(obstacle3_1, 380);
            Canvas.SetLeft(obstacle3_1, 1600);
            Canvas.SetTop(obstacle3_2, 880);
            Canvas.SetLeft(obstacle3_2, 1600);

            Canvas.SetTop(obstacle4_1, 330);
            Canvas.SetLeft(obstacle4_1, 1900);
            Canvas.SetTop(obstacle4_2, 830);
            Canvas.SetLeft(obstacle4_2, 1900);

            Canvas.SetTop(obstacle5_1, 360);
            Canvas.SetLeft(obstacle5_1, 2200);
            Canvas.SetTop(obstacle5_2, 860);
            Canvas.SetLeft(obstacle5_2, 2200);

            Canvas.SetTop(obstacle6_1, 350);
            Canvas.SetLeft(obstacle6_1, 2500);
            Canvas.SetTop(obstacle6_2, 850);
            Canvas.SetLeft(obstacle6_2, 2500);

            Canvas.SetTop(obstacle7_1, 380);
            Canvas.SetLeft(obstacle7_1, 2800);
            Canvas.SetTop(obstacle7_2, 880);
            Canvas.SetLeft(obstacle7_2, 2800);

            Canvas.SetTop(obstacle8_1, 370);
            Canvas.SetLeft(obstacle8_1, 3100);
            Canvas.SetTop(obstacle8_2, 870);
            Canvas.SetLeft(obstacle8_2, 3100);

            myCanvas.Children.Add(obstacle1_1);
            myCanvas.Children.Add(obstacle1_2);

            myCanvas.Children.Add(obstacle2_1);
            myCanvas.Children.Add(obstacle2_2);

            myCanvas.Children.Add(obstacle3_1);
            myCanvas.Children.Add(obstacle3_2);

            myCanvas.Children.Add(obstacle4_1);
            myCanvas.Children.Add(obstacle4_2);

            myCanvas.Children.Add(obstacle5_1);
            myCanvas.Children.Add(obstacle5_2);

            myCanvas.Children.Add(obstacle6_1);
            myCanvas.Children.Add(obstacle6_2);

            myCanvas.Children.Add(obstacle7_1);
            myCanvas.Children.Add(obstacle7_2);

            myCanvas.Children.Add(obstacle8_1);
            myCanvas.Children.Add(obstacle8_2);

            Rectangle finish1 = new Rectangle
            {
                Tag = "finish",
                Height = 300,
                Width = 100,
                Fill = finishSprite
            };

            Rectangle finish2 = new Rectangle
            {
                Tag = "finish",
                Height = 300,
                Width = 100,
                Fill = finishSprite
            };

            Canvas.SetTop(finish1, 100);
            Canvas.SetLeft(finish1, 3400);
            Canvas.SetTop(finish2, 600);
            Canvas.SetLeft(finish2, 3400);

            myCanvas.Children.Add(finish1);
            myCanvas.Children.Add(finish2);

            StartGame();
        }

        /// <summary>
        /// Private void gemaakt voor het maken van de obstacles voor level 2
        /// </summary>
        private void Level2()
        {
            Rectangle obstacle2_1_1 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_1_2 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_2_1 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_2_2 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_3_1 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_3_2 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_4_1 = new()
            {
                Tag = "obstacle",
                Height = 70,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_4_2 = new()
            {
                Tag = "obstacle",
                Height = 70,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_5_1 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_5_2 = new()
            {
                Tag = "obstacle",
                Height = 40,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_6_1 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_6_2 = new()
            {
                Tag = "obstacle",
                Height = 50,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_7_1 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_7_2 = new()
            {
                Tag = "obstacle",
                Height = 20,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_8_1 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_8_2 = new()
            {
                Tag = "obstacle",
                Height = 30,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_9_1 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_9_2 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_10_1 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_10_2 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_11_1 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_11_2 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_12_1 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Rectangle obstacle2_12_2 = new()
            {
                Tag = "obstacle",
                Height = 35,
                Width = 50,
                Fill = obstacleSprite
            };

            Canvas.SetTop(obstacle2_1_1, 350);
            Canvas.SetLeft(obstacle2_1_1, 1000);
            Canvas.SetTop(obstacle2_1_2, 850);
            Canvas.SetLeft(obstacle2_1_2, 1000);

            Canvas.SetTop(obstacle2_2_1, 270);
            Canvas.SetLeft(obstacle2_2_1, 1300);
            Canvas.SetTop(obstacle2_2_2, 770);
            Canvas.SetLeft(obstacle2_2_2, 1300);

            Canvas.SetTop(obstacle2_3_1, 380);
            Canvas.SetLeft(obstacle2_3_1, 1600);
            Canvas.SetTop(obstacle2_3_2, 880);
            Canvas.SetLeft(obstacle2_3_2, 1600);

            Canvas.SetTop(obstacle2_4_1, 330);
            Canvas.SetLeft(obstacle2_4_1, 1900);
            Canvas.SetTop(obstacle2_4_2, 830);
            Canvas.SetLeft(obstacle2_4_2, 1900);

            Canvas.SetTop(obstacle2_5_1, 360);
            Canvas.SetLeft(obstacle2_5_1, 2200);
            Canvas.SetTop(obstacle2_5_2, 860);
            Canvas.SetLeft(obstacle2_5_2, 2200);

            Canvas.SetTop(obstacle2_6_1, 350);
            Canvas.SetLeft(obstacle2_6_1, 2500);
            Canvas.SetTop(obstacle2_6_2, 850);
            Canvas.SetLeft(obstacle2_6_2, 2500);

            Canvas.SetTop(obstacle2_7_1, 380);
            Canvas.SetLeft(obstacle2_7_1, 2800);
            Canvas.SetTop(obstacle2_7_2, 880);
            Canvas.SetLeft(obstacle2_7_2, 2800);

            Canvas.SetTop(obstacle2_8_1, 370);
            Canvas.SetLeft(obstacle2_8_1, 3100);
            Canvas.SetTop(obstacle2_8_2, 870);
            Canvas.SetLeft(obstacle2_8_2, 3100);

            Canvas.SetTop(obstacle2_9_1, 370);
            Canvas.SetLeft(obstacle2_9_1, 3250);
            Canvas.SetTop(obstacle2_9_2, 870);
            Canvas.SetLeft(obstacle2_9_2, 3250);

            Canvas.SetTop(obstacle2_10_1, 370);
            Canvas.SetLeft(obstacle2_10_1, 3450);
            Canvas.SetTop(obstacle2_10_2, 870);
            Canvas.SetLeft(obstacle2_10_2, 3450);

            Canvas.SetTop(obstacle2_11_1, 370);
            Canvas.SetLeft(obstacle2_11_1, 3500);
            Canvas.SetTop(obstacle2_11_2, 870);
            Canvas.SetLeft(obstacle2_11_2, 3500);

            Canvas.SetTop(obstacle2_12_1, 270);
            Canvas.SetLeft(obstacle2_12_1, 3625);
            Canvas.SetTop(obstacle2_12_2, 770);
            Canvas.SetLeft(obstacle2_12_2, 3625);

            myCanvas.Children.Add(obstacle2_1_1);
            myCanvas.Children.Add(obstacle2_1_2);

            myCanvas.Children.Add(obstacle2_2_1);
            myCanvas.Children.Add(obstacle2_2_2);

            myCanvas.Children.Add(obstacle2_3_1);
            myCanvas.Children.Add(obstacle2_3_2);

            myCanvas.Children.Add(obstacle2_4_1);
            myCanvas.Children.Add(obstacle2_4_2);

            myCanvas.Children.Add(obstacle2_5_1);
            myCanvas.Children.Add(obstacle2_5_2);

            myCanvas.Children.Add(obstacle2_6_1);
            myCanvas.Children.Add(obstacle2_6_2);

            myCanvas.Children.Add(obstacle2_7_1);
            myCanvas.Children.Add(obstacle2_7_2);

            myCanvas.Children.Add(obstacle2_8_1);
            myCanvas.Children.Add(obstacle2_8_2);

            myCanvas.Children.Add(obstacle2_9_1);
            myCanvas.Children.Add(obstacle2_9_2);

            myCanvas.Children.Add(obstacle2_10_1);
            myCanvas.Children.Add(obstacle2_10_2);

            myCanvas.Children.Add(obstacle2_11_1);
            myCanvas.Children.Add(obstacle2_11_2);

            myCanvas.Children.Add(obstacle2_12_1);
            myCanvas.Children.Add(obstacle2_12_2);

            Rectangle finish2_1 = new Rectangle
            {
                Tag = "finish",
                Height = 300,
                Width = 100,
                Fill = finishSprite
            };

            Rectangle finish2_2 = new Rectangle
            {
                Tag = "finish",
                Height = 300,
                Width = 100,
                Fill = finishSprite
            };

            Canvas.SetTop(finish2_1, 100);
            Canvas.SetLeft(finish2_1, 3800);
            Canvas.SetTop(finish2_2, 600);
            Canvas.SetLeft(finish2_2, 3800);

            myCanvas.Children.Add(finish2_1);
            myCanvas.Children.Add(finish2_2);

            StartGame();
        }

        /// <summary>
        /// Private void om de game te starten
        /// </summary>
        private void StartGame()
        {
            // Bepaalt waar het canvas start
            Canvas.SetLeft(background1, 0);
            Canvas.SetLeft(background1_2, 1262);
            Canvas.SetLeft(background2, 0);
            Canvas.SetLeft(background2_2, 1262);

            //Bepaalt de positie van Player 1 
            Canvas.SetLeft(player1, 110);
            Canvas.SetTop(player1, 140);

            //Bepaalt de positie van Player 2 
            Canvas.SetLeft(player2, 110);
            Canvas.SetBottom(player2, 140);

            //Start de sprite
            RunSprite(1);

            // Bools om voor als de player jumped
            jumping1 = false;
            jumping2 = false;

            // Bool voor de timer
            TimerActive = true;

            // Start de Timer en Gametimer
            gameTimer.Start();
            stopwatch.Start();
        }

        /// <summary>
        /// Exit Button
        /// </summary>
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Set de playernames wanneer de window geload wordt
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            player1naam = UsrName;
            player2naam = UsrName + "-guest";
        }
        /// <summary>
        /// Restart de game
        /// </summary>
        public void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            GameWindow newWindow = new GameWindow();
            newWindow.Show();
            this.Close(); 
        }

        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetTop(player1, Canvas.GetTop(player1) + speed1);
            Canvas.SetTop(player2, Canvas.GetTop(player2) + speed2);

            #region "verplaatst achtergrond en obstacle elke tick naar links"
            Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 3);
            Canvas.SetLeft(background1_2, Canvas.GetLeft(background1_2) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);
            Canvas.SetLeft(background2_2, Canvas.GetLeft(background2_2) - 3);

            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "obstacle" || (string)x.Tag == "finish")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 6);
                }
            }
            #endregion

            // Player Hitboxes
            playerHitBox1 = new Rect(Canvas.GetLeft(player1), Canvas.GetTop(player1), player1.Width, player1.Height);
            playerHitBox2 = new Rect(Canvas.GetLeft(player2), Canvas.GetTop(player2), player2.Width, player2.Height);

            // Ground Hitboxes
            groundHitBox1 = new Rect(Canvas.GetLeft(ground1), Canvas.GetTop(ground1), ground1.Width, ground1.Height);
            groundHitBox2 = new Rect(Canvas.GetLeft(ground2), Canvas.GetTop(ground2), ground2.Width, ground2.Height);

            // Timepsan voor de timer
            TimeSpan ts = stopwatch.Elapsed;
            TimeSpanOutput = ts.ToString("ss\\.fff");
            
            // Berekent de verstreken tijd en set de content van de timer
            int verstrekentijd = Convert.ToInt32(stopwatch.Elapsed.TotalSeconds);
            Timer.Content = TimeSpanOutput;

            // Set de datetimes naar now voor de penaltys
            DateTime Time = DateTime.Now;
            DateTime Time2 = DateTime.Now;

            #region "aanmaken hitbox voor obstacle en checken of player obstacle raakt"
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "obstacle")
                {
                    obstacleHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (playerHitBox1.IntersectsWith(obstacleHitBox))
                    {
                        if (Time > Timeout) // Penalty System Player 1
                        {
                            count1 += 3;                          
                            strafseconden1.Content = "Penalty: " + count1;
                            Timeout = Time.AddSeconds(1);
                        }
                        else
                        {
                            strafseconden1.Content = "Penalty: " + count1;                            
                        }
                    }
                    if (playerHitBox2.IntersectsWith(obstacleHitBox)) // Penalty system Player 2
                    {
                        if (Time2 > Timeout2)
                        {
                            count2 += 3;
                            strafseconden2.Content = "Penalty: " + count2;
                            Timeout2 = Time2.AddSeconds(1);
                        }
                        else
                        {
                            strafseconden2.Content = "Penalty: " + count2;
                        }
                    }
                }
            }
            #endregion        

            #region "checken of player finish aanraakt"
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "finish")
                {
                    finishHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                }
                if (playerHitBox1.IntersectsWith(finishHitBox))
                {
                    stopwatch.Stop();
                    gameTimer.Stop();
                    TimerActive = false;
                    MessageBox.Show("Congratulations you win!", "You win!");
                    int score1 = verstrekentijd + count1;
                    int score2 = verstrekentijd + count2;
                    AddHighscoreToDatabase(player1naam, player2naam, score1, score2);
                    GameWindow newWindow = new GameWindow();
                    newWindow.Show();
                    this.Close();
                }
            }
            #endregion

            #region  "checkt of player grond aanraakt"
            if (playerHitBox1.IntersectsWith(groundHitBox1))
            {
                speed1 = 0;
                Canvas.SetTop(player1, Canvas.GetTop(ground1) - player1.Height);
                jumping1 = false;
                spriteInt += .125;
                if (spriteInt > 8)
                {
                    spriteInt = 1;
                };
                RunSprite(spriteInt);
            };
            if (playerHitBox2.IntersectsWith(groundHitBox2))
            {
                speed2 = 0;
                Canvas.SetTop(player2, Canvas.GetTop(ground2) - player2.Height);
                jumping2 = false;
                spriteInt += .125;
                if (spriteInt > 8)
                {
                    spriteInt = 1;
                };
                RunSprite(spriteInt);
            };
            #endregion

            #region "past speed en force en jumping aan"
            if (jumping1)
            {
                speed1 = -15;
                force1--;
            }
            else
            {
                speed1 = 12;
            }
            if (force1 < 0)
            {
                jumping1 = false;
            }

            if (jumping2)
            {
                speed2 = -15;
                force2--;
            }
            else
            {
                speed2 = 12;
            }
            if (force2 < 0)
            {
                jumping2 = false;
            }
            #endregion

            #region "achtergrond bewegen"
            if (Canvas.GetLeft(background1) < -1262)
            {
                Canvas.SetLeft(background1, Canvas.GetLeft(background1_2) + background1_2.Width);
            }

            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background2_2) + background2_2.Width);
            }
            #endregion
        }

        /// <summary>
        /// Runsprites voor de players
        /// </summary>
        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/2.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/4.png"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/5.png"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/6.png"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/7.png"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/8.png"));
                    break;
            }
            // Voegt de sprite toe aan de players
            player1.Fill = playerSprite;
            player2.Fill = playerSprite;
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            // Jump voor player 1
            if (e.Key == Key.W && !jumping1 && Canvas.GetTop(player1) > 260)
            {
                jumping1 = true;
                force1 = 15;
                speed1 = -12;
            }
            // Jump voor player 2
            if (e.Key == Key.Up && !jumping2 && Canvas.GetTop(player2) > 760)
            {
                jumping2 = true;
                force2 = 15;
                speed2 = -12;
            }
        }
        /// <summary>
        /// Voegt aan het einde van de game de Highscores toe aan de Leaderboard/Database
        /// </summary>
        /// <param name="player1naam">Playername van de bovenste speler</param>
        /// <param name="player2naam">Playername van de onderste speler</param>
        /// <param name="highscore1">Highscore van de bovenste speler</param>
        /// <param name="highscore2">Highscore van de onderste speler</param>
        public void AddHighscoreToDatabase(string player1naam, string player2naam, int highscore1, int highscore2)
        {
            string query1 = "INSERT INTO [Highscores] ([Player],[Score],[Date]) VALUES ('" + player1naam + "','" + highscore1 + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "')"; // Insert de highscore van Player 1
            string query2 = "INSERT INTO [Highscores] ([Player],[Score],[Date]) VALUES ('" + player2naam + "','" + highscore2 + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "')"; // Insert de highscore van Player 2
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = query1;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                command.CommandText = query2;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
            }
        }
    }
}
