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
    public sealed partial class Page4Regis : Page
    {
        private SqlCommand cmd;
        SqlConnection conn;
        string conString;
        string Uname;
        public string SaveName;
        public Page4Regis()
        {
            this.InitializeComponent();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            
            conn = new SqlConnection();

            conString = "server=LAPTOP-2ILO70CM\\SQLEXPRESS;Database=UserLog;User=Gurjot;Password=123";

            if ((suser.Text == "") || (spass.Text == ""))
            {
                wrong.Text = "Username & Password Required!!";
            }
            else
            {
                try
                {
                     Uname = suser.Text;
                    string Pass = spass.Text;
                    conn.ConnectionString = conString;
                    cmd = conn.CreateCommand();
                    string query = "Insert into UserLog values('"
                         + Uname + "','"
                         + Pass + "','"
                         + 0 + "','"
                         + 0 + "','"
                         + DateTime.Now + "')";

                    cmd.CommandText = query;
                    conn.Open();
                   cmd.ExecuteScalar();
                  wrong.Text = "Successfully Added!!";
                    SaveName = Uname;
                    
                }
                catch (Exception ex)

                {
                    string message = ex.Message.ToString();
                    wrong.Text = message;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
           
            App.TryGoBack();
        }
    }
}
