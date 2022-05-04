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
    /// Логика взаимодействия для MonitoringView.xaml
    /// </summary>
    public partial class MonitoringView : UserControl
    {
        
        public MonitoringView()
        {
            InitializeComponent();
            Consumo consumo = new Consumo();
            DataContext = new ConsumoViewModel(consumo);

        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
       

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(zakaz.summa) FROM zakaz", sqlCon);
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM klient", sqlCon);
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM sklad", sqlCon);
                SqlCommand cm1 = new SqlCommand("SELECT COUNT(*) FROM zakaz", sqlCon);
                SqlCommand cm2 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Подготовка'", sqlCon);
                SqlCommand cm3 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Отправлен'", sqlCon);
                SqlCommand cm4 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Доставлен'", sqlCon);
                SqlCommand cm5 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Завершен'", sqlCon);

                string scm1 = Convert.ToString(cm1.ExecuteScalar());
                string scm2 = Convert.ToString(cm2.ExecuteScalar());
                string scm3 = Convert.ToString(cm3.ExecuteScalar());
                string scm4 = Convert.ToString(cm4.ExecuteScalar());
                string scm5 = Convert.ToString(cm5.ExecuteScalar());

                z1.Text = scm1;
                z2.Text = scm2;
                z3.Text = scm3;
                z4.Text = scm4;
                z5.Text = scm5;


                string sum = Convert.ToString(cmd.ExecuteScalar());
                string kol = Convert.ToString(cmd2.ExecuteScalar());
                string koltovar = Convert.ToString(cmd3.ExecuteScalar());
                
                textmoney.Text = sum;
                textklient.Text = kol;
                texttovar.Text = koltovar;
                txt1.Text = sum;

               
                int prog = Convert.ToInt32(textmoney.Text.ToString());
                progress.Value = prog;

                sqlCon.Close();



            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        internal class ConsumoViewModel
        {
            public List<Consumo> Consumo { get; private set; }

            public ConsumoViewModel(Consumo consumo)
            {
                Consumo = new List<Consumo>();
                Consumo.Add(consumo);
            }
        }

        internal class Consumo
        {
            public string Titulo { get; private set; }
            public int Porcentagem { get; private set; }

            public float proc;
            public int proc2;

            public Consumo()
            {
                Titulo = "Заказы";
                
                SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=neftebaza; Integrated Security=True;");
                try
                {
                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("SELECT SUM(zakaz.summa) FROM zakaz", sqlCon);
                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM klient", sqlCon);
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM sklad", sqlCon);


                    SqlCommand cmdyes = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Завершен'", sqlCon);
                    SqlCommand cmdne = new SqlCommand("SELECT COUNT(*) FROM zakaz", sqlCon);
                    string yes = Convert.ToString(cmdyes.ExecuteScalar());
                    string no = Convert.ToString(cmdne.ExecuteScalar());

                    int yes1 = Convert.ToInt32(yes);
                    int no2 = Convert.ToInt32(no);

                    float yes2 = Convert.ToInt32(yes1);
                    float no3 = Convert.ToInt32(no2);

                     proc = (yes2 / no3) * 100;
                    proc2 = Convert.ToInt32(proc);
                    Porcentagem = proc2;





                    sqlCon.Close();



                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }



            }
            

            public int CalcularPorcentagem()
            {

                return proc2; //Calculo da porcentagem de consumo

            }
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(zakaz.summa) FROM zakaz", sqlCon);
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM klient", sqlCon);
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM sklad", sqlCon);
                SqlCommand cm1 = new SqlCommand("SELECT COUNT(*) FROM zakaz", sqlCon);
                SqlCommand cm2 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Подготовка'", sqlCon);
                SqlCommand cm3 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Отправлен'", sqlCon);
                SqlCommand cm4 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Доставлен'", sqlCon);
                SqlCommand cm5 = new SqlCommand("SELECT COUNT(*) FROM zakaz WHERE status='Завершен'", sqlCon);

                string scm1 = Convert.ToString(cm1.ExecuteScalar());
                string scm2 = Convert.ToString(cm2.ExecuteScalar());
                string scm3 = Convert.ToString(cm3.ExecuteScalar());
                string scm4 = Convert.ToString(cm4.ExecuteScalar());
                string scm5 = Convert.ToString(cm5.ExecuteScalar());

                z1.Text = scm1;
                z2.Text = scm2;
                z3.Text = scm3;
                z4.Text = scm4;
                z5.Text = scm5;


                string sum = Convert.ToString(cmd.ExecuteScalar());
                string kol = Convert.ToString(cmd2.ExecuteScalar());
                string koltovar = Convert.ToString(cmd3.ExecuteScalar());

                textmoney.Text = sum;
                textklient.Text = kol;
                texttovar.Text = koltovar;
                txt1.Text = sum;


                int prog = Convert.ToInt32(textmoney.Text.ToString());
                progress.Value = prog;

                sqlCon.Close();



            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

