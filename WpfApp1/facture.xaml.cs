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
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using System.Printing;
using System.Resources.Extensions;

using System.Data;




namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for facture.xaml
    /// </summary>
    public partial class facture : Window
    {
        int x;
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        
        public facture()
        {
            InitializeComponent();
        }
        public facture( string x)
        {

            InitializeComponent();
            this.x = int.Parse(x);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SqlConnection conn = new SqlConnection(con);
            string req = "select NUMVENTE ICECLIENT QTVENDU MODEPAYE REFERENCE NUMPAPIERS NOMVENDUER DESIGNATION PRIX_UNI_HT NOMCLIENT REMISE PRIX_UNI_HT*QTVENDU from VENTE CLIENT ARTICLE where VENTE.REFERENCE = ARTICLE.REFERENCE AND VENTE.ICECLIENT =  CLIENT.ICECLIENT AND NUMPAPIERS = " + x;
            dataset1 ds1 = new dataset1();
            SqlDataAdapter dad = new SqlDataAdapter(req,conn) ;
            dad.Fill(ds1.Tables["order"]);
            order ord = new order();
            ord.SetDataSource(ds1.Tables["order"]);
            //CrystalDecisions.ReportSource
            y.ViewerCore.ReportSource = ord;

            
            

            
             
           
            
            
            
            


        }
    }
}
