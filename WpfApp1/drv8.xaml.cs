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
    /// Interaction logic for drv8.xaml
    /// </summary>
    public partial class drv8 : UserControl
    {
        string con = @"Data Source=.\MSSQLSERVER99;Initial Catalog=ORBILAC;Integrated Security=True";
        SqlDataAdapter sda;
        public drv8()
        {
            InitializeComponent();
        }
        
        private void AJOUFOURNISSEUR(object sender, RoutedEventArgs e)
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
                SqlCommand cmd1 = new SqlCommand("insert into FOURNISSEUR VALUES(@A,@B,@C,@D,@E,@F);", conn);
                cmd1.Parameters.AddWithValue("@A", A.Text);
                cmd1.Parameters.AddWithValue("@B", B.Text);
                cmd1.Parameters.AddWithValue("@C", C.Text);
                cmd1.Parameters.AddWithValue("@D", D.Text);
                cmd1.Parameters.AddWithValue("@E", E.Text);
                cmd1.Parameters.AddWithValue("@F", F.Text);
                cmd1.ExecuteNonQuery();
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK+" + Convert.ToInt32(D.Text) + " WHERE REFERENCE =" + C.Text + ";", conn);
                //cmd2.ExecuteNonQuery();
                MessageBox.Show("c'est bien fait");
                TABLEFOURNISSEUR(sender, e);
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

        private void TABLEFOURNISSEUR(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    sda = new SqlDataAdapter("SELECT *   FROM FOURNISSEUR", conn);
                    DataTable Db = new DataTable();
                    sda.Fill(Db);
                    FOUR.ItemsSource = Db.DefaultView;
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

        private void SUPFOURNISSEUR(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM FOURNISSEUR WHERE NUMFORNISSEUR ='" + SUP.Text+"';", conn);
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-" + C.Text + "where REFERENCE =" + E.Text+" ;", conn);
                /*SqlCommand qt = new SqlCommand("SELECT QTVENDU FROM dbo.VENTE WHERE QTVENDU = 123", conn);
            
                int a = (Int32)qt.ExecuteScalar();
                MessageBox.Show(" " + a);*/
                //SqlCommand cmd2 = new SqlCommand("UPDATE STOCK SET QTENSTOCK=QTENSTOCK-(SELECT QTACHETE FROM ACHAT WHERE NUMACHAT=" + SUP.Text + ")", conn);


                //cmd2.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();



                MessageBox.Show(" SUPRESSION EFFECTUEE ");
                TABLEFOURNISSEUR(sender, e);



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

        

        private void CHERCHEFOURNISSEURPARNOM(object sender, TextChangedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(con);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string ligneconvient = "SELECT * FROM FOURNISSEUR WHERE NOMFOURNISSEUR LIKE '%" + CHERCHEFOURNISSEURNOM.Text + "%';";
            SqlDataAdapter sda1 = new SqlDataAdapter(ligneconvient, conn);
            DataTable DT = new DataTable();
            sda1.Fill(DT);
            FOUR.ItemsSource = DT.DefaultView;
        }

        private void FOURSELECT(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid DT = (DataGrid)sender;
            DataRowView VAR = DT.SelectedItem as DataRowView;
            if (VAR != null)
            {
                A.Text = VAR["NUMFORNISSEUR"].ToString();
                B.Text = VAR["NOMFOURNISSEUR"].ToString();
                C.Text = VAR["PRENOMFOURNISSEUR"].ToString();
                D.Text = VAR["ADRESSEFOURNISSEUR"].ToString();
                E.Text = VAR["NUMTELEFOURNISSEUR"].ToString();
                F.Text = VAR["EMAILFOURNISSEUR"].ToString();
                SUP.Text = VAR["NUMFORNISSEUR"].ToString();
                //GETBYNM.Text = VAR["NOMCLIENT"].ToString();


            }
            else
            {
                A.Text = "";
                B.Text = "";
                C.Text = "";
                D.Text = "";
                E.Text = "";
                F.Text = "";
                SUP.Text = "";
                //GETBYNM.Text = "";

            }
        }
    }
}
