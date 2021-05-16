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

namespace ShopControlApp.Sellers
{
    /// <summary>
    /// Логика взаимодействия для Sellers.xaml
    /// </summary>
    public partial class Sellers : Window
    {
        public Sellers()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SellersInsert f = new SellersInsert();
            f.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            SellersDelete f = new SellersDelete();
            f.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            SellersUpdate f = new SellersUpdate();
            f.ShowDialog();
        }

        private void birthAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByBirthDay, ApplicationViewModel.FilterParameter.Acs);
        }

        private void birthDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByBirthDay, ApplicationViewModel.FilterParameter.Decs);
        }

        private void empDateAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByEmploymentDate, ApplicationViewModel.FilterParameter.Acs);
        }

        private void empDateDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByEmploymentDate, ApplicationViewModel.FilterParameter.Decs);
        }

        private void nameDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByName, ApplicationViewModel.FilterParameter.Decs);
        }

        private void nameAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByName, ApplicationViewModel.FilterParameter.Acs);
        }

        private void surnameDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.BySurname, ApplicationViewModel.FilterParameter.Decs);
        }

        private void surnameAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.BySurname, ApplicationViewModel.FilterParameter.Acs);
        }

        private void patronymicDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByPatronymic, ApplicationViewModel.FilterParameter.Decs);
        }

        private void patronymicAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByPatronymic, ApplicationViewModel.FilterParameter.Acs);
        }

        private void PositionDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByPosition, ApplicationViewModel.FilterParameter.Decs);
        }

        private void PositionAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.Sellers,
ApplicationViewModel.FilterAction.ByPosition, ApplicationViewModel.FilterParameter.Acs);
        }
    }
}
