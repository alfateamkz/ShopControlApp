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

namespace ShopControlApp.DiscontCards
{
    /// <summary>
    /// Логика взаимодействия для DiscontCards.xaml
    /// </summary>
    public partial class DiscontCards : Window
    {
        public DiscontCards()
        {
            InitializeComponent();
            this.DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards);
        }

        private void DCInsert(object sender, RoutedEventArgs e)
        {
            this.Close();
            DiscontCardsInsert f = new DiscontCardsInsert(); f.Show();
        }

        private void DCDelete(object sender, RoutedEventArgs e)
        {
            this.Close();
            DiscontCardsDelete f = new DiscontCardsDelete(); f.Show();
        }

        private void DCUpdate(object sender, RoutedEventArgs e)
        {
            this.Close();
            DiscontCardsUpdate f = new DiscontCardsUpdate();  f.Show();
        }

        private void percentageAcs(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards,
ApplicationViewModel.FilterAction.ByPercent, ApplicationViewModel.FilterParameter.Acs);
        }

        private void percentageDecs(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards,
ApplicationViewModel.FilterAction.ByPercent, ApplicationViewModel.FilterParameter.Decs);
        }

        private void SumAsc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards,
ApplicationViewModel.FilterAction.BySum, ApplicationViewModel.FilterParameter.Acs);
        }

        private void SumDesc(object sender, RoutedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards,
ApplicationViewModel.FilterAction.BySum, ApplicationViewModel.FilterParameter.Decs);
        }

        private void searchBySumAsc(object sender, TextChangedEventArgs e)
        {
            DataContext = new ApplicationViewModel(ApplicationViewModel.Tables.DiscontCards,
ApplicationViewModel.FilterAction.BySum, ApplicationViewModel.FilterParameter.Acs);
        }
    }
}
