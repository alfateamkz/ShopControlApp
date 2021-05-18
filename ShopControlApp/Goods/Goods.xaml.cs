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

namespace ShopControlApp.Goods
{
    /// <summary>
    /// Логика взаимодействия для Goods.xaml
    /// </summary>
    public partial class Goods : Window
    {
        public Goods()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods);
        }

        private void GoodsInsert(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsInsert f = new GoodsInsert(); f.ShowDialog();
        }

        private void GoodsDelete(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsDelete f = new GoodsDelete(); f.ShowDialog();
        }

        private void GoodsUpdate(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsUpdate f = new GoodsUpdate(); f.ShowDialog();
        }

        /// <summary>
        /// Обновление контекста для сортировки
        /// </summary>
        private void TitleAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Acs);
        }

        private void TitleDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Decs);
        }

        private void WarrantyDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByWarranty, ApplicationViewModel.FilterParameter.Decs);
        }

        private void WarrantyAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByWarranty, ApplicationViewModel.FilterParameter.Acs);
        }

        private void PriceDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByPrice, ApplicationViewModel.FilterParameter.Decs);
        }

        private void PriceAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByPrice, ApplicationViewModel.FilterParameter.Acs);
        }


        /// <summary>
        /// Обновление контекста для поиска в БД
        /// </summary>
        private void searchByPriceAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByPrice, ApplicationViewModel.FilterParameter.Acs);
        }

        private void searchByPriceDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByPrice, ApplicationViewModel.FilterParameter.Decs);
        }

        private void searchByTitle(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByTitle, ApplicationViewModel.FilterParameter.Null);
        }

        private void searchByWarranty(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Goods,
ApplicationViewModel.FilterAction.ByWarranty, ApplicationViewModel.FilterParameter.Null);
        }
    }
}
