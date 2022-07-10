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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for drv1.xaml
    /// </summary>
    public partial class drv1 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv1()
        {
            InitializeComponent();
        }

        private void tabledescommande(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM SITUATION", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    VNT.ItemsSource = Db.DefaultView;
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

        private void isert(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            //try
            //{
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmdexisteice = new SqlCommand("SELECT COUNT(*) FROM SITUATION WHERE ICECLIENT =  '" + A.Text + "' ;", conn) ;
                int existeice = (Int32)cmdexisteice.ExecuteScalar();
                if(existeice > 0)
                {
                    SqlCommand cmdexistefact = new SqlCommand("SELECT COUNT(*) FROM SITUATION WHERE NUMFACTURE = " + B.Text + ";",conn);
                    int existefact = (Int32)cmdexistefact.ExecuteScalar();
                    if(existefact >= 1)
                    {
                        float avance = float.Parse(C.Text);
                        SqlCommand recrest = new SqlCommand("SELECT RESTE FROM SITUATION  WHERE NUMFACTURE = " + B.Text + " AND ICECLIENT ='" + A.Text + "';", conn) ;
                        float rest = float.Parse(recrest.ExecuteScalar().ToString());
                        if(avance <= rest)
                        {
                            SqlCommand updatesituation1 = new SqlCommand("UPDATE SITUATION SET RESTE=RESTE-"+avance+"  WHERE NUMFACTURE = " + B.Text + " AND ICECLIENT ='" + A.Text + "'; ", conn);
                            SqlCommand updatesituation2 = new SqlCommand("UPDATE SITUATION SET  AVANCEDONNEE = AVANCEDONNEE + "+avance+" WHERE NUMFACTURE = " + B.Text + " AND ICECLIENT ='" + A.Text + "'; ", conn);
                            SqlCommand updatesituation3 = new SqlCommand("UPDATE SITUATION SET  DATEDAVANCE = '" + date2.Text+"' WHERE NUMFACTURE = " + B.Text + " AND ICECLIENT ='" + A.Text + "'; ", conn);
                            
                            
                            
                            updatesituation3.ExecuteNonQuery();
                            
                            
                            
                            updatesituation2.ExecuteNonQuery(); 


                            updatesituation1.ExecuteNonQuery();
                            tabledescommande(sender,  e); ;


                        }
                        else
                        {
                            MessageBox.Show("l'avance donner est superieur au reste");
                        }





                    }
                    else
                    {
                        MessageBox.Show("Le numéro de fature que vous avez saisi n'existe pas ");
                    }

                }
                else
                {
                    MessageBox.Show("ce client n'existe pas ou il a tout paye");
                }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    conn.Close();
            //}
           
            
        }

        private void factur(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM SITUATION WHERE NUMFACTURE ='" + SUP.Text + "';", conn);
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                int a = (Int32)qt.ExecuteScalar();
                MessageBox.Show(" " + a);*/
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-(SELECT QTACHETE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ")", conn);


                //cmd2.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();



                MessageBox.Show(" SUPRESSION EFFECTUEE ");
                tabledescommande(sender, e); ;
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

        private void SELECTION(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            if (var != null)
            {
                B.Text = var["NUMFACTURE"].ToString();
                A.Text = var["ICECLIENT"].ToString();
                C.Text = var["AVANCEDONNEE"].ToString();
                

                date2.Text = DateTime.Now.ToString();
                SUP.Text = var["NUMFACTURE"].ToString();

            }
            else
            {
                B.Text ="";
                A.Text ="";
                C.Text ="";


                date2.Text = "";
                SUP.Text = "";
            }
        }

        private void AVANCECHANGE(object sender, TextChangedEventArgs e)
        {

            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                if(B.Text != "" && A.Text != "")
                {
                    SqlCommand r = new SqlCommand("SELECT RESTE FROM SITUATION  WHERE NUMFACTURE = " + B.Text + " AND ICECLIENT ='" + A.Text + "';", conn);
                    float rest = float.Parse(r.ExecuteScalar().ToString());
                    rest = rest - float.Parse(C.Text);
                    D.Text = rest.ToString();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
            
        }

        private void IMPRIMECLIENT(object sender, RoutedEventArgs e)
        {
            try
            {
                viewsituation v = new viewsituation(B.Text);
                v.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        

        private void CHERCHSITUATBYNOMCLI(object sender, TextChangedEventArgs e)
        {

            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand silyaice = new SqlCommand("SELECT COUNT(*) FROM CLIENT WHERE NOMCLIENT ='" + GETSITUATBYNOMCLI.Text + "';", conn);
                int nbrclient = Convert.ToInt32(silyaice.ExecuteScalar());
                if (nbrclient > 0)
                {
                    SqlCommand getice = new SqlCommand("SELECT TOP 1 ICECLIENT FROM CLIENT WHERE NOMCLIENT ='" + GETSITUATBYNOMCLI.Text + "';", conn);
                    string ice = getice.ExecuteScalar().ToString();

                    //
                    //
                    string ligneconvient = "SELECT * FROM SITUATION WHERE ICECLIENT LIKE '%" + ice + "%';";
                    SqlDataAdapter sdaV = new SqlDataAdapter(ligneconvient, conn);
                    DataTable DT = new DataTable();
                    sdaV.Fill(DT);
                    //
                    VNT.ItemsSource = DT.DefaultView;


                }
                //else
                //{
                //    string ligneconvient = "SELECT  FROM VENTE ;";
                //    SqlDataAdapter sdaV = new SqlDataAdapter(ligneconvient, conn);
                //    DataTable DT = new DataTable();
                //    sdaV.Fill(DT);
                //    //
                //    VNT.ItemsSource = DT.DefaultView;
                //}
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
