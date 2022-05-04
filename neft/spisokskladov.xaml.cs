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
    /// Логика взаимодействия для spisokskladov.xaml
    /// </summary>
    public partial class spisokskladov : UserControl
    {
        public spisokskladov()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT namesk, SUM(koltovar) AS [Всего товаров], SUM(obstoim) AS [Общая стоимость всех товаров] FROM sklad GROUP BY namesk", sqlCon);
                DataTable dt = new DataTable();
                sqlCon.Open();
                SqlDataReader srd = cmd.ExecuteReader();
                dt.Load(srd);
                sqlCon.Close();
                datagrid.ItemsSource = dt.DefaultView;


            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "Склад";
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "Склад";
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       

        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "Склад";
        }

   
    }
}
