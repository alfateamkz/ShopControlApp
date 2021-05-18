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

namespace ShopControlApp.Warehouses
{
    /// <summary>
    /// Логика взаимодействия для Warehouses.xaml
    /// </summary>
    public partial class Warehouses : Window
    {
        public Warehouses()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Warehouses);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            WarehousesInsert f = new WarehousesInsert();
            f.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            WarehousesDelete f = new WarehousesDelete();
            f.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            WarehousesUpdate f = new WarehousesUpdate();
            f.ShowDialog();
        }
        /// <summary>
        /// Обновление контекста для сортировки
        /// </summary>
        private void titleAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Warehouses,
ApplicationViewModel.FilterAction.ByAddress, ApplicationViewModel.FilterParameter.Acs);
        }

        private void titleDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Warehouses,
ApplicationViewModel.FilterAction.ByAddress, ApplicationViewModel.FilterParameter.Decs);
        }



        /// <summary>
        /// Обновление контекста для поиска в БД
        /// </summary>
        /// 
        private void searchByAddress(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Warehouses,
ApplicationViewModel.FilterAction.ByAddress, ApplicationViewModel.FilterParameter.Null);
        }
    }
}
