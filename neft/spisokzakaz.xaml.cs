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
    /// Логика взаимодействия для spisokzakaz.xaml
    /// </summary>
    public partial class spisokzakaz : UserControl
    {
        public spisokzakaz()
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
                SqlCommand cmd = new SqlCommand("SELECT zakaz.status, zakaz.id_zakaz, klient.namekl, tovar.name, zakaz.datezak, zakaz.koltovar, zakaz.summa, klient.adres, klient.telefon FROM zakaz,tovar,klient WHERE tovar.id_tovar=zakaz.id_tovar and klient.id_klient=zakaz.id_klient;", sqlCon);
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
                SqlCommand cmd = new SqlCommand("SELECT id_tovar FROM tovar WHERE name = @name", sqlCon);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@name", comboboxtovar.Text);
                string id = Convert.ToString(cmd.ExecuteScalar());
                labelid.Content = id;
                SqlCommand cmd2 = new SqlCommand("SELECT id_klient FROM klient WHERE namekl = @namekl", sqlCon);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.Parameters.AddWithValue("@namekl", textboxklient.Text);
                string id2 = Convert.ToString(cmd2.ExecuteScalar());
                labelidkl.Content = id2;
                SqlCommand cmd3 = new SqlCommand("SELECT cena FROM tovar WHERE name = @name", sqlCon);
                cmd3.CommandType = System.Data.CommandType.Text;
                cmd3.Parameters.AddWithValue("@name", comboboxtovar.Text);
                string cena = Convert.ToString(cmd3.ExecuteScalar());
                labelcena.Text = cena;
                sqlCon.Close();
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
                }
                sqlCon.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }
        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                textboxkoltovar.Clear();
                textboxsumma.Clear();
                comboboxstatus.Text = dr["status"].ToString();
                textboxklient.Text = dr["namekl"].ToString();
                comboboxtovar.Text = dr["name"].ToString();
                textboxdatezak.Text = dr["datezak"].ToString();
                textboxkoltovar.Text = dr["koltovar"].ToString();
                textboxsumma.Text = dr["summa"].ToString();
                textboxadres.Text = dr["adres"].ToString();
                textboxtelefon.Text = dr["telefon"].ToString();

            }
        }
        private void buttondob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboboxstatus.Text == "" || textboxklient.Text == "" || comboboxtovar.Text == "" || textboxdatezak.Text == "" || textboxkoltovar.Text =="" || textboxsumma.Text == "" || textboxadres.Text == "" || textboxtelefon.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (MessageBox.Show("Создать новый заказ?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        sqlCon.Open();
                        SqlCommand cmdKl = new SqlCommand("INSERT INTO klient VALUES (@namekl, @adres, @telefon)", sqlCon);
                        cmdKl.CommandType = CommandType.Text;
                        cmdKl.Parameters.AddWithValue("@namekl", textboxklient.Text);
                        cmdKl.Parameters.AddWithValue("@adres", textboxadres.Text);
                        cmdKl.Parameters.AddWithValue("@telefon", textboxtelefon.Text);
                        cmdKl.ExecuteNonQuery();
                        labelidadd();
                        SqlCommand cmd = new SqlCommand("INSERT INTO zakaz VALUES (@id_tovar, @id_klient, @datezak, @koltovar, @summa, @status)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_tovar", labelid.Content);
                        cmd.Parameters.AddWithValue("@id_klient", labelidkl.Content);
                        cmd.Parameters.AddWithValue("@datezak", textboxdatezak.Text);
                        cmd.Parameters.AddWithValue("@koltovar", textboxkoltovar.Text);
                        cmd.Parameters.AddWithValue("@summa", textboxsumma.Text);
                        cmd.Parameters.AddWithValue("@status", comboboxstatus.Text);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        LoadGrid();
                        comboboxstatus.Text = "";
                        comboboxtovar.Text = "";
                        textboxklient.Clear();
                        textboxadres.Clear();
                        textboxkoltovar.Clear();
                        textboxsumma.Clear();
                        textboxtelefon.Clear();
                        textboxdatezak.Text = "";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT id_tovar FROM tovar WHERE name = @name", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@name", comboboxtovar.Text);
            string id = Convert.ToString(cmd.ExecuteScalar());
            labelid.Content = id;
            SqlCommand cmd2 = new SqlCommand("SELECT id_klient FROM klient WHERE namekl = @namekl", sqlCon);
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.Parameters.AddWithValue("@namekl", textboxklient.Text);
            string id2 = Convert.ToString(cmd2.ExecuteScalar());
            labelidkl.Content = id2;
            SqlCommand cmd3 = new SqlCommand("SELECT cena FROM tovar WHERE name = @name", sqlCon);
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.Parameters.AddWithValue("@name", comboboxtovar.Text);
            string cena = Convert.ToString(cmd3.ExecuteScalar());
            labelcena.Text = cena;
            sqlCon.Close();
        }

        private void buttonedit_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_zakaz"];
                try
                {

                    SqlCommand cmd2 = new SqlCommand("UPDATE zakaz SET id_tovar='" + labelid.Content + "', id_klient='" + labelidkl.Content + "', datezak='" + textboxdatezak.Text + "', koltovar='"+textboxkoltovar.Text+"', summa='"+textboxsumma.Text+"', status='"+comboboxstatus.Text+"' WHERE id_zakaz=" +id+";", sqlCon);
                    SqlCommand cmd3 = new SqlCommand("UPDATE klient SET namekl='" + textboxklient.Text + "', adres='" + textboxadres.Text + "', telefon='" + textboxtelefon.Text + "' WHERE id_klient=" + labelidkl.Content + ";", sqlCon);
                    sqlCon.Open();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно обновлена", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    comboboxstatus.Text = "";
                    comboboxtovar.Text = "";
                    textboxklient.Clear();
                    textboxadres.Clear();
                    textboxkoltovar.Clear();
                    textboxsumma.Clear();
                    textboxtelefon.Clear();
                    textboxdatezak.Text = "";

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
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }

        private void buttonsbr_Click(object sender, RoutedEventArgs e)
        {
            comboboxstatus.Text = "";
            comboboxtovar.Text = "";
            textboxklient.Clear();
            textboxadres.Clear();
            textboxkoltovar.Clear();
            textboxdatezak.Text = "";
            textboxsumma.Clear();
            textboxtelefon.Clear();
            datagrid.SelectedIndex = -1;
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }

        private void btndel_Click(object sender, RoutedEventArgs e)
        {
            if (datagrid.SelectedCells.Count > 0)
            {
                DataRowView row = (DataRowView)datagrid.SelectedItems[0];
                int id = (int)row["id_zakaz"];

                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM zakaz WHERE id_zakaz=" + id + ";", sqlCon);
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlCon.Close();
                    LoadGrid();
                    comboboxstatus.Text = "";
                    comboboxtovar.Text = "";
                    textboxklient.Clear();
                    textboxadres.Clear();
                    textboxdatezak.Text = "";
                    textboxkoltovar.Clear();
                    textboxsumma.Clear();
                    textboxtelefon.Clear();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else MessageBox.Show("Чтобы удалить запись, нужно выбрать ее в таблице", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT zakaz.status, zakaz.id_zakaz, klient.namekl, tovar.name, zakaz.datezak, zakaz.koltovar, zakaz.summa, klient.adres, klient.telefon FROM zakaz,tovar,klient WHERE tovar.id_tovar=zakaz.id_tovar and klient.id_klient=zakaz.id_klient and tovar.name LIKE '%" + textboxtovarpoisk.Text + "%'", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();
            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }



        private void textboxcena_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }


        private void textboxkoltovar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textboxkoltovar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void btndeltab_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите очистить таблицу?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    {
                        sqlCon.Open();

                        SqlCommand cmd = new SqlCommand("TRUNCATE TABLE zakaz", sqlCon);
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonpod_Click(object sender, RoutedEventArgs e)
        {

            if (textboxkoltovar.Text == "")
            {
                textboxsumma.Text = "";
            }
            else
            {
                int cena1 = Convert.ToInt32(labelcena.Text.ToString());
                int kol = Convert.ToInt32(textboxkoltovar.Text.ToString());
                textboxsumma.Text = Convert.ToString(cena1 * kol);
            }

        }

        private void textboxtelefon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void buttonsort_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("SELECT zakaz.status, zakaz.id_zakaz, klient.namekl, tovar.name, zakaz.datezak, zakaz.koltovar, zakaz.summa, klient.adres, klient.telefon FROM zakaz,tovar,klient WHERE tovar.id_tovar=zakaz.id_tovar and klient.id_klient=zakaz.id_klient and zakaz.status=@status", sqlCon);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@status", comboboxsort.Text);
            DataTable dt = new DataTable();

            SqlDataReader srd = cmd.ExecuteReader();
            dt.Load(srd);
            sqlCon.Close();

            datagrid.ItemsSource = dt.DefaultView;
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }

        private void buttonsbrossort_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            comboboxsort.Text = "";
            datagrid.Columns[0].Header = "Статус";
            datagrid.Columns[1].Header = "id заказа";
            datagrid.Columns[2].Header = "Клиент";
            datagrid.Columns[3].Header = "Товар";
            datagrid.Columns[4].Header = "Дата заказа";
            datagrid.Columns[5].Header = "Количество";
            datagrid.Columns[6].Header = "Сумма";
            datagrid.Columns[7].Header = "Адрес";
            datagrid.Columns[8].Header = "Телефон";
        }
    }
}
