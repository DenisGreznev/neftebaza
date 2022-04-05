using System;
using System.Collections.Generic;
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
using BeautySolutions.View.ViewModel;
using MaterialDesignThemes.Wpf;

namespace neft
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       
        public MainWindow()
        {
            InitializeComponent();

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Движение товара"));
            menuRegister.Add(new SubItem("Список клиентов"));
            var item6 = new ItemMenu("Доставка", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Товары на складе", new tovarsinsklad()));
            menuSchedule.Add(new SubItem("Список складов"));
            var item1 = new ItemMenu("Склады", menuSchedule, PackIconKind.Shop);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Список товаров"));
            menuReports.Add(new SubItem("Списанные товары"));
            var item2 = new ItemMenu("Товары", menuReports, PackIconKind.ShoppingBasket);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Список заказов"));
            menuExpenses.Add(new SubItem("Выполненные заказы"));
            var item3 = new ItemMenu("Заказы", menuExpenses, PackIconKind.FileReport);

            var menuFinancial = new List<SubItem>();
            menuFinancial.Add(new SubItem("Учет продаж", new MonitoringView()));
            var item4 = new ItemMenu("Продажи", menuFinancial, PackIconKind.AccountBalance);

            var item0 = new ItemMenu("Мониторинг", new MonitoringView(), PackIconKind.Monitor);


            var menuSotr = new List<SubItem>();
            menuSotr.Add(new SubItem("Список сотрудников"));
            var item7 = new ItemMenu("Сотрудники", menuSotr, PackIconKind.PeopleCheck);

            string nam = item0.Header;
           

            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item2, this));
            Menu.Children.Add(new UserControlMenuItem(item3, this));
            Menu.Children.Add(new UserControlMenuItem(item4, this));
            Menu.Children.Add(new UserControlMenuItem(item6, this));
            Menu.Children.Add(new UserControlMenuItem(item7, this));


        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
                
            }
        }

        internal void SwitchText(object sender)
        {
            var text = ((TextBlock)sender);

            if (text != null)
            {
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void akk_Click(object sender, RoutedEventArgs e)
        {
            string name = txtEmail.Text;
            account accountboard = new account();
            accountboard.labelemail.Content = name;
            accountboard.Show();



        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
                
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Environment.Exit(0);
        }

        private void StackPanelMain_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
