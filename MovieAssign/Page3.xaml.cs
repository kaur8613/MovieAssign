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
using System.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MovieAssign
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Page3 : Page
    {
        private SqlCommand cmd;
        SqlConnection conn;
        string conString;


        public Page3()
        {
            this.InitializeComponent();
          
            
        }
        private void PageLoad(object sender, RoutedEventArgs e)
        {
            conn = new SqlConnection();

            conString = "server=LAPTOP-2ILO70CM\\SQLEXPRESS;Database=UserLog;User=Gurjot;Password=123";
            
            conn.ConnectionString = conString;
            conn.Open();
            Refresh(conString, cmd);
            conn.Close();

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e) // CRUD - Create button
        {
            
            Answer.Text = "";
            string Uname = suser.Text;
            string Pass = spass.Text;

            if ((Uname == "") || (Pass == ""))
            {
                ErrorMessage.Text = "Username & Password Required!!";
            }
            
            else
            {
               
                try
                {
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
                    ErrorMessage.Text = "Successfully Added!!";
                   
                    Refresh(conString, cmd);
                }
                catch (Exception ex)

                {
                    string message = ex.Message.ToString();
                    ErrorMessage.Text = message;
                }
                finally
                {  
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }
        private void Read_Click(object sender, RoutedEventArgs e) // CRUD - Read button 
        {
            Answer.Text = "";
            conn.ConnectionString = conString;
            cmd = conn.CreateCommand();
            conn.Open();
            Refresh(conString, cmd);
                cmd.Dispose();
                conn.Close();
            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) // CRUD - Update button 
        {
            Answer.Text = "";
            if ((suser.Text == "") || (spass.Text == "") || (ComboSelect.SelectionBoxItem == null))
            {
                ErrorMessage.Text = "Input Missing !!";
            }
            else
            {
                try
                {
                    string Uname = suser.Text;
                    string Pass = spass.Text;
                    string slectedUser = ComboSelect.SelectionBoxItem.ToString();
                    conn.ConnectionString = conString;
                    cmd = conn.CreateCommand();
                    string query = "Update UserLog set username='"
                         + Uname + "',password='"
                         + Pass + "' Where username='"

                         + slectedUser + "';";

                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();
                    ErrorMessage.Text = "Updated!!";

                    Refresh(conString, cmd);
                    
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToString();
                    ErrorMessage.Text = message;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // CRUD - Delete button
        {
            Answer.Text = "";
             if (ComboSelect.SelectionBoxItem == null)
            { ErrorMessage.Text = "Select User from Drop Down List!!"; }
            else
            {
                string slectedUser = ComboSelect.SelectionBoxItem.ToString();

                try
                {
                    conn.ConnectionString = conString;
                    cmd = conn.CreateCommand();
                    string query = "Delete from UserLog Where username='"

                         + slectedUser + "';";

                    cmd.CommandText = query;
                    conn.Open();
                    cmd.ExecuteScalar();
                    ErrorMessage.Text = "Deleted Succesfully!!";
                    Refresh(conString, cmd);
                }
                catch (Exception ex)
                {
                    string message = ex.Message.ToString();
                    ErrorMessage.Text = message;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }

        private void High1Button_Click(object sender, RoutedEventArgs e) // Highest Player Score button
        {
            Answer.Text = "";
            Answer.Visibility = Visibility;
            try
            {
                Answer.Text = "";
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                string query = "select username + '   Score :    ' + RightGuess as OUTPUT from UserLog where RightGuess = (select MAX(RightGuess) from UserLog); ";


                cmd.CommandText = query;
                conn.Open();
                string gotAnswer = (string)cmd.ExecuteScalar();
            
               // ErrorMessage.Text = "Updated!!";
                Answer.Text = "Name:   "+ gotAnswer;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ErrorMessage.Text = message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        private void HighNButton_Click(object sender, RoutedEventArgs e) // Top N Players with their score
        {
            Answer.Text = "";
            Answer.Visibility = Visibility;
            int num = 0;

            if (GetNumT.Text == "") { ErrorMessage.Text = "Enter a valid number please"; }

            try
            {
                num = Convert.ToInt32(GetNumT.Text);
            }
            catch
            {
                ErrorMessage.Text = "Enter only Numbers";
            }

            try
            {
                Answer.Text = "";
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                string query = "SELECT TOP " + num +
                    "username, MAX(RightGuess) FROM userLog Group by username order by 2 desc;";
                cmd.CommandText = query;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                for (int x = 0; x < num; x++)
                {
                    Answer.Text += dt.Rows[x][0].ToString() + "\n";
                }

                reader.Close();

             

            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ErrorMessage.Text = message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

        }

        private void Low1Button_Click(object sender, RoutedEventArgs e) // Lowet Score Player Score
        {
            Answer.Visibility = Visibility;
            try
            {
                Answer.Text = "";
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                string query = "select username + '   Score :    ' + RightGuess as OUTPUT from UserLog where RightGuess = (select MIN(RightGuess) from UserLog); ";


                cmd.CommandText = query;
                conn.Open();
                string gotAnswer = (string)cmd.ExecuteScalar();
               
                Answer.Text = gotAnswer;
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ErrorMessage.Text = message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }

        private void LowNButton_Click(object sender, RoutedEventArgs e) // Lowest N Players with thier score
        {
            Answer.Visibility = Visibility;

            if (GetNumT.Text == "") { ErrorMessage.Text = "Enter a valid number please"; }
            int num = 0;
            try
            {
                num = Convert.ToInt32(GetNumT.Text);
            }
            catch
            {
                ErrorMessage.Text = "Enter only Numbers";
            }

            try
            {
                Answer.Text = "";
                conn.ConnectionString = conString;
                cmd = conn.CreateCommand();
                string query = "SELECT TOP " + num +
                    "username, MIN(RightGuess) FROM userLog Group by username order by 2 asc;";
                cmd.CommandText = query;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                for (int x = 0; x < num; x++)
                {
                    Answer.Text += dt.Rows[x][0].ToString() + "\n";
                }

                
                reader.Close();

               

            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ErrorMessage.Text = message;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        
   
        }
         
        private void Logout_Click(object sender, RoutedEventArgs e) // Logout
        {
            App.TryGoBack();
        }

    private void Refresh( string conString, SqlCommand cmd )
        {
            
            
            Answer.Visibility = Visibility;
            try
            {
              
                cmd = conn.CreateCommand();
                string query = "Select * from UserLog ";
                cmd.CommandText = query;
                cmd.ExecuteScalar();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                Answer.Text += "ID \tUsername      Password"+"\t"+"WrongGuess   RightGuess      Register \n";
                ComboSelect.Items.Clear(); 
                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    ComboSelect.Items.Add(dt.Rows[x][1].ToString());
                       DataRow drow = dt.Rows[x];
                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                       
                      DataColumn dcol = dt.Columns[y];
                        
                      
                           
                        if (y == 5)
                        {
                        Answer.Text += dt.Rows[x][dcol].ToString().Substring(0,10);
                            
                        }
                        else if(y == 0){
                            Answer.Text += dt.Rows[x][dcol].ToString() + "\t";
                        }
                        else if(dt.Rows[x][dcol].ToString().Length > 7)
                        {
                            Answer.Text += dt.Rows[x][dcol].ToString()+ "             ";
                        }
                       else
                       {
                           Answer.Text += dt.Rows[x][dcol].ToString() + "\t           ";
                      }
                        
                      
                   }
                    Answer.Text += "\n";
                    
                }
                reader.Close();

               // ErrorMessage.Text = "Updated!!";

            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ErrorMessage.Text = message;
            }
            
        }

       
    }
}
