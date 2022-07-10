using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for loginscreen.xaml
    /// </summary>
    public partial class loginscreen : Window
    {
        public loginscreen()
        {
            InitializeComponent();
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                string query = "SELECT COUNT(1) FROM tblUSER where UserName = @UserName and Password = @Password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.CommandType = CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@UserName", txtusername.Text);
                sqlcmd.Parameters.AddWithValue("@Password", txtpassword.Password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                

                if (count == 1)
                {
                    this.Background = System.Windows.Media.Brushes.DarkBlue;
                    //Thread.Sleep(20000);
                    MainWindow obj = new MainWindow();
                    obj.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mot d'enter ou Mot de passe est incorrect");
                    btnsubmit.Background = System.Windows.Media.Brushes.DarkRed;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Erreur Sémantique");
                
            }
            finally
            {
                sqlcon.Close();
            }

            
        }

        private void KYDOWNUSERNAME(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtpassword.Focus();
            }

        }
        

            private void KYDOWNPASSWORD(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnsubmit.Focus();
            }

        }

    }
}
