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
    /// Логика взаимодействия для tovarsinsklad.xaml
    /// </summary>
    public partial class tovarsinsklad : UserControl
    {
        public tovarsinsklad()
        {
            InitializeComponent();
            LoadGrid();
            fill_combo();
            
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

        public void LoadGrid()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT id_sklad, tovar.id_tovar, sklad.namesk, tovar.name, tovar.cena, sklad.koltovar, sklad.obstoim FROM sklad, tovar WHERE tovar.id_tovar=sklad.id_tovar", sqlCon);
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

        void fill_combo()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM tovar", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(1);
                    comboboxtovar.Items.Add(name);
                    comboboxtovarpoisk.Items.Add(name);
                    
                }
                sqlCon.Close();

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
                SqlCommand cmd = new SqlCommand("SELECT id_tovar FROM tovar WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", comboboxtovar.Text);
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
            datagrid.Columns[0].Header = "id склада";
            datagrid.Columns[1].Header = "id товара";
            datagrid.Columns[2].Header = "Название склада";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Цена (1 ед)";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Общая стоимость";
            spisokskladov.SelectedIndex = 0;
            
            

        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "id склада";
            datagrid.Columns[1].Header = "id товара";
            datagrid.Columns[2].Header = "Название склада";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Цена (1 ед)";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Общая стоимость";
        }

        

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                comboboxsklad.Text = dr["namesk"].ToString();
                comboboxtovar.Text = dr["name"].ToString();
                textboxcena.Text = dr["cena"].ToString();
                textboxkol.Text = dr["koltovar"].ToString();
                textboxstoim.Text = dr["obstoim"].ToString();
                labelid.Content = dr["id_tovar"].ToString();
                labelidadd();

            }
        }

        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboboxsklad.Text == "" || comboboxtovar.Text == "" || textboxcena.Text == "" || textboxkol.Text == "" || textboxstoim.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Добавить новый товар на склад?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO sklad VALUES (@id_tovar, @namesk, @koltovar, @obstoim)", sqlCon);

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_tovar", labelid.Content);
                        cmd.Parameters.AddWithValue("@namesk", comboboxsklad.Text);
                        cmd.Parameters.AddWithValue("@koltovar", textboxkol.Text);
                        cmd.Parameters.AddWithValue("@obstoim", textboxstoim.Text);
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        LoadGrid();
                        comboboxsklad.Text = "";
                        comboboxtovar.Text = "";
                        textboxstoim.Clear();
                        textboxcena.Clear();
                        textboxkol.Clear();
                       
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboboxtovar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            

            
        }

        private void comboboxtovar_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT id_tovar FROM tovar WHERE name = @name", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@name", comboboxtovar.Text);
            string id = Convert.ToString(cmd.ExecuteScalar());
            labelid.Content = id;
            sqlCon.Close();
        }

        private void buttonedit_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_sklad"];

                try
                {
                    SqlCommand cmd = new SqlCommand("UPDATE sklad SET id_tovar='" + labelid.Content + "', namesk='" + comboboxsklad.Text + "', koltovar='" + textboxkol.Text + "', obstoim='" + textboxstoim.Text + "' WHERE id_sklad=" + id + ";", sqlCon);
                    SqlCommand cmd2 = new SqlCommand("UPDATE tovar SET cena='" + textboxcena.Text + "' WHERE id_tovar=" + labelid.Content + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    comboboxsklad.Text = "";
                    comboboxtovar.Text = "";
                    textboxstoim.Clear();
                    textboxcena.Clear();
                    textboxkol.Clear();


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
            
        }

        private void buttonsbr_Click(object sender, RoutedEventArgs e)
        {
            comboboxsklad.Text = "";
            comboboxtovar.Text = "";
            textboxstoim.Clear();
            textboxcena.Clear();
            textboxkol.Clear();
            datagrid.SelectedIndex = -1;
        }

        private void btndel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_sklad"];

                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM sklad WHERE id_sklad=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    comboboxsklad.Text = "";
                    comboboxtovar.Text = "";
                    textboxstoim.Clear();
                    textboxcena.Clear();
                    textboxkol.Clear();


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
            if (MessageBox.Show("Вы действительно хотите очистить таблицу?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    {                    
                        sqlCon.Open();
                        SqlCommand cmd = new SqlCommand("TRUNCATE TABLE sklad;", sqlCon);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        LoadGrid();
                        comboboxsklad.Text = "";
                        comboboxtovar.Text = "";
                        textboxstoim.Clear();
                        textboxcena.Clear();
                        textboxkol.Clear();

                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void allspisok_Selected(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void spisok1_Selected(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT id_sklad, tovar.id_tovar, sklad.namesk, tovar.name, tovar.cena, sklad.koltovar, sklad.obstoim FROM sklad, tovar WHERE tovar.id_tovar=sklad.id_tovar and sklad.namesk=@namesk", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@namesk", spisok1.Content);
            DataTable dt = new DataTable();
            sqlCon.Open();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        private void spisok2_Selected(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT id_sklad, tovar.id_tovar, sklad.namesk, tovar.name, tovar.cena, sklad.koltovar, sklad.obstoim FROM sklad, tovar WHERE tovar.id_tovar=sklad.id_tovar and sklad.namesk=@namesk", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@namesk", spisok2.Content);
            DataTable dt = new DataTable();
            sqlCon.Open();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
    }
}
