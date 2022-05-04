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
    /// Логика взаимодействия для sotrudniki.xaml
    /// </summary>
    public partial class sotrudniki : UserControl
    {
        public sotrudniki()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT id_user, fam, name, otch, telefon, dolgnost FROM users WHERE email != 'admin'", sqlCon);
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

        void labelidadd()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT id_user FROM users WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textboxname.Text);
                string id = Convert.ToString(cmd.ExecuteScalar());
                labelid.Content = id;
                sqlCon.Close();

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
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                textboxfam.Text = dr["fam"].ToString();
                textboxname.Text = dr["name"].ToString();
                textboxotch.Text = dr["otch"].ToString();
                textboxtelefon.Text = dr["telefon"].ToString();
                textboxdolg.Text = dr["dolgnost"].ToString();
                labelid.Content = dr["id_user"].ToString();
                labelidadd();

            }
        }



        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT id_user, fam, name, otch, telefon, dolgnost FROM users WHERE name != 'admin' and name LIKE '%" + sotrpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;

            DataTable dt = new DataTable();

            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();

            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            sotrpoisk.Clear();
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
           
           
        }

        private void buttonedit_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_user"];

                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE users SET fam='" + textboxfam + "', name='" + textboxname.Text + "', otch='" + textboxotch.Text + "', telefon='" + textboxtelefon.Text + "', dolgnost='" + textboxdolg.Text + "' WHERE id_users=" + id + ";", sqlCon);
                  
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                 
                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                   
                    textboxfam.Clear();
                    textboxname.Clear();
                    textboxotch.Clear();
                    textboxtelefon.Clear();
                    textboxdolg.Clear();
                    datagrid.Columns[0].Header = "id сотрудника";
                    datagrid.Columns[1].Header = "Фамилия";
                    datagrid.Columns[2].Header = "Имя";
                    datagrid.Columns[3].Header = "Отчество";
                    datagrid.Columns[4].Header = "Телефон";
                    datagrid.Columns[5].Header = "Должность";


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы редактировать запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonsbr_Click(object sender, RoutedEventArgs e)
        {
            textboxfam.Clear();
            textboxname.Clear();
            textboxotch.Clear();
            textboxtelefon.Clear();
            textboxdolg.Clear();
            datagrid.Columns[0].Header = "id сотрудника";
            datagrid.Columns[1].Header = "Фамилия";
            datagrid.Columns[2].Header = "Имя";
            datagrid.Columns[3].Header = "Отчество";
            datagrid.Columns[4].Header = "Телефон";
            datagrid.Columns[5].Header = "Должность";
        }
    }
}
