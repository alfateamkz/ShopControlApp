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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Interactivity;

namespace ShopControlApp.Checks
{
    /// <summary>
    /// Логика взаимодействия для Checks.xaml
    /// </summary>
    public partial class Checks : Window
    {
        public Checks()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Checks);

        }
        /// <summary>
        /// Обновление контекста для сортировки
        /// </summary>
        private void dateAcs(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Checks,
                ApplicationViewModel.FilterAction.ByDate,ApplicationViewModel.FilterParameter.Acs);
        }

        private void dateDecs(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Checks,
       ApplicationViewModel.FilterAction.ByDate, ApplicationViewModel.FilterParameter.Decs);
        }
        /// <summary>
        /// Обновление контекста для поиска в БД
        /// </summary>
        private void searchByDate(object sender, SelectionChangedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Checks,
      ApplicationViewModel.FilterAction.ByDate, ApplicationViewModel.FilterParameter.Null);
        }
    }
}
