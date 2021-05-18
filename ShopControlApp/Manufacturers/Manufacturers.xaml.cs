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

namespace ShopControlApp.Manufacturers
{
    /// <summary>
    /// Логика взаимодействия для Manufacturers.xaml
    /// </summary>
    public partial class Manufacturers : Window
    {
        public Manufacturers()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ManufacturersInsert f = new ManufacturersInsert();
            f.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            ManufacturersDelete f = new ManufacturersDelete();
            f.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            ManufacturersUpdate f = new ManufacturersUpdate();
            f.ShowDialog();
        }
        /// <summary>
        /// Обновление контекста для сортировки
        /// </summary>
        private void TitleAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Acs);
        }

        private void TitleDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Decs);
        }

        private void CountryDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByCountry, ApplicationViewModel.FilterParameter.Acs);
        }

        private void CountryAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByCountry, ApplicationViewModel.FilterParameter.Null);
        }

        /// <summary>
        /// Обновление контекста для поиска в БД
        /// </summary>
        private void searchByTitle(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Null);
        }

        private void searchByCountry(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Manufacturers,
ApplicationViewModel.FilterAction.ByCountry, ApplicationViewModel.FilterParameter.Null);
        }
    }
}
