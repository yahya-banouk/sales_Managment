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
    /// Interaction logic for drv2.xaml
    /// </summary>
    public partial class drv2 : UserControl
    {
        //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-4VMGARO\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True");
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        bool POSSIBILITY = true;
        //string con = @"Data Source=192.168.11.167,1433;Initial Catalog=AZAROU;User ID=hi;Password=N@ouz123";
        public drv2()
        {
            InitializeComponent();
        }

        
        
        


        private void tabledescommande(object sender, RoutedEventArgs e)
        {

                using (SqlConnection conn = new SqlConnection(con))
                {
                try
                {
                    if(conn.State== ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM VENTE", conn);
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
            try
            {
                if (F.Text != "")
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand sqlcmd0 = new SqlCommand("SELECT COUNT(*) FROM dbo.STOCK WHERE REFERENCE = '" + E.Text + "';", conn);
                    int c = (Int32)sqlcmd0.ExecuteScalar();
                    SqlCommand article = new SqlCommand("SELECT COUNT(*) FROM ARTICLE WHERE REFERENCE = '" + E.Text + "';", conn);
                    int art = (Int32)article.ExecuteScalar();
                    if (art >= 1)
                    {
                        //SqlCommand cmfduplication = new SqlCommand("SELECT COUN(*) FROM VENTE WHERE REFERENCE NUMPAPIERS ICECLIENT", conn);
                        if (c >= 1)
                        {
                            int a = Convert.ToInt32(C.Text);
                            SqlCommand sqlcmd = new SqlCommand("SELECT QTENSTOCK FROM dbo.STOCK WHERE REFERENCE = '" + E.Text + "';", conn);
                            int b = Convert.ToInt32(sqlcmd.ExecuteScalar());
                            if (a <= b)
                            {



                                SqlCommand cmd1 = new SqlCommand("insert into VENTE VALUES(@A,@B,@C,@DTCOM,@D,@E,@F,@G,@H,@I,@DTECH);", conn);
                                cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                                cmd1.Parameters.AddWithValue("@B", B.Text);
                                cmd1.Parameters.AddWithValue("@C", int.Parse(C.Text));
                                cmd1.Parameters.AddWithValue("@DTCOM", date.SelectedDate);
                                cmd1.Parameters.AddWithValue("@D", D.Text);

                                cmd1.Parameters.AddWithValue("@E", E.Text);
                                cmd1.Parameters.AddWithValue("@F", int.Parse(F.Text));
                                cmd1.Parameters.AddWithValue("@G", G.Text);
                                //
                                cmd1.Parameters.AddWithValue("@H", Math.Round(float.Parse(H.Text),3));
                                cmd1.Parameters.AddWithValue("@I", Math.Round(float.Parse(I.Text),3));
                                //
                                cmd1.Parameters.AddWithValue("@DTECH", date2.SelectedDate);
                                cmd1.ExecuteNonQuery();
                                tabledescommande(sender, e);

                                SqlCommand testF = new SqlCommand("SELECT COUNT(*) FROM SITUATION WHERE NUMFACTURE = " + F.Text + " AND ICECLIENT = '" + B.Text + "';", conn);
                                int nbrF = (Int32)testF.ExecuteScalar();
                                if (nbrF == 0)
                                {
                                    DateTime dataujour = DateTime.Now;
                                    SqlCommand cmdsituation = new SqlCommand("insert into SITUATION VALUES(@F,@B,0,(SELECT SUM(QTVENDU*PRIXVENTE-(QTVENDU*PRIXVENTE*REMISE/100)+(20*PRIXVENTE*QTVENDU/100)) FROM VENTE WHERE NUMPAPIERS = @F),'" + dataujour + "' ,@DTECH);", conn);
                                    cmdsituation.Parameters.AddWithValue("@F", int.Parse(F.Text));
                                    cmdsituation.Parameters.AddWithValue("@B", B.Text);
                                    cmdsituation.Parameters.AddWithValue("@DTECH", date2.SelectedDate);
                                    cmdsituation.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand updatesituation = new SqlCommand("UPDATE SITUATION SET RESTE = RESTE+(SELECT PRIXVENTE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE))*"+ int.Parse(C.Text) + " - ((SELECT PRIXVENTE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE))*" + int.Parse(C.Text) + "*" + int.Parse(H.Text) / 100 + ") + ((SELECT PRIXVENTE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE))*" + int.Parse(C.Text) + "*20 / 100 ) WHERE NUMFACTURE =" + F.Text + " AND ICECLIENT = '" + B.Text + "';", conn);
                                    updatesituation.ExecuteNonQuery();
                                }

                                if (a < b)
                                {

                                    SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + " WHERE REFERENCE ='" + E.Text + "';", conn);
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("mise à jour de stock s'est bien faite");
                                }
                                else
                                {
                                    SqlCommand cmd2 = new SqlCommand("DELETE FROM STOCK  WHERE REFERENCE ='" + E.Text + "';", conn);
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("mise à jour de stock s'est bien faite");
                                }
                                


                            }
                            else
                            {
                                MessageBox.Show("la quantité que vous voulez vendre est superieur à  la quantité en stock");

                            }
                        }
                        else
                        {
                            MessageBox.Show("Cet article n'existe pas dans le stock verifier le stock ou la reference que vous avez saisi");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ajouter les info de cet article dans ARTICLE avant d'effectuer la vente");
                    }
                }
                else
                {
                    MessageBox.Show("vous avez oublié de remplir un champ nécessaire");

                }
                
                

                
                

                

            }
            catch (Exception ex)
            {
                  MessageBox.Show(ex.Message);
                //MessageBox.Show("dsfvdf");
            }
            finally
            {
                conn.Close();
                
            }
            
            

                 

            
            
            
            
            
            
            
            
        }

        private void factur(object sender, RoutedEventArgs e)
        {
            //facture f = new facture(A.Text);
            //f.Show();
            try
            {
                view v = new view(F.Text);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {

                conn.Open();
                SqlCommand cmd0 = new SqlCommand("SELECT COUNT(*) FROM STOCK WHERE REFERENCE = (SELECT REFERENCE FROM VENTE WHERE NUMVENTE =" +SUP.Text+");", conn);
                int a = (Int32)cmd0.ExecuteScalar();
                
                
                if (a>=1)
                {

                    //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                    /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                    int a = (Int32)qt.ExecuteScalar();
                    MessageBox.Show(" " + a);*/
                    //SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM STOCK WHERE REFERENCE = (SELECT REFERENCE FROM VENTE WHERE NUMVENTE =" + SUP.Text + ");", conn);
                    //int y = (Int32)cmd3.ExecuteScalar();
                    //if(y>=1)
                    //{
                        SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+(SELECT QTVENDU FROM VENTE WHERE NUMVENTE="+SUP.Text+ ") WHERE REFERENCE = (SELECT REFERENCE FROM VENTE WHERE NUMVENTE=" + SUP.Text + ") ;", conn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("la  mise à jour du stock s'est bien fait");
                    //}
                    
                    
                        //SqlCommand cmd2 = new SqlCommand("insert into STOCK VALUES()  WHERE REFERENCE = (SELECT REFERENCE FROM VENTE WHERE NUMVENTE =" + SUP.Text + ");", conn);
                        //cmd2.ExecuteNonQuery();
                        /*SqlCommand cmd1 = new SqlCommand("insert into VENTE VALUES(@A,@B,@C,@DTCOM,@D,@E,@F,@G,@H);", conn);
                        cmd1.Parameters.AddWithValue("@A", int.Parse(A.Text));
                        cmd1.Parameters.AddWithValue("@B", B.Text);
                        cmd1.Parameters.AddWithValue("@C", int.Parse(C.Text));
                        cmd1.Parameters.AddWithValue("@DTCOM", date.SelectedDate);
                        cmd1.Parameters.AddWithValue("@D", D.Text);
                        */
                    
                }
                else
                {

                    MessageBox.Show("cet article n'existe pas dans dans le STOCK ");
                    SqlCommand cmdINSER = new SqlCommand("insert into STOCK(REFERENCE,QTENSTOCK) SELECT VENTE.REFERENCE  , QTVENDU FROM VENTE WHERE NUMVENTE=" + SUP.Text + " AND REFERENCE = (SELECT REFERENCE FROM VENTE WHERE NUMVENTE=" + SUP.Text + ");", conn);
                    
                    cmdINSER.ExecuteNonQuery();
                    MessageBox.Show("L'article s'est ajouter dans le stock");
                }
                //23/04/2021
                //SqlCommand testF = new SqlCommand("SELECT COUNT(*) FROM SITUATION WHERE NUMFACTURE = (SELECT TOP 1 NUMPAPIERS FROM VENTE WHERE NUMVENTE =" + SUP.Text + ") AND ICECLIENT = (SELECT TOP 1 ICECLIENT FROM VENTE WHERE NUMVENTE =" + SUP.Text + ");", conn);
                //int nbrF = Convert.ToInt32(testF.ExecuteScalar());
                //if(nbrF>0)
                //{
                //    SqlCommand selectionreste = new SqlCommand("SELECT REST FROM SITUATION WHERE NUMFACTURE = (SELECT TOP 1 NUMPAPIERS FROM VENTE WHERE NUMVENTE =" + SUP.Text + ") AND ICECLIENT = (SELECT TOP 1 ICECLIENT FROM VENTE WHERE NUMVENTE =" + SUP.Text + ");", conn);
                //    float rest = float.Parse(selectionreste.ExecuteScalar().ToString());

                //}
                //


                SqlCommand cmd1 = new SqlCommand("DELETE FROM VENTE WHERE NUMVENTE =" + SUP.Text + ";", conn);
                MessageBox.Show("c'est bien fait");
                cmd1.ExecuteNonQuery();
                tabledescommande(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                /*SqlCommand cmdd = new SqlCommand("update crystale reporte  wher nom == crystalereportsi ftom app1")
                
                 */


            }
            finally
            {
                conn.Close();
            }

            

        }

        private void bl(object sender, RoutedEventArgs e)
        {
            try
            {
                view1 x = new view1(F.Text);
                x.Show();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            
        }

        private void bc(object sender, RoutedEventArgs e)
        {
            try
            {
                view2 x = new view2(F.Text);
                x.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                /*SqlConnection conn = new SqlConnection(con);
                if(conn.State==ConnectionState.Closed)
                {
                    conn.Open();
                }*/

            }
            

        }

        private void A_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //private void SORTIEICE(object sender, KeyEventArgs e)
        //{
        //    C.Text = "QLWA1";
        //}

        private void SORTIEICE1(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CLIENT WHERE ICECLIENT = '" + B.Text + "';", conn);
                int a = (Int32)cmd.ExecuteScalar();
                if(a>=1)
                {
                    SqlCommand cmd0 = new SqlCommand("SELECT NOMCLIENT FROM CLIENT WHERE ICECLIENT = '"+ B.Text+"';", conn);
                    string b = (string)cmd0.ExecuteScalar();
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM DEVIS WHERE NOMCLIENT = '" + b + "' AND REFERENCE = '" + E.Text + "';", conn); ;
                    int c = (Int32)cmd1.ExecuteScalar();
                    if(c>=1)
                    {
                        if(E.Text != "")
                        {
                            SqlCommand cmd2 = new SqlCommand("SELECT PRIX FROM DEVIS WHERE NUMDEVIS = (SELECT MAX(NUMDEVIS) FROM DEVIS WHERE NOMCLIENT =  '" + b+ "' AND REFERENCE = '" + E.Text + "' );", conn) ;
                        
                        
                            I.Text = cmd2.ExecuteScalar().ToString() ;
                        }
                        

                    }
                    else
                    {
                        if(E.Text != "")
                        {
                            SqlCommand dernierecommdinchallah = new SqlCommand("SELECT COUNT(*) FROM ARTICLE  WHERE  REFERENCE = '" + E.Text + "';", conn);
                            int dernexe = (Int32)cmd1.ExecuteScalar();
                            if(dernexe>0)
                            { 
                                SqlCommand cmd3 = new SqlCommand("SELECT PRIX_UNI_HT FROM ARTICLE WHERE  REFERENCE = '" + E.Text + "';", conn) ;


                                I.Text = cmd3.ExecuteScalar().ToString();
                            }
                            
                        }
                        
                    } 

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

        private void VENTESELECT(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            if (var != null)
            {
                if(POSSIBILITY)
                {
                    A.Text = var["NUMVENTE"].ToString();

                    B.Text = var["ICECLIENT"].ToString();
                    C.Text = var["QTVENDU"].ToString();
                    date.Text = var["DATEVENTE"].ToString();
                    D.Text = var["MODEPAYE"].ToString();
                    //datedevis.Text = var["DATEDEVIS"].ToString();
                    E.Text = var["REFERENCE"].ToString();
                    F.Text = var["NUMPAPIERS"].ToString();
                    G.Text = var["NOMVENDEUR"].ToString();
                    H.Text = var["REMISE"].ToString();
                    date2.Text = var["DATEDECHEANCE"].ToString();
                    I.Text = var["PRIXVENTE"].ToString();
                    SUP.Text = var["NUMVENTE"].ToString();
                }
                SUP.Text = var["NUMVENTE"].ToString();






                //date2.Text = DateTime.Now.ToString();

            }
            else
            {
                if(POSSIBILITY)
                {
                    A.Text = "";

                    B.Text = "";
                    C.Text = "";
                    date.Text = "";
                    D.Text = "";
                    //datedevis.Text = var["DATEDEVIS"].ToString();
                    E.Text = "";
                    F.Text = "";
                    G.Text = "";
                    H.Text = "";
                    date2.Text = "";
                    I.Text = "";
                    SUP.Text = "";
                }
                SUP.Text = "";

            }
        }

        

        

        

        

        private void IN(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existemax = new SqlCommand("SELECT COUNT(*) FROM VENTE ", conn);
                int existance = Convert.ToInt32(existemax.ExecuteScalar());
                if(existance >= 1)
                {
                    SqlCommand getmax = new SqlCommand("SELECT MAX(NUMVENTE) FROM VENTE", conn);
            
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

        private void meme_Checked(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existevente = new SqlCommand("SELECT COUNT(*) FROM VENTE ;",conn);
                int nbrevente = Convert.ToInt32(existevente.ExecuteScalar());
                if(nbrevente > 0)
                {
                    SqlCommand derniereligne = new SqlCommand("SELECT NUMPAPIERS FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);",conn);
                    int numpapier = Convert.ToInt32(derniereligne.ExecuteScalar());
                    F.Text = numpapier.ToString() ;
                    SqlCommand DSQL = new SqlCommand("SELECT MODEPAYE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    D.Text = DSQL.ExecuteScalar().ToString();
                    H.Text = "0";
                    SqlCommand DATESQL = new SqlCommand("SELECT DATEVENTE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    date.Text = DATESQL.ExecuteScalar().ToString();
                    SqlCommand DATE2SQL = new SqlCommand("SELECT DATEDECHEANCE FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    date2.Text = DATE2SQL.ExecuteScalar().ToString();
                    SqlCommand NOMVENDEURSQL = new SqlCommand("SELECT NOMVENDEUR FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    G.Text = NOMVENDEURSQL.ExecuteScalar().ToString();
                    SqlCommand ICECLI = new SqlCommand("SELECT ICECLIENT FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    B.Text = ICECLI.ExecuteScalar().ToString();
                    //
                    SORTIEICE1(sender,e) ;
                    //
                    B.Background = System.Windows.Media.Brushes.Gray;
                    nomclient.Background = System.Windows.Media.Brushes.Gray;
                    D.Background = System.Windows.Media.Brushes.Gray;
                    H.Background = System.Windows.Media.Brushes.Gray;
                    date.Background = System.Windows.Media.Brushes.Gray;
                    date2.Background = System.Windows.Media.Brushes.Gray;
                    G.Background = System.Windows.Media.Brushes.Gray;
                    
                    B.IsEnabled = false;
                    nomclient.IsEnabled = false;
                    D.IsEnabled = false;
                    H.IsEnabled = false;
                    date.IsEnabled = false;
                    date2.IsEnabled = false;
                    G.IsEnabled = false;
                    POSSIBILITY = false;

                }
                else
                {
                    POSSIBILITY = true;
                    F.Text = "1";
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

        private void nonmeme_Checked(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand existevente = new SqlCommand("SELECT COUNT(*) FROM VENTE ;", conn);
                int nbrevente = Convert.ToInt32(existevente.ExecuteScalar());
                if (nbrevente > 0)
                {
                    SqlCommand derniereligne = new SqlCommand("SELECT NUMPAPIERS FROM VENTE WHERE NUMVENTE = (SELECT MAX(NUMVENTE) FROM VENTE);", conn);
                    int numpapier = Convert.ToInt32(derniereligne.ExecuteScalar());
                    numpapier = numpapier + 1;
                    F.Text = numpapier.ToString();
                    B.Background = System.Windows.Media.Brushes.White;
                    nomclient.Background = System.Windows.Media.Brushes.White;
                    D.Background = System.Windows.Media.Brushes.White;
                    H.Background = System.Windows.Media.Brushes.White;
                    date.Background = System.Windows.Media.Brushes.White;
                    date2.Background = System.Windows.Media.Brushes.White;
                    G.Background = System.Windows.Media.Brushes.White;

                    B.IsEnabled = true;
                    nomclient.IsEnabled = true;
                    D.IsEnabled = true;
                    H.IsEnabled = true;
                    date.IsEnabled = true;
                    date2.IsEnabled = true;
                    G.IsEnabled = true;
                    POSSIBILITY = true;

                }
                else
                {
                    POSSIBILITY = true;
                    F.Text = "1";
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

        private void TROUVEICE(object sender, TextChangedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand silyaice = new SqlCommand("SELECT COUNT(*) FROM CLIENT WHERE NOMCLIENT ='"+nomclient.Text+"';",conn);
                int nbrclient = Convert.ToInt32(silyaice.ExecuteScalar());
                if(nbrclient >0)
                {
                    SqlCommand getice = new SqlCommand("SELECT TOP 1 ICECLIENT FROM CLIENT WHERE NOMCLIENT ='" + nomclient.Text + "';", conn) ;
                    B.Text = getice.ExecuteScalar().ToString();
                    SORTIEICE1(sender, e);

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

        private void ICECHANGEMENT(object sender, TextChangedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CLIENT WHERE ICECLIENT = '" + B.Text + "';", conn);
                int a = (Int32)cmd.ExecuteScalar();
                if (a >= 1)
                {
                    SqlCommand cmd0 = new SqlCommand("SELECT NOMCLIENT FROM CLIENT WHERE ICECLIENT = '" + B.Text + "';", conn);
                    string b = (string)cmd0.ExecuteScalar();
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM DEVIS WHERE NOMCLIENT = '" + b + "' AND REFERENCE = '" + E.Text + "';", conn); ;
                    int c = (Int32)cmd1.ExecuteScalar();
                    if (c >= 1)
                    {
                        SqlCommand cmd2 = new SqlCommand("SELECT PRIX FROM DEVIS WHERE NUMDEVIS = (SELECT MAX(NUMDEVIS) FROM DEVIS WHERE NOMCLIENT =  '" + b + "' AND REFERENCE = '" + E.Text + "' );", conn);


                        I.Text = cmd2.ExecuteScalar().ToString();

                    }
                    else
                    {
                        if (E.Text != null)
                        {
                            SqlCommand requetnbr = new SqlCommand("SELECT COUNT(*) FROM ARTICLE WHERE REFERENCE = '" + E.Text + "';", conn);
                            int nbrexist = Convert.ToInt32(requetnbr.ExecuteScalar());
                            if(nbrexist >0)
                            {
                                SqlCommand cmd3 = new SqlCommand("SELECT PRIX_UNI_HT FROM ARTICLE WHERE  REFERENCE = '" + E.Text + "';", conn);


                                I.Text = cmd3.ExecuteScalar().ToString();

                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("donner la refeence d'article pour avoir le prix");
                        }
                        
                    }

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

        private void CHERCHREFER(object sender, TextChangedEventArgs e)
        {   SqlConnection conn = new SqlConnection(con);
            //try
            //{

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string ligneconvient = "SELECT * FROM VENTE WHERE REFERENCE LIKE '%" + GETBYREF.Text + "%';";
                SqlDataAdapter sda1 = new SqlDataAdapter(ligneconvient, conn);
                DataTable DT = new DataTable();
                sda1.Fill(DT);
                //
                VNT.ItemsSource = DT.DefaultView;
            //}
            //catch (Exception ex)
            //{
                //MessageBox.Show(ex.Message);
            //}
            //finally
            //{
                conn.Close();
            //}
            
            

        }

        private void CHERCHNOMCLI(object sender, TextChangedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            try
            {
                SqlCommand silyaice = new SqlCommand("SELECT COUNT(*) FROM CLIENT WHERE NOMCLIENT ='" + GETBYNOMCLI.Text + "';", conn);
                int nbrclient = Convert.ToInt32(silyaice.ExecuteScalar());
                if (nbrclient > 0)
                {
                    SqlCommand getice = new SqlCommand("SELECT TOP 1 ICECLIENT FROM CLIENT WHERE NOMCLIENT ='" + GETBYNOMCLI.Text + "';", conn);
                    string ice = getice.ExecuteScalar().ToString();

                    //
                    //
                    string ligneconvient = "SELECT * FROM VENTE WHERE ICECLIENT LIKE '%" + ice + "%';";
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
