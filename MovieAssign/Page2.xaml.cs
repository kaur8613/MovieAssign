using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Data.SqlClient;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieAssign
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page2 : Page
    {
        String MasterGuess;
        int decided;
        int clickCount = 0;
        string usern;
        SqlConnection conn;
        string conString;
        private SqlCommand cmd;

        public Page2()
        {
            this.InitializeComponent();
        }
        private void Page_load(object sender, RoutedEventArgs e)
        {
             conn = new SqlConnection();

             conString = "server=LAPTOP-2ILO70CM\\SQLEXPRESS;Database=UserLog;User=Gurjot;Password=123";

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string[] MovieNames = { "ToyStory", "Cars","Dora"};           
                User user1  = (User)e.Parameter;
            Random rand = new Random();
         int guess = rand.Next(MovieNames.Length);
            MasterGuess = MovieNames[guess];
             decided = (int)user1.Decide;
                usern = user1.Username.ToString();
                if (decided == 1)
                {
                    GuessText.Text = "Welcome " + usern + " Guess the Movie";
               
                Logout.Content = "Log Out";
                }
                else
                {
                Logout.Content = "Go Back";
                GuessText.Text = "Guess the Movie";
                }
        
            base.OnNavigatedTo(e);
        }
       
        private void Check_Click(object sender, RoutedEventArgs e)
        {
                   string byPlayer = Convert.ToString(GuessPlayer.Text);
            clickCount++;//to give only three chences to the user
                if(clickCount <= 3)
            {
                if (MasterGuess.Equals(byPlayer))
                {
                    result.Text = "Yay !! You are correct";
                    try //incrementing the right guess by 1
                    {
                        conn.ConnectionString = conString;
                        cmd = conn.CreateCommand();
                        string query = "UPDATE UserLog SET RightGuess = RightGuess + 1 WHERE Username='" + usern + "'";

                        cmd.CommandText = query;
                        conn.Open();
                      cmd.ExecuteScalar();

                    }
                    catch (Exception ex)

                    {
                        string message = ex.Message.ToString();

                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Close();
                    }
                }
                else
                {
                    result.Text = "Oops !! Wrong Guess";
                    try //decrementing the right guess by 1
                    {
                        conn = new SqlConnection();

                        conString = "server = LAPTOP - 2ILO70CM\\SQLEXPRESS; Database = UserLog; User = Gurjot; Password = 123";

                        conn.ConnectionString = conString;
                        cmd = conn.CreateCommand();
                        string query = "UPDATE UserLog SET WrongGuess = WrongGuess + 1 WHERE Username='" + usern + "'";

                        cmd.CommandText = query;
                        conn.Open();
                        cmd.ExecuteScalar();

                    }
                    catch (Exception ex)

                    {
                        string message = ex.Message.ToString();

                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
            else
            {
                result.Text = "Your Limit Exceeded";
            } 
            }

        private void GuessLetter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (GuessLetter.Text.Length > 0)
            {
                int count = 0;

                for (int i = 0; i < MasterGuess.Length; i++)
                {
                    if (MasterGuess[i].Equals(GuessLetter.Text[0]))
                        count++;

                }
                GuessAnswer.Text = "This Letter Appers " + count + " times";
            }
            else
            {
                GuessAnswer.Text = "";
            }
        }

        private void GuessLength_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(GuessLength.Text.Length > 0)
            {
                int lengthGuess = 0;
                try
                {
                    lengthGuess = Convert.ToInt32(GuessLength.Text);
                }

                catch
                {
                    GuessAnswer1.Text = "Enter number!";
                }
                if (lengthGuess == MasterGuess.Length)
                {
                    GuessAnswer1.Text = "Correct!";
                }
                else
                {
                    GuessAnswer1.Text = "Incorrect";
                }
            }
            else
            {
                GuessAnswer1.Text = "Enter number!";
            }
           
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            App.TryGoBack(); // this method is created in app class to go back
        }
        
    }
    }

