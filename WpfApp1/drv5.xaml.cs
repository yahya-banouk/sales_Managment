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
    /// Interaction logic for drv5.xaml
    /// </summary>
    public partial class drv5 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv5()
        {
            InitializeComponent();
        }
        private void TABLESTOCK(object sender, RoutedEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    sda = new SqlDataAdapter("SELECT * FROM STOCK", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    st.ItemsSource = Db.DefaultView;
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

        private void ajoustock(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //int a = Convert.ToInt32(C.Text);
                //SqlCommand sqlcmd = new SqlCommand("SELECT QTENSTOCK FROM dbo.STOCK WHERE REFERENCE = " + E.Text, conn);
                /*int b = (Int32)sqlcmd.ExecuteScalar();
                if (a <= b)
                {
                SqlCommand cmd1 = new SqlCommand("insert into STOCK VALUES(@A,@B,@C);", conn);
                
                cmd1.Parameters.AddWithValue("@A", A.Text);
                cmd1.Parameters.AddWithValue("@B", B.Text);
                cmd1.Parameters.AddWithValue("@C", int.Parse(C.Text));
                
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+" + Convert.ToInt32(D.Text) + " WHERE REFERENCE =" + C.Text + ";", conn);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("c'est bien fait");
                }
                else
                {
                MessageBox.Show("la quantité que vous voulez vendre est superieur à  la quantité en stock");
                }*/
                SqlCommand sqlcmd = new SqlCommand("SELECT COUNT(*) FROM dbo.STOCK WHERE REFERENCE = '" + A.Text + "';", conn);
                int b = (Int32)sqlcmd.ExecuteScalar();
                if (b >= 1)
                {
                    SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+" + Convert.ToInt32(C.Text) + " WHERE REFERENCE = '" + A.Text + "';", conn);
                    SqlCommand cmdexistefour = new SqlCommand("SELECT COUNT(*) FROM FOURNISSEUR WHERE  NUMFORNISSEUR = '" + B.Text + "';", conn) ;
                    int nbrfourexiste = (Int32)cmdexistefour.ExecuteScalar();
                    if(nbrfourexiste > 0)
                    {
                        SqlCommand cmdFOR = new SqlCommand("UPDATE STOCK SET NUMFOURNISSEUR='" + B.Text + "' WHERE REFERENCE = '" + A.Text + "';", conn);
                       
                        cmdFOR.ExecuteNonQuery();
                        MessageBox.Show("la mise à jour de de numéro de fournisseur est bien faites ");
                    }
                    
                    
                    

                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("la mise à jour de quantité est bien faites ");
                   
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("insert into STOCK VALUES(@A,@B,@C);", conn);

                    cmd1.Parameters.AddWithValue("@A", A.Text);
                    cmd1.Parameters.AddWithValue("@B", B.Text);
                    cmd1.Parameters.AddWithValue("@C", int.Parse(C.Text));

                    cmd1.ExecuteNonQuery();
                    MessageBox.Show("Operation Effectuer ! n'oublie pas d'ajouter les details de cet article dans ARTICLE ");
                }
                TABLESTOCK(sender, e);



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

        private void SUP_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                conn.Open();
                //SqlCommand cmd1 = new SqlCommand("DELETE FROM STOCK WHERE REFERENCE =" + SUP.Text + ";", conn);
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                int a = (Int32)qt.ExecuteScalar();
                MessageBox.Show(" " + a);*/
                int a = Convert.ToInt32(qt.Text);
                SqlCommand sqlcmd = new SqlCommand("SELECT QTENSTOCK FROM dbo.STOCK WHERE REFERENCE = '" + refe.Text+"';", conn);
                int b = Convert.ToInt32(sqlcmd.ExecuteScalar());
                if(a<=b)
                {
                    if(a<b)
                    {
                        
                        SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-"+ Convert.ToInt32(qt.Text)+"where REFERENCE ='"+refe.Text+"';", conn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show(" mise à jour effectuer ");
                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand("DELETE FROM STOCK where REFERENCE = '" + refe.Text+"';", conn);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show(" SUPRESSION EFFECTUEE ");
                    }
                    TABLESTOCK(sender, e);



            }
                else
                {
                    MessageBox.Show("VOUS POUVEZ PAS EFFECTUER CETTE OPPERATION CAR QUANTITE EST SUPERIEUR AU STOCK");
                }
                


                //cmd2.ExecuteNonQuery();
                
                //cmd1.ExecuteNonQuery();



                
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

        private void SLECTIONLIGNE(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dt = (DataGrid)sender;
            DataRowView var = dt.SelectedItem as DataRowView;
            if (var != null)
            {
                A.Text = var["REFERENCE"].ToString();

                B.Text = var["NUMFOURNISSEUR"].ToString();
                C.Text = var["QTENSTOCK"].ToString();
                refe.Text = var["REFERENCE"].ToString(); 
                qt.Text = var["QTENSTOCK"].ToString();




                //date2.Text = DateTime.Now.ToString();

            }
            else
            {
                A.Text = "";

                B.Text = "";
                C.Text = "";
                refe.Text = "";
                qt.Text = "";

            }
        }
    }
}
