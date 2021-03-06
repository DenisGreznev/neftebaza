using BeautySolutions.View.ViewModel;
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

namespace neft
{
    /// <summary>
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
            
            this.DataContext = itemMenu;
            
        }

      

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        
            try
            {
                
                _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
                _context.zagol.Text = ((SubItem)((ListView)sender).SelectedItem).Name;
                ListViewMenu.UnselectAll();
                
                
                
                
               
       

            }
            catch (Exception ex)
            {
                
            }
        }

        private void ListViewItemMenu_Selected(object sender, RoutedEventArgs e)
        {
            _context.StackPanelMain.Children.Clear();
            _context.zagol.Text = (string)ListViewItemMenu.Content;
            _context.StackPanelMain.Children.Add(new MonitoringView());
            ListViewItemMenu.IsSelected = false;
        }

        private void ExpanderMenu_Expanded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
