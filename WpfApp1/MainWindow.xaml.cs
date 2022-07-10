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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int a = 0;
        
        public MainWindow()
        {
            InitializeComponent();
        }


        private void SITUATION(object sender, RoutedEventArgs e)
        {
            
            if(a == 0 )
            {

                dr1.Visibility = Visibility.Collapsed;
                dr2.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;

                dr.Visibility = Visibility.Visible;

                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;
                //S.Background = System.Windows.Media.Brushes.DarkSeaGreen;
                
                
                a++;
            }
            else
            {
                
                dr.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;

                //S.Background = System.Windows.Media.Brushes.Blue;
                //MNWIND.Background = System.Windows.Media.Brushes.White;

                a--;
            }
            
            
        }

        private void VENTE(object sender, RoutedEventArgs e)
        {
           
            if (a==0 )
            {
                dr.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Collapsed;
                dr2.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr1.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;
                

                a++;
            }
            else
            {

                dr1.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;

                a--;
            }


        }

        private void ATTESTASTION(object sender, RoutedEventArgs e)
        {
            
            if (a == 0)
            {
                dr.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;


                dr6.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Collapsed;
                dr1.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr2.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;


                a++;
            }
            else
            {

                dr2.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }
        }

        private void ACHAT(object sender, RoutedEventArgs e)
        {
           
            if (a == 0 )
            {
                dr.Visibility = Visibility.Collapsed;
                
                dr2.Visibility = Visibility.Collapsed;
                 dr6.Visibility = Visibility.Collapsed;
                dr1.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;


                a++;
            }
            else
            {

                dr3.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }
        }

        private void STOCK(object sender, RoutedEventArgs e)
        {
           

            if (a == 0 )
            {
                dr.Visibility = Visibility.Collapsed;

                dr2.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Collapsed;
                dr1.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;


                a++;
            }
            else
            {

                dr4.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }
        }

        private void CLIENT(object sender, RoutedEventArgs e)
        {
            
            if (a == 0 )
            {
                dr.Visibility = Visibility.Collapsed;

                dr2.Visibility = Visibility.Collapsed;

                dr1.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                dr3.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;



                a++;
            }
            else
            {

                dr5.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }

        }

        private void ARTICLE(object sender, RoutedEventArgs e)
        {
           
            if (a == 0 )
            {
                dr.Visibility = Visibility.Collapsed;

                dr2.Visibility = Visibility.Collapsed;

                dr1.Visibility = Visibility.Collapsed;

                dr3.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;



                a++;
            }
            else
            {

                dr6.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }
        }

        private void FOURNISSEUR(object sender, RoutedEventArgs e)
        {
           
            if (a == 0 )
            {
                dr.Visibility = Visibility.Collapsed;

                dr2.Visibility = Visibility.Collapsed;

                dr1.Visibility = Visibility.Collapsed;

                dr3.Visibility = Visibility.Collapsed;
                dr4.Visibility = Visibility.Collapsed;
                dr5.Visibility = Visibility.Collapsed;
                dr6.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Collapsed;

                dr7.Visibility = Visibility.Visible;
                MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;



                a++;
            }
            else
            {

                dr7.Visibility = Visibility.Collapsed;
                //plainecran.Visibility = Visibility.Visible;
                //MNWIND.Background = System.Windows.Media.Brushes.White;
                a--;
            }
        }

        private void Button_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void AFFICHEPLEIN(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            //System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/#sent/DmwnWsCZDhXBzZxhVzRbvvZwGCpPZvMtLRtQbrLmHSKdgRlKFmSvrBNnwDRKhPVgrcpFQZKqCTKG");
        }

        private void bacgload(object sender, RoutedEventArgs e)
        {
            MNWIND.Background = System.Windows.Media.Brushes.DarkBlue;

        }

        private void SORTIR(object sender, RoutedEventArgs e)
        {
            loginscreen obj = new loginscreen();
            obj.Show();
            this.Close();
        }
    }
}
