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
    /// Логика взаимодействия для spisanietovar.xaml
    /// </summary>
    public partial class spisanietovar : UserControl
    {
        public spisanietovar()
        {
            InitializeComponent();
            LoadGrid();

        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM spistovar", sqlCon);
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
                SqlCommand cmd = new SqlCommand("SELECT id_spis FROM spistovar WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", textboxtovar.Text);
                string id = Convert.ToString(cmd.ExecuteScalar());
                labelid.Content = id;
                sqlCon.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }





        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";




        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";
        }



        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                textboxtovar.Text = dr["name"].ToString();
                textboxcena.Text = dr["cena"].ToString();
                textboxopis.Text = dr["opisanie"].ToString();
                labelid.Content = dr["id_spis"].ToString();
                labelidadd();

            }
        }

       

        private void comboboxtovar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




        }



        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT id_spis FROM spistovar WHERE name = @name", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@name", textboxtovar.Text);
            string id = Convert.ToString(cmd.ExecuteScalar());
            labelid.Content = id;
            sqlCon.Close();
        }

        private void buttonedit_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_spis"];

                try
                {

                    SqlCommand cmd2 = new SqlCommand("UPDATE spistovar SET name='" + textboxtovar.Text + "', cena='" + textboxcena.Text + "', opisanie='" + textboxopis.Text + "' WHERE id_spistovar=" + labelid.Content + ";", sqlCon);
                    sqlCon.Open();
                    cmd2.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxtovar.Text = "";
                    textboxopis.Clear();
                    textboxcena.Clear();


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы редактировать запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonobn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";


        }

        private void buttonsbr_Click(object sender, RoutedEventArgs e)
        {
            textboxtovar.Text = "";
            textboxopis.Clear();
            textboxcena.Clear();
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";
        }

        private void btndel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_spis"];

                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM spistovar WHERE id_spis=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxtovar.Text = "";
                    textboxopis.Clear();
                    textboxcena.Clear();


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы удалить запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btndeltab_Click(object sender, RoutedEventArgs e)
        {
            DeleteTableView dashboard = new DeleteTableView();
            dashboard.Show();
            textboxtovar.Text = "";
            textboxopis.Clear();
            textboxcena.Clear();

        }







        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM spistovar WHERE name LIKE '%" + textboxtovarpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;

            DataTable dt = new DataTable();

            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();

            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            textboxtovarpoisk.Clear();
            datagrid.Columns[0].Header = "id товара";
            datagrid.Columns[1].Header = "Название";
            datagrid.Columns[2].Header = "Цена";
            datagrid.Columns[3].Header = "Описание";
        }



        private void textboxcena_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void textboxopis_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textboxopis_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void btnspis_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_spis"];

                try
                {
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO tovar VALUES (@name, @cena, @opisanie)", sqlCon);
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.AddWithValue("@name", textboxtovar.Text);
                    cmd2.Parameters.AddWithValue("@cena", textboxcena.Text);
                    cmd2.Parameters.AddWithValue("@opisanie", textboxopis.Text);
                    SqlCommand cmd = new SqlCommand("DELETE FROM spistovar WHERE id_spis=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd2.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Товар успешно добавлен на склад.", "Списание", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    textboxtovar.Text = "";
                    textboxopis.Clear();
                    textboxcena.Clear();


                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы вернуть товар, нужно выбрать его в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
   