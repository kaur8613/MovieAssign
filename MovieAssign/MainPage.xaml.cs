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
using Microsoft.Extensions.Configuration;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MovieAssign
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        User user;
        private SqlCommand cmd;
        SqlConnection conn;
        string conString;
        string Usern;
        string Pass;
        public MainPage()
        {
          
            this.InitializeComponent();
            
        }
        private void MainLoad(object sender, RoutedEventArgs e)
        {
             conn = new SqlConnection();

             conString = "server=LAPTOP-2ILO70CM\\SQLEXPRESS;Database=UserLog;User=Gurjot;Password=123";
          
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conn = new SqlConnection();

            conString = "server=LAPTOP-2ILO70CM\\SQLEXPRESS;Database=UserLog;User=Gurjot;Password=123";

            user = new User();
                  
            user.Decide = 1;
          
            

            if ((username.Text == "") || (passwordBox.Password.ToString() == ""))
            {
                Wrong.Text = "Username & Password Required!!";
            }
            else
            {
                Usern = username.Text;
                Pass = passwordBox.Password.ToString();
                try
                {
                    conn.ConnectionString = conString;
                    cmd = conn.CreateCommand();
                    string query = "Select count(*) from UserLog WHERE Username='" + Usern + "'AND password='" + Pass + "'";

                    cmd.CommandText = query;
                    conn.Open();
                    int num = (int)cmd.ExecuteScalar();
                    if (num == 1)
                    {
                        user.Username = Usern;
                        this.Frame.Navigate(typeof(Page2), user);
                    }
                    else
                    {
                        Wrong.Text = "Incorrect Creditails";
                    }

                }
                catch(Exception ex)

                {
                    string message = ex.Message.ToString();
                    Wrong.Text = message;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

     

        private void NotLog_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            user.Decide = 0;


            this.Frame.Navigate(typeof(Page2),user);
        }

        private void Admin_log(object sender, RoutedEventArgs e) // admin login as powerful otherwise can also operate as user
        {
            Usern = username.Text;
            Pass = passwordBox.Password.ToString();
            if ((Usern == "") || (Pass == ""))
            {
                Wrong.Text = "nope";
            }
            else if((Usern == "DarioGuelph") && (Pass == "abcde"))
            {
                this.Frame.Navigate(typeof(Page3));
            }
            else
            {
                Wrong.Text = "Incorrect ";
            }
        }
   

        private void RegisterN(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page4Regis));
        }
    }
   
}
