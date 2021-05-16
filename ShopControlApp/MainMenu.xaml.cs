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

namespace ShopControlApp
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>

   #pragma warning disable
    public partial class MainMenu : Window
    {
        private int Position;
        public MainMenu(int position)
        {
            this.Position = position;
            InitializeComponent();          
            switch (Position)
            {
                case 1:
                    this.Title = "Панель продавца";
                    Manufacturers.IsEnabled = false;
                    Warehouses.IsEnabled = false;
                    Sellers.IsEnabled = false;
                    Checks.IsEnabled = false;
                    Goods.IsEnabled = false;
                    GoodsAtWarehouses.IsEnabled = false;
                    break;
                case 2:
                    this.Title = "Панель администратора";
                    Manufacturers.IsEnabled = false;
                    Warehouses.IsEnabled = false;
                    Sellers.IsEnabled = false;
                    break;
                case 3:
                    this.Title = "Панель директора";
                    break;
            }

        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // выход
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e) // создать чек
        {
            DealWindow f = new DealWindow(); f.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // дисконтные карты
        {
            DiscontCards.DiscontCards f = new DiscontCards.DiscontCards(); f.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // товары
        {
            Goods.Goods f = new Goods.Goods(); f.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // товары на складе
        {
            GoodsAtWarehouse.GoodsAtWarehouseSelect f = new GoodsAtWarehouse.GoodsAtWarehouseSelect();
            f.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // чеки
        {
            Checks.Checks f = new Checks.Checks(); f.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) // склады
        {
            Warehouses.Warehouses f = new Warehouses.Warehouses(); f.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) // производители
        {
            Manufacturers.Manufacturers f = new Manufacturers.Manufacturers(); f.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e) // персонал
        {
            Sellers.Sellers f = new Sellers.Sellers(); f.Show();
        }

        private void Settings(object sender, RoutedEventArgs e) // настройки
        {
            Settings.Settings f = new Settings.Settings(); f.Show();
        }
    }
}

