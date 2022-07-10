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
    /// Interaction logic for drv4.xaml
    /// </summary>
    public partial class drv4 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv4()
        {
            InitializeComponent();
        }

        
            



            private void valide1(object sender, RoutedEventArgs e)
            {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM ACHAT", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    ACH.ItemsSource = Db.DefaultView;
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

            //-----------------
            /*using (SqlConnection conn = new SqlConnection(con))
                {

                    conn.Open();
                    sda = new SqlDataAdapter("SELECT * FROM ACHAT", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    ACH.ItemsSource = Db.DefaultView;

                }*/

            }

        private void ajouachat(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            //try
            //{
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
            try
            {
                //int a = Convert.ToInt32(D.Text);
                

                SqlCommand sqlcmd = new SqlCommand("SELECT COUNT(*) FROM dbo.STOCK WHERE REFERENCE = '" + C.Text + "';", conn);
                int b = (Int32)sqlcmd.ExecuteScalar();
                SqlCommand countarticle = new SqlCommand("SELECT COUNT(*) FROM ARTICLE WHERE REFERENCE = '" + C.Text + "';", conn);
                int nbrarticle = (Int32)countarticle.ExecuteScalar();
                if(nbrarticle>0)
                {
                    SqlCommand cmd1 = new SqlCommand("insert into ACHAT VALUES(@A,@B,@C,@D,@DTCOM,@E);", conn);
                    cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                    cmd1.Parameters.AddWithValue("@B", B.Text);
                    cmd1.Parameters.AddWithValue("@C", C.Text);
                    cmd1.Parameters.AddWithValue("@D", int.Parse(D.Text));
                    cmd1.Parameters.AddWithValue("@DTCOM", DATEACH.SelectedDate);
                    cmd1.Parameters.AddWithValue("@E", E.Text);
                    cmd1.ExecuteNonQuery();
                    if (b >= 1)
                    {

                        SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+" + Convert.ToInt32(D.Text) + " WHERE REFERENCE ='" + C.Text + "';", conn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("c'est bien fait");

                    }
                    else
                    {
                        SqlCommand cmd0 = new SqlCommand("insert into STOCK VALUES(@C,@B,@D);", conn);
                        cmd0.Parameters.AddWithValue("@C", C.Text);
                        cmd0.Parameters.AddWithValue("@B", B.Text);
                        cmd0.Parameters.AddWithValue("@D", int.Parse(D.Text));
                        cmd0.ExecuteNonQuery();
                        MessageBox.Show("L'article s'est ajouter dans le stock");
                        //MessageBox.Show("la quantité que vous voulez vendre est superieur à  la quantité en stock");
                    }
                    valide1(sender, e);
                }
                else
                {
                    MessageBox.Show("L'article  n'existe pas dans ARTICLE");
                }
                //int b = Convert.ToInt32(sqlcmd.ExecuteScalar());
                



                //}
                //catch (Exception ex)
                //{
                //MessageBox.Show(ex.Message);
                //MessageBox.Show("RET");
                //}
                //finally
                //{
                //  conn.Close();

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

        private void supachat(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            //try
            //{
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM ACHAT WHERE NUMACHAT =" + SUP.Text + ";", conn);
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                int a = (Int32)qt.ExecuteScalar();
                MessageBox.Show(" " + a);*/
                SqlCommand existanceEnStock = new SqlCommand("SELECT COUNT(*) FROM STOCK WHERE REFERENCE =(SELECT REFERENCE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ");", conn);
                int nbrexiste = Convert.ToInt32(existanceEnStock.ExecuteScalar());
                if(nbrexiste>0)
                {
                    SqlCommand qtenstock = new SqlCommand("SELECT QTENSTOCK FROM STOCK WHERE REFERENCE =(SELECT REFERENCE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ");", conn);
                    int qteSTOCKxiste = Convert.ToInt32(qtenstock.ExecuteScalar());
                    SqlCommand qtenachat = new SqlCommand("SELECT QTACHETE FROM ACHAT WHERE NUMACHAT =" + SUP.Text + ";", conn);
                    int qtACHATexiste = Convert.ToInt32(qtenachat.ExecuteScalar());
                    if(qteSTOCKxiste >= qtACHATexiste)
                    {
                        if (qteSTOCKxiste > qtACHATexiste)
                        {
                            SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-(SELECT QTACHETE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ") WHERE REFERENCE = (SELECT REFERENCE FROM ACHAT WHERE NUMACHAT =" + SUP.Text + ");", conn);


                            cmd2.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd2 = new SqlCommand("DELETE FROM STOCK WHERE REFERENCE = (SELECT REFERENCE FROM ACHAT WHERE NUMACHAT =" + SUP.Text + ");", conn);


                            cmd2.ExecuteNonQuery();
                        }
                    }
                    
                    
                }
                
                cmd1.ExecuteNonQuery();




                MessageBox.Show(" SUPRESSION EFFECTUEE ");
                valide1(sender, e);
            //}
            //catch (Exception ex)
            //{
                //MessageBox.Show(ex.Message);

            //}
            //finally
            //{
                //conn.Close();
            //}



        }

        private void NUMACHAT(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existemax = new SqlCommand("SELECT COUNT(*) FROM ACHAT ", conn);
                int existance = Convert.ToInt32(existemax.ExecuteScalar());
                if (existance >= 1)
                {
                    SqlCommand getmax = new SqlCommand("SELECT MAX(NUMACHAT) FROM ACHAT", conn);

                    int val = Convert.ToInt32(getmax.ExecuteScalar());

                    val = val + 1;

                    A.Text = val.ToString();

                }
                else
                {
                    A.Text = "1";
                }


            }
            catch
            {
                //MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        private void SELECTACHAT(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            if (var != null)
            {
                A.Text = var["NUMACHAT"].ToString();

                B.Text = var["NUMFOURNISSEUR"].ToString();
                C.Text = var["REFERENCE"].ToString();
                
                D.Text = var["QTACHETE"].ToString();




                DATEACH.Text = var["DATEACHAT"].ToString();
                //datedevis.Text = var["DATEDEVIS"].ToString();
                E.Text = var["MODEPAYE"].ToString();
                SUP.Text = var["NUMACHAT"].ToString();





                //date2.Text = DateTime.Now.ToString();

            }
            else
            {
                A.Text = "";

                B.Text = "";
                C.Text = "";

                D.Text = "";




                DATEACH.Text = "";
                //datedevis.Text = var["DATEDEVIS"].ToString();
                E.Text = "";
                SUP.Text = "";

            }
        }
    }
    
    
}
