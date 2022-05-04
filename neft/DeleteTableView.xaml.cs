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
    /// Логика взаимодействия для DeleteTableView.xaml
    /// </summary>
    public partial class DeleteTableView : Window
    {
        public DeleteTableView()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

       

        private void buttonsbros_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Вы действительно хотите очистить таблицу?", "Подтверждение действия", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    {
                        sqlCon.Open();

                        SqlCommand cmd = new SqlCommand("TRUNCATE TABLE sklad", sqlCon);
                        
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                        tovarsinsklad tovar = new tovarsinsklad();
                        WindowDelete.Close();
                        tovar.LoadGrid();
                        tovar.comboboxsklad.Text = "";
                        tovar.comboboxtovar.Text = "";
                        tovar.textboxstoim.Clear();
                        tovar.textboxcena.Clear();
                        tovar.textboxkol.Clear();


                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textboxpassword_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
          
        }

        private void Window_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void textboxpassword_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void textboxpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (textboxpassword.Password == "clear")
            {
                buttonsbros.Visibility = Visibility.Visible;
            }
            else buttonsbros.Visibility = Visibility.Collapsed;
        }
    }
}
