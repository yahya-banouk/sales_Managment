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
    /// Interaction logic for drv7.xaml
    /// </summary>
    public partial class drv7 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv7()
        {
            InitializeComponent();
        }

        private void AJOUCLIENT(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //int a = Convert.ToInt32(C.Text);
                //SqlCommand sqlcmd = new SqlCommand("SELECT QTENSTOCK FROM dbo.STOCK WHERE REFERENCE = " + E.Text, conn);
                //int b = (Int32)sqlcmd.ExecuteScalar();
                //if (a <= b)
                //{
                SqlCommand cmd1 = new SqlCommand("insert into ARTICLE VALUES(@A,@B,@C,@D,@E,@F);", conn);
                cmd1.Parameters.AddWithValue("@A", A.Text);
                cmd1.Parameters.AddWithValue("@B", B.Text);
                cmd1.Parameters.AddWithValue("@C", Math.Round(float.Parse(C.Text),3));
                cmd1.Parameters.AddWithValue("@D", D.Text);
                cmd1.Parameters.AddWithValue("@E", E.Text) ;
                cmd1.Parameters.AddWithValue("@F", F.Text);
                cmd1.ExecuteNonQuery();
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+" + Convert.ToInt32(D.Text) + " WHERE REFERENCE =" + C.Text + ";", conn);
                //cmd2.ExecuteNonQuery();
                MessageBox.Show("c'est bien fait");
                TABLECLIENT(sender, e);
                //}
                //else
                //{
                //MessageBox.Show("la quantité que vous voulez vendre est superieur à  la quantité en stock");
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

        private void TABLECLIENT(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM ARTICLE", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    ART.ItemsSource = Db.DefaultView;
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





        private void SUP(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM ARTICLE WHERE REFERENCE ='" + SUPTXT.Text + "';", conn);
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                int a = (Int32)qt.ExecuteScalar();
                MessageBox.Show(" " + a);*/
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-(SELECT QTACHETE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ")", conn);


                //cmd2.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();



                MessageBox.Show(" SUPRESSION EFFECTUEE ");
                TABLECLIENT(sender, e);

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




        private void ARTICLESELECTE(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            try
            {
                //
                if (var != null)
                {
                    A.Text = var["REFERENCE"].ToString();

                    B.Text = var["DESIHNATION"].ToString();
                    C.Text = var["PRIX_UNI_HT"].ToString();

                    D.Text = var["COLOR"].ToString();
                    //datedevis.Text = var["DATEDEVIS"].ToString();
                    E.Text = var["UNITE"].ToString();
                    F.Text = var["CATEGORIE"].ToString();
                    SUPTXT.Text = var["REFERENCE"].ToString();





                    //date2.Text = DateTime.Now.ToString();

                }
                else
                {
                    A.Text = "";

                    B.Text = "";
                    C.Text = "";

                    D.Text = "";
                    //datedevis.Text = var["DATEDEVIS"].ToString();
                    E.Text = "";
                    F.Text = "";



                    SUPTXT.Text = "";

                }

                //
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }

            
        }

        private void CHERCHDESI(object sender, TextChangedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            { 
                string ligneconvient = "SELECT * FROM ARTICLE WHERE DESIHNATION LIKE '%" + GETBYDESI.Text + "%';";
            
                SqlDataAdapter sda1 = new SqlDataAdapter(ligneconvient, conn);
            
                DataTable DT = new DataTable();
            
                sda1.Fill(DT);
            //
            
                ART.ItemsSource = DT.DefaultView;

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

        private void CHERCHREFE(object sender, TextChangedEventArgs e)
        {   SqlConnection conn = new SqlConnection(con);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            try
            {
                string ligneconvient = "SELECT * FROM ARTICLE WHERE REFERENCE LIKE '%" + GETBYREF.Text + "%';";
            
                SqlDataAdapter sda1 = new SqlDataAdapter(ligneconvient, conn);
            
                DataTable DT = new DataTable();
            
                sda1.Fill(DT);
            
                //
            ART.ItemsSource = DT.DefaultView;

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
