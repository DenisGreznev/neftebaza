using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM users WHERE email=@email AND password=@password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@email", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@password", txtPassword.Password);
                int coun = Convert.ToInt32(sqlCmd.ExecuteScalar());
            
                if (coun == 1)
                {
                    
                    String queryfam = "SELECT fam FROM users WHERE email=@email";
                    String queryname = "SELECT name FROM users WHERE email=@email";
                    String queryemail = "SELECT email FROM users WHERE email=@email";
                   



                    SqlCommand sqlCmdfam = new SqlCommand(queryfam, sqlCon);
                    SqlCommand sqlCmdname = new SqlCommand(queryname, sqlCon);
                    SqlCommand sqlCmdemail = new SqlCommand(queryemail, sqlCon);
                    



                    sqlCmdfam.CommandType = System.Data.CommandType.Text;
                    sqlCmdfam.Parameters.AddWithValue("@email", txtUsername.Text);
                    sqlCmdname.CommandType = System.Data.CommandType.Text;
                    sqlCmdname.Parameters.AddWithValue("@email", txtUsername.Text);
                
                    sqlCmdemail.CommandType = System.Data.CommandType.Text;
                    sqlCmdemail.Parameters.AddWithValue("@email", txtUsername.Text);
 
                    MainWindow dashboard = new MainWindow();
                    string fam = Convert.ToString(sqlCmdfam.ExecuteScalar());
                    string name = Convert.ToString(sqlCmdname.ExecuteScalar());
                    string email = Convert.ToString(sqlCmdemail.ExecuteScalar());

                    dashboard.Show();
                    dashboard.txtUser.Text = fam +" "+name;
                    dashboard.txtEmail.Text = email; 
                    this.Close();
                    sqlCon.Close();



                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
        }

       

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            stacklogin.Visibility = Visibility.Collapsed;
            stackregister.Visibility = Visibility.Visible;
        }

        private void btnRegister2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
            try
            {
                if (txtUsername1.Text == "" || txtPassword2.Password == "" || txtPassword3.Password == "" || txtNames.Text == "" || txtFams.Text == "" || txtOtchs.Text == "") MessageBox.Show("Все поля должны быть заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (txtPassword2.Password == txtPassword3.Password)
                    {
                        if (sqlCon.State == System.Data.ConnectionState.Closed)
                            sqlCon.Open();
                        String query1 = "SELECT COUNT(1) FROM users WHERE email=@email";
                        SqlCommand sqlCmd = new SqlCommand(query1, sqlCon);
                        sqlCmd.CommandType = System.Data.CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@email", txtUsername1.Text);
                        int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Такой пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        } else
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO users VALUES (@email, @password, @name, @fam, @otch, null, null, null, null)", sqlCon);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@email", txtUsername1.Text);
                            cmd.Parameters.AddWithValue("@password", txtPassword2.Password);
                            cmd.Parameters.AddWithValue("@name", txtNames.Text);
                            cmd.Parameters.AddWithValue("@fam", txtFams.Text);
                            cmd.Parameters.AddWithValue("@otch", txtOtchs.Text);
                            sqlCon.Open();
                            cmd.ExecuteNonQuery();
                            sqlCon.Close();
                            MessageBox.Show("Регистрация завершена, пройдите авторизацию.", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                            stackregister.Visibility = Visibility.Hidden;
                            stacklogin.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают, повторите попытку.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        txtPassword2.Password = "";
                        txtPassword3.Password = "";
                    }
                   
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            stackregister.Visibility = Visibility.Hidden;
            stacklogin.Visibility = Visibility.Visible;
            
        }
    }
    }

