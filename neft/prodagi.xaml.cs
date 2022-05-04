using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace neft
{
    /// <summary>
    /// Логика взаимодействия для prodagi.xaml
    /// </summary>
    public partial class prodagi : UserControl
    {
        public prodagi()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; MultipleActiveResultSets=True; Integrated Security=True;");

        public void LoadGrid()
        {
            
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT tovar.name AS [Товар], SUM(zakaz.koltovar) AS [Продано], SUM(zakaz.summa) AS [На сумму] FROM tovar,zakaz WHERE tovar.id_tovar=zakaz.id_tovar GROUP BY tovar.name", sqlCon);
                SqlCommand cmd2 = new SqlCommand("SELECT klient.namekl AS [Клиент], SUM(zakaz.koltovar) AS [Куплено товаров], SUM(zakaz.summa) AS [На сумму] FROM tovar,zakaz,klient WHERE klient.id_klient=zakaz.id_klient and tovar.id_tovar=zakaz.id_tovar GROUP BY klient.namekl", sqlCon);
                SqlCommand cmd3 = new SqlCommand("SELECT SUM(zakaz.summa) FROM zakaz;", sqlCon);
                DataTable dt = new DataTable();
                DataTable dt2 = new DataTable();
                SqlDataReader srd = cmd.ExecuteReader();
                string stoim = Convert.ToString(cmd3.ExecuteScalar());
                SqlDataReader srd2 = cmd2.ExecuteReader();
                dt.Load(srd);
                dt2.Load(srd2);
                sqlCon.Close();
                datagrid.ItemsSource = dt.DefaultView;
                datagrid2.ItemsSource = dt2.DefaultView;
                textstoim.Content = stoim;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            
        }

        private void allspisok_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void datagrid2_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void datagrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
