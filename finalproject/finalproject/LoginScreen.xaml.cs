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

namespace finalproject
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        //Marten connection
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Marten\\Desktop\\ProjectArcadeb08\\finalproject\\finalproject\\Data\\GameDatabase.mdf;Integrated Security=True";

        //Leon Connection
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eigenaar\Documents\GitHub\ProjectArcadeb08\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security = True";

        //Marc Connection
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\marcd\OneDrive\Documenten\DE GAME\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security = True";

        //Marten laptop
        //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\MGdeJ\\Desktop\\ProjectArcadeb08\\finalproject\\finalproject\\Data\\GameDatabase.mdf;Integrated Security=True";

        //Leon Laptop
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Leon\Documents\ProjectArcadeb08\finalproject\finalproject\Data\GameDatabase.mdf;Integrated Security=True";

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password != string.Empty || txtUsername.Text != string.Empty)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from UserData where Username='" + txtUsername.Text + "' and Password='" + txtPassword.Password + "'", con); // Checkt als de Username en Password Matchen in de database.
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) 
                {
                    dr.Close();
                    con.Close();
                    Hide();

                    MainWindow window = new MainWindow();
                    window.UsrName = txtUsername.Text;
                    window.ShowDialog();
                }
                else // Voor het geval dat de username of password incorrect is.
                {
                    dr.Close();
                    MessageBox.Show("No Account avilable with this username and password.", "Error");
                }
            }
            else // Voor het geval dat een veld niet ingevuld is.
            {
                MessageBox.Show("Please enter in all fields.", "Error");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {            
            if (txtPassword.Password != string.Empty || txtUsername.Text != string.Empty)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Userdata where Username=@Username", con);
                cmd.Parameters.AddWithValue("@Username", this.txtUsername.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows) // Checkt als de username al bestaat in de Database.
                {
                    dr.Close();
                    MessageBox.Show(string.Format("Username {0} already exist.", this.txtUsername.Text));
                }
                else // Als de username niet bestaat dan wordt deze aangemaakt en opgeslagen in de Database.
                {
                    dr.Close();
                    cmd = new SqlCommand("insert into Userdata values(@Username,@Password)", con);
                    cmd.Parameters.AddWithValue("Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("Password", txtPassword.Password);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Your Account is created, Please login now.", "Done");
                    con.Close();
                }
            }
            else // Voor het geval dat een veld niet ingevuld is.
            {
                MessageBox.Show("Please enter in all fields.", "Error");
            }
        }
    }
}

