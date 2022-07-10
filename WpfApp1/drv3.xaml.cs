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
    /// Interaction logic for drv3.xaml
    /// </summary>
    public partial class drv3 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv3()
        {
            InitializeComponent();
        }

        private void insertionDevis(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand verifarticle = new SqlCommand("SELECT COUNT(*) FROM ARTICLE WHERE REFERENCE = '" + C.Text + "';", conn);
                SqlCommand verifstock = new SqlCommand("SELECT COUNT(*) FROM STOCK WHERE REFERENCE = '" + C.Text + "';", conn);
                SqlCommand verifnumdevis = new SqlCommand("SELECT COUNT(*) FROM DEVIS WHERE NUMDEVIS = " + A.Text + ";", conn);

                //
                int article1 = (Int32)verifarticle.ExecuteScalar();
                int stock1 = (Int32)verifstock.ExecuteScalar();
                int numdevis1 = (Int32)verifnumdevis.ExecuteScalar();

                //
                string article2 = C.Text;
                int stock2 = Convert.ToInt32(D.Text);
                int numdevis2 = Convert.ToInt32(A.Text);
                if (numdevis1 == 0)
                {



                    if (article1 >= 1)
                    {
                        if (stock1 >= stock2)
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into DEVIS VALUES(@A,@B,@C,@D,@E,@DTDEVIS,@F,@G,@H);", conn);
                            cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                            cmd1.Parameters.AddWithValue("@B", B.Text);
                            cmd1.Parameters.AddWithValue("@C", C.Text);
                            cmd1.Parameters.AddWithValue("@D", int.Parse(D.Text));
                            cmd1.Parameters.AddWithValue("@E", Math.Round(float.Parse(E.Text),2));
                            cmd1.Parameters.AddWithValue("@DTDEVIS", datedevis.SelectedDate);
                            cmd1.Parameters.AddWithValue("@F", Math.Round(float.Parse(F.Text),4));
                            cmd1.Parameters.AddWithValue("@G", int.Parse(G.Text));
                            cmd1.Parameters.AddWithValue("@H", H.Text);

                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Cette opération est effectuée avec succés");
                        }
                        else
                        {
                            SqlCommand cmd1 = new SqlCommand("insert into DEVIS VALUES(@A,@B,@C,@D,@E,@DTDEVIS,@F,@G,@H);", conn);
                            cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                            cmd1.Parameters.AddWithValue("@B", B.Text);
                            cmd1.Parameters.AddWithValue("@C", C.Text);
                            cmd1.Parameters.AddWithValue("@D", int.Parse(D.Text));
                            cmd1.Parameters.AddWithValue("@E", float.Parse(E.Text));
                            cmd1.Parameters.AddWithValue("@DTDEVIS", datedevis.SelectedDate);
                            cmd1.Parameters.AddWithValue("@F", float.Parse(F.Text));
                            cmd1.Parameters.AddWithValue("@G", int.Parse(G.Text));
                            cmd1.Parameters.AddWithValue("@H", H.Text);


                            cmd1.ExecuteNonQuery();
                            MessageBox.Show("Remarque : l'operation est effectuée mais la quantité que vous avez saisi est superieur à  la quantité en stock");
                        }
                        TABLEDEVIS(sender, e);
                    }
                    else
                    {

                        MessageBox.Show("Erreur : cet article n'existe pas vérifier la référence que vous avez saisi ");
                    }

                }
                else
                {
                    MessageBox.Show("Erreur : le Numero de Devis que vous avez saisi est deja exist ");

                }




                //SqlCommand sqlcmd0 = new SqlCommand("SELECT COUNT(*) FROM dbo.STOCK WHERE REFERENCE = '" + C.Text + "';", conn);
                //int c = (Int32)sqlcmd0.ExecuteScalar();
                //if (c >= 1)
                //{
                //string d = C.Text;
                //SqlCommand verifarticle = new SqlCommand("SELECT COUNT(*) FROM ARTICLE WHERE REFERENCE = '" + C.Text, conn);
                //int d = (Int32)verifarticle.ExecuteScalar();
                //if (d >= 1)
                //{
                //int a = Convert.ToInt32(D.Text);
                //SqlCommand sqlcmd = new SqlCommand("SELECT QTENSTOCK FROM dbo.STOCK WHERE REFERENCE = '" + C.Text + "';", conn);
                //int b = (Int32)sqlcmd.ExecuteScalar();
                //if (a <= b)
                //{

                //    SqlCommand cmd1 = new SqlCommand("insert into DEVIS VALUES(@A,@B,@C,@D,@E,@DTDEVIS,@F,@G);", conn);
                //    cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                //    cmd1.Parameters.AddWithValue("@B", B.Text);
                //    cmd1.Parameters.AddWithValue("@C", C.Text);
                //    cmd1.Parameters.AddWithValue("@D", int.Parse(D.Text));
                //    cmd1.Parameters.AddWithValue("@E", float.Parse(E.Text));
                //    cmd1.Parameters.AddWithValue("@DTCOM", date.SelectedDate);



                //cmd1.Parameters.AddWithValue("@F", float.Parse(F.Text));
                //cmd1.Parameters.AddWithValue("@G", int.Parse(G.Text));
                //cmd1.Parameters.AddWithValue("@H", float.Parse(H.Text));
                //cmd1.ExecuteNonQuery();
                //if (a < b)
                //{

                //    SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + " WHERE REFERENCE ='" + E.Text + "';", conn);
                //    cmd2.ExecuteNonQuery();
                //    MessageBox.Show("mise à jour de stock s'est bien faite");
                //}
                //else
                //{
                //    SqlCommand cmd2 = new SqlCommand("DELETE FROM STOCK  WHERE REFERENCE ='" + E.Text + "';", conn);
                //    cmd2.ExecuteNonQuery();
                //    MessageBox.Show("mise à jour de stock s'est bien faite");
                //}


                //}
                //else
                //{
                //    MessageBox.Show("l'operation a été effectuer mais la quantité que vous avez saisi est superieur à  la quantité en stock");
                //    SqlCommand cmd1 = new SqlCommand("insert into DEVIS VALUES(@A,@B,@C,@D,@E,@DTDEVIS,@F,@G);", conn);
                //    cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                //    cmd1.Parameters.AddWithValue("@B", B.Text);
                //    cmd1.Parameters.AddWithValue("@C", C.Text);
                //    cmd1.Parameters.AddWithValue("@D", int.Parse(D.Text));
                //    cmd1.Parameters.AddWithValue("@E", float.Parse(E.Text));
                //    cmd1.Parameters.AddWithValue("@DTCOM", date.SelectedDate);



                //cmd1.Parameters.AddWithValue("@F", float.Parse(F.Text));
                //cmd1.Parameters.AddWithValue("@G", int.Parse(G.Text));
                //cmd1.Parameters.AddWithValue("@H", float.Parse(H.Text));
                //cmd1.ExecuteNonQuery();
                //if (a < b)
                //{

                //    SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + " WHERE REFERENCE ='" + E.Text + "';", conn);
                //    cmd2.ExecuteNonQuery();
                //    MessageBox.Show("mise à jour de stock s'est bien faite");
                //}
                //else
                //{
                //    SqlCommand cmd2 = new SqlCommand("DELETE FROM STOCK  WHERE REFERENCE ='" + E.Text + "';", conn);
                //    cmd2.ExecuteNonQuery();
                //    MessageBox.Show("mise à jour de stock s'est bien faite");
                //}
                //}
                //}
                //else
                //{
                //MessageBox.Show("cette article n'existe pas dans article il faut l'ajouter dans ARTICLE pour effectuer cette operation");
                //}

                //}
                //else
                //{
                //    MessageBox.Show("cette article n'existe pas dans article il faut l'ajouter dans ARTICLE pour effectuer cette operation");
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

        private void TABLEDEVIS(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM DEVIS", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    DEVIS.ItemsSource = Db.DefaultView;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                conn.Open();
                SqlCommand verifnumdevis = new SqlCommand("SELECT COUNT(*) FROM DEVIS WHERE NUMDEVIS = " + SUP.Text + ";", conn);
                int numdevis1 = (Int32)verifnumdevis.ExecuteScalar();
                if (numdevis1 >= 1)
                {
                    SqlCommand cmd1 = new SqlCommand("DELETE FROM DEVIS WHERE NUMDEVIS = " + SUP.Text + ";", conn);
                    //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                    /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);

                    int a = (Int32)qt.ExecuteScalar();
                    MessageBox.Show(" " + a);*/
                    //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-(SELECT QTACHETE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ")", conn);


                    //cmd2.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();



                    MessageBox.Show(" SUPRESSION EFFECTUEE ");
                    TABLEDEVIS(sender, e);
                }
                else
                {
                    MessageBox.Show("Ce numéro de Devis que vous avez saisi n'existe pas ");
                }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //facture f = new facture(A.Text);
            //f.Show();
            try
            {
                DEVIS d = new DEVIS(G.Text);
                d.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void DEVISCANGEMENT(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            if (var != null)
            {
                A.Text = var["NUMDEVIS"].ToString();

                B.Text = var["NOMCLIENT"].ToString();
                C.Text = var["REFERENCE"].ToString();
                D.Text = var["QT"].ToString();
                E.Text = var["REMISE"].ToString();
                datedevis.Text = var["DATEDEVIS"].ToString();
                F.Text = var["PRIX"].ToString();
                G.Text = var["NUMPAPIER"].ToString();
                H.Text = var["ADRESSECLIENT"].ToString();
                SUP.Text = var["NUMDEVIS"].ToString();



                //date2.Text = DateTime.Now.ToString();

            }
            else
            {
                A.Text = "";

                B.Text = "";
                C.Text = "";
                D.Text = "";
                E.Text = "";
                datedevis.Text = "";
                F.Text = "";
                G.Text = "";
                H.Text = "";
                SUP.Text = "";
            }
        }

        private void AVOIRNUMDEVIS(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existemax = new SqlCommand("SELECT COUNT(*) FROM DEVIS ", conn);
                int existance = Convert.ToInt32(existemax.ExecuteScalar());
                if (existance >= 1)
                {
                    SqlCommand getmax = new SqlCommand("SELECT MAX(NUMDEVIS) FROM DEVIS", conn);

                    int MAXNUMDEVIS = Convert.ToInt32(getmax.ExecuteScalar());

                    MAXNUMDEVIS = MAXNUMDEVIS + 1;

                    A.Text = MAXNUMDEVIS.ToString();

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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existevente = new SqlCommand("SELECT COUNT(*) FROM DEVIS ;", conn);
                int nbrevente = Convert.ToInt32(existevente.ExecuteScalar());
                if (nbrevente > 0)
                {
                    SqlCommand derniereligne = new SqlCommand("SELECT NUMPAPIER FROM DEVIS WHERE NUMDEVIS = (SELECT MAX(NUMDEVIS) FROM DEVIS);", conn);
                    int numpapier = Convert.ToInt32(derniereligne.ExecuteScalar());
                    G.Text = numpapier.ToString();

                }
                else
                {
                    G.Text = "1";
                }

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

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existevente = new SqlCommand("SELECT COUNT(*) FROM DEVIS ;", conn);
                int nbrevente = Convert.ToInt32(existevente.ExecuteScalar());
                if (nbrevente > 0)
                {
                    SqlCommand derniereligne = new SqlCommand("SELECT NUMPAPIER FROM DEVIS WHERE NUMDEVIS = (SELECT MAX(NUMDEVIS) FROM DEVIS);", conn);
                    int numpapier = Convert.ToInt32(derniereligne.ExecuteScalar());
                    numpapier = numpapier + 1;
                    G.Text = numpapier.ToString();

                }
                else
                {
                    G.Text = "1";
                }

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
