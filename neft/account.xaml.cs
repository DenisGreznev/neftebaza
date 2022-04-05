using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace neft
{
    /// <summary>
    /// Логика взаимодействия для account.xaml
    /// </summary>
    public partial class account : Window
    {
        public account()
        {
            InitializeComponent();
            this.txttele.PreviewTextInput += new TextCompositionEventHandler(txttele_PreviewTextInput);

        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();

            
         
            String queryfam = "SELECT fam FROM users WHERE email=@email";
            String queryname = "SELECT name FROM users WHERE email=@email";
            String queryotch = "SELECT otch FROM users WHERE email=@email";
         
            String querydatar = "SELECT datar FROM users WHERE email=@email";
            String querytelefon = "SELECT telefon FROM users WHERE email=@email";
            String queryadres = "SELECT adres FROM users WHERE email=@email";
            String querydolg = "SELECT dolgnost FROM users WHERE email=@email";
            String querypassword = "SELECT password FROM users WHERE email=@email";



            SqlCommand sqlCmdfam = new SqlCommand(queryfam, sqlCon);
            SqlCommand sqlCmdname = new SqlCommand(queryname, sqlCon);
            SqlCommand sqlCmdotch = new SqlCommand(queryotch, sqlCon);
           
            SqlCommand sqlCmddatar = new SqlCommand(querydatar, sqlCon);
            SqlCommand sqlCmdtelefon = new SqlCommand(querytelefon, sqlCon);
            SqlCommand sqlCmdadres = new SqlCommand(queryadres, sqlCon);
            SqlCommand sqlCmddolg = new SqlCommand(querydolg, sqlCon);
            SqlCommand sqlCmdpassword = new SqlCommand(querypassword, sqlCon);



            sqlCmdfam.CommandType = System.Data.CommandType.Text;
            sqlCmdfam.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmdname.CommandType = System.Data.CommandType.Text;
            sqlCmdname.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmdotch.CommandType = System.Data.CommandType.Text;
            sqlCmdotch.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmddatar.CommandType = System.Data.CommandType.Text;
            sqlCmddatar.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmdtelefon.CommandType = System.Data.CommandType.Text;
            sqlCmdtelefon.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmdadres.CommandType = System.Data.CommandType.Text;
            sqlCmdadres.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmddolg.CommandType = System.Data.CommandType.Text;
            sqlCmddolg.Parameters.AddWithValue("@email", labelemail.Content);
            sqlCmdpassword.CommandType = System.Data.CommandType.Text;
            sqlCmdpassword.Parameters.AddWithValue("@email", labelemail.Content);


            string fam = Convert.ToString(sqlCmdfam.ExecuteScalar());
            string name = Convert.ToString(sqlCmdname.ExecuteScalar());
            string otch = Convert.ToString(sqlCmdotch.ExecuteScalar());
           
            string datar = Convert.ToString(sqlCmddatar.ExecuteScalar());
            string telefon = Convert.ToString(sqlCmdtelefon.ExecuteScalar());
            string adres = Convert.ToString(sqlCmdadres.ExecuteScalar());
            string dolg = Convert.ToString(sqlCmddolg.ExecuteScalar());
            string password = Convert.ToString(sqlCmdpassword.ExecuteScalar());
            labelname.Content = name;
            labelotch.Content = otch;
            labelfam.Content = fam;
            labeldata.Content = datar;
            labeltele.Content = telefon;
            labeladres.Content = adres;
            labeldolg.Content = dolg;
            if (labeldata.Content != "")
            {
                reddate.Visibility = Visibility.Collapsed;
            }
            if (labeldata.Content == "") reddate.Visibility = Visibility.Visible;




        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void reddate_Click(object sender, RoutedEventArgs e)
        {
            reddate.Visibility = Visibility.Collapsed;
            reddateprim.Visibility = Visibility.Visible;
            txtdatar.Visibility = Visibility.Visible;
            labeldata.Visibility = Visibility.Collapsed;

        }

        private void reddateprim_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE users SET datar='"+txtdatar.Text+"' WHERE email=@email;", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@email", labelemail.Content);
                cmd.ExecuteNonQuery();
                txtdatar.Visibility = Visibility.Collapsed;
                labeldata.Visibility = Visibility.Visible;
                reddate.Visibility = Visibility.Visible;
                reddateprim.Visibility = Visibility.Collapsed;
            labeldata.Content = "";
                String querydatar = "SELECT datar FROM users WHERE email=@email";
                SqlCommand sqlCmddatar = new SqlCommand(querydatar, sqlCon);
                sqlCmddatar.CommandType = System.Data.CommandType.Text;
                sqlCmddatar.Parameters.AddWithValue("@email", labelemail.Content);
                string datar = Convert.ToString(sqlCmddatar.ExecuteScalar());
                sqlCon.Close();
            labeldata.Content = datar;
        }

        private void redadres_Click(object sender, RoutedEventArgs e)
        {
            redadres.Visibility = Visibility.Collapsed;
            redadresprim.Visibility = Visibility.Visible;
            txtadres.Visibility = Visibility.Visible;
            labeladres.Visibility = Visibility.Collapsed;
        }

        private void redadresprim_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE users SET adres='" + txtadres.Text + "' WHERE email=@email;", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@email", labelemail.Content);
            cmd.ExecuteNonQuery();
            txtadres.Visibility = Visibility.Collapsed;
            labeladres.Visibility = Visibility.Visible;
            redadres.Visibility = Visibility.Visible;
            redadresprim.Visibility = Visibility.Collapsed;
            labeladres.Content = "";
            String queryadres = "SELECT adres FROM users WHERE email=@email";
            SqlCommand sqlCmdadres = new SqlCommand(queryadres, sqlCon);
            sqlCmdadres.CommandType = System.Data.CommandType.Text;
            sqlCmdadres.Parameters.AddWithValue("@email", labelemail.Content);
            string adres = Convert.ToString(sqlCmdadres.ExecuteScalar());
            sqlCon.Close();
            labeladres.Content = adres;
        }

        private void redtele_Click(object sender, RoutedEventArgs e)
        {
            redtele.Visibility = Visibility.Collapsed;
            redteleprim.Visibility = Visibility.Visible;
            txttele.Visibility = Visibility.Visible;
            labeltele.Visibility = Visibility.Collapsed;
        }

        private void redteleprim_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE users SET telefon='" + txttele.Text + "' WHERE email=@email;", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@email", labelemail.Content);
            cmd.ExecuteNonQuery();
            txttele.Visibility = Visibility.Collapsed;
            labeltele.Visibility = Visibility.Visible;
            redtele.Visibility = Visibility.Visible;
            redteleprim.Visibility = Visibility.Collapsed;
            labeltele.Content = "";
            String querytele = "SELECT telefon FROM users WHERE email=@email";
            SqlCommand sqlCmdtele = new SqlCommand(querytele, sqlCon);
            sqlCmdtele.CommandType = System.Data.CommandType.Text;
            sqlCmdtele.Parameters.AddWithValue("@email", labelemail.Content);
            string tele = Convert.ToString(sqlCmdtele.ExecuteScalar());
            sqlCon.Close();
            labeltele.Content = tele;
        }

        private void txttele_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void txttele_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void reddolg_Click(object sender, RoutedEventArgs e)
        {
            reddolg.Visibility = Visibility.Collapsed;
            reddolgprim.Visibility = Visibility.Visible;
            boxdolg.Visibility = Visibility.Visible;
            labeldolg.Visibility = Visibility.Collapsed;
        }

        private void reddolgprim_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE users SET dolgnost='" + boxdolg.Text + "' WHERE email=@email;", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@email", labelemail.Content);
            cmd.ExecuteNonQuery();
            boxdolg.Visibility = Visibility.Collapsed;
            labeldolg.Visibility = Visibility.Visible;
            reddolg.Visibility = Visibility.Visible;
            reddolgprim.Visibility = Visibility.Collapsed;
            labeldolg.Content = "";
            String querydolg = "SELECT dolgnost FROM users WHERE email=@email";
            SqlCommand sqlCmddolg = new SqlCommand(querydolg, sqlCon);
            sqlCmddolg.CommandType = System.Data.CommandType.Text;
            sqlCmddolg.Parameters.AddWithValue("@email", labelemail.Content);
            string dolg = Convert.ToString(sqlCmddolg.ExecuteScalar());
            sqlCon.Close();
            labeldolg.Content = dolg;
        }
    }
}
