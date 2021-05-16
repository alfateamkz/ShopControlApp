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

namespace ShopControlApp.GoodsAtWarehouse
{
    /// <summary>
    /// Логика взаимодействия для GoodsAtWarehouse.xaml
    /// </summary>
    public partial class GoodsAtWarehouse : Window
    {
        public GoodsAtWarehouse()
        {
            InitializeComponent();
        
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsAtWarehouseInsert f = new GoodsAtWarehouseInsert();
            f.DataContext = this.DataContext;
            f.ShowDialog();
         
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsAtWarehouseDelete f = new GoodsAtWarehouseDelete();
            f.DataContext = this.DataContext;
            f.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            GoodsAtWarehouseUpdate f = new GoodsAtWarehouseUpdate();
            f.DataContext = this.DataContext;
            f.ShowDialog();
        }
    }
}
