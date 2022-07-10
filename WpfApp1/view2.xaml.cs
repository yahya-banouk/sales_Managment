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


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for view2.xaml
    /// </summary>
    public partial class view2 : Window
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlConnection conn = new SqlConnection();
        int x;
        public view2()
        {
            InitializeComponent();
        }
        public view2(string x)
        {
            InitializeComponent();

            this.x = int.Parse(x);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn.ConnectionString = con;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string req = "select VENTE.NUMVENTE , VENTE.ICECLIENT ,VENTE.QTVENDU , VENTE.DATEVENTE ,VENTE.MODEPAYE ,VENTE.REFERENCE, VENTE.NUMPAPIERS, VENTE.NOMVENDEUR, ARTICLE.DESIHNATION ,ARTICLE.PRIX_UNI_HT, NOMCLIENT, REMISE, PRIX_UNI_HT*QTVENDU AS TOTALHT from VENTE ,CLIENT, ARTICLE where VENTE.REFERENCE = ARTICLE.REFERENCE AND VENTE.ICECLIENT =  CLIENT.ICECLIENT  AND NUMPAPIERS = " + x + ";";
                DataSet ds1 = new DataSet();
                SqlDataAdapter dad = new SqlDataAdapter(req, conn);
                dad.Fill(ds1.Tables["facture"]);
                CrystalReport3 ord = new CrystalReport3();
                ord.SetDataSource(ds1.Tables["facture"]);
                //CrystalDecisions.ReportSource
                bc.ViewerCore.ReportSource = ord;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
