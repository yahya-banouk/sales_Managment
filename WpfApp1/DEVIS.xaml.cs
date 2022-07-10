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
    /// Interaction logic for DEVIS.xaml
    /// </summary>
    public partial class DEVIS : Window
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";

        SqlConnection conn = new SqlConnection();
        int x;
        public DEVIS()
        {
            InitializeComponent();
        }
        public DEVIS(string x)
        {
            InitializeComponent();
            this.x = int.Parse(x);

        }
        private void DEVIS1(object sender, RoutedEventArgs e)
        {
            conn.ConnectionString = con;
            //try
            //{
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string req = "SELECT NUMDEVIS, NOMCLIENT, DEVIS.REFERENCE, QT, REMISE, DATEDEVIS, PRIX, DEVIS.NUMPAPIER, ADRESSECLIENT, ARTICLE.DESIHNATION FROM DEVIS , ARTICLE WHERE DEVIS.REFERENCE = ARTICLE.REFERENCE    AND NUMPAPIER = " + x + ";";
                DataSetDevis ds1 = new DataSetDevis();
                SqlDataAdapter dad = new SqlDataAdapter(req, conn);
                dad.Fill(ds1.Tables["DEVISTABLE"]);
                CrystalReportDEVIS ord = new CrystalReportDEVIS();
                ord.SetDataSource(ds1.Tables["DEVISTABLE"]);
                //CrystalDecisions.ReportSource
                zb.ViewerCore.ReportSource = ord;
            //}
            //catch (Exception ex)
            //{
              //  MessageBox.Show(ex.Message);
            //}
            //finally
            //{
                conn.Close();
            //}
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

