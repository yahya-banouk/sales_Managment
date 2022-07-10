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
    /// Interaction logic for viewsituation.xaml
    /// </summary>
    public partial class viewsituation : Window
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlConnection conn = new SqlConnection();
        int x;
        public viewsituation()
        {
            InitializeComponent();
        }
        public viewsituation(string x)
        {
            InitializeComponent();
            this.x = int.Parse(x);

        }
        private void situationloaded(object sender, RoutedEventArgs e)
        {
            conn.ConnectionString = con;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string req = "SELECT SITUATION.NUMFACTURE , SITUATION.ICECLIENT , SITUATION.AVANCEDONNEE , SITUATION.RESTE , SITUATION.DATEDAVANCE , SITUATION.DATEDECHEANCE , SITUATION.NUMPAPIER , VENTE.DATEVENTE , CLIENT.NOMCLIENT FROM SITUATION , VENTE , CLIENT WHERE  VENTE.NUMPAPIERS = "+x+ " AND SITUATION.ICECLIENT = VENTE.ICECLIENT AND CLIENT.ICECLIENT=SITUATION.ICECLIENT ";
                DataSetsituation ds1 = new DataSetsituation();
                SqlDataAdapter dad = new SqlDataAdapter(req, conn);
                dad.Fill(ds1.Tables["situation"]);
                CrystalReportsituation ord = new CrystalReportsituation();
                ord.SetDataSource(ds1.Tables["situation"]);
                //CrystalDecisions.ReportSource
                impsitu.ViewerCore.ReportSource = ord;
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
