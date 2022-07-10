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
    /// Interaction logic for view1.xaml
    /// </summary>
    public partial class view1 : Window
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlConnection conn = new SqlConnection();
        int x;
        public view1()
        {
            InitializeComponent();
        }
        public view1(string x)
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
                CrystalReport2 ord = new CrystalReport2();
                ord.SetDataSource(ds1.Tables["facture"]);
                //CrystalDecisions.ReportSource
               bl.ViewerCore.ReportSource = ord;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //string sql = "SELECT * FROM VENTE";
            //DataSet ds = new DataSet();
            //SqlDataAdapter dad = new SqlDataAdapter(sql, conn);
            //dad.Fill(ds.Tables["facture"]) ;
            //CrystalReport1 rprt = new CrystalReport1();
            //rprt.SetDataSource(ds.Tables["facture"]);
            //c.ViewerCore.ReportSource = rprt;
            //c.ViewerCore.RefreshReport();
        }
    }
}
