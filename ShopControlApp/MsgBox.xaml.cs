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
    /// Логика взаимодействия для MsgBox.xaml
    /// </summary>
    public partial class MsgBox : Window
    {

        public MsgBox(string title, string article)
        {
            InitializeComponent();
            this.DataContext = new MsgBoxCustom(title,article);
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
