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
    /// Логика взаимодействия для GoodsAtWarehouseUpdate.xaml
    /// </summary>
    public partial class GoodsAtWarehouseUpdate : Window
    {
        public GoodsAtWarehouseUpdate()
        {
            InitializeComponent();
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            GoodsAtWarehouse f = new GoodsAtWarehouse(); f.Show();
        }
    }
}
