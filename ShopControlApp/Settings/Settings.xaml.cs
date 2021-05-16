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
using System.Reflection;
using System.Collections.ObjectModel;

namespace ShopControlApp.Settings
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        class colorParameter
        {
            public colorParameter(SolidColorBrush brush, string title)
            {
                this.Brush = brush;
                this.Title = title;
            }
            public SolidColorBrush Brush { get; set; }
            public string Title { get; set; }

            public override string ToString()
            {
                return Title;

            }
        }
        ObservableCollection<colorParameter> brushes;
        public Settings()
        {
            InitializeComponent();
            brushes = new ObservableCollection<colorParameter>();
           var brushesVar = typeof(Brushes).GetProperties()
                .Select(p => new { Name = p.Name, Brush = p.GetValue(null) as SolidColorBrush }).ToArray();
            foreach (var i in brushesVar)
            {
                brushes.Add(new colorParameter(i.Brush, i.Name));
            }



            buttonTextCB.ItemsSource = brushes;
            buttonBgCB.ItemsSource = brushes;
            BgCB.ItemsSource = brushes;
            TextCB.ItemsSource = brushes;
            BorderCB.ItemsSource = brushes;
        }

        private void ThemeLight(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes\Light.xaml", UriKind.Relative);
            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);

        }

        private void ThemeDark(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes\Dark.xaml", UriKind.Relative);
            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        private void ThemeBlue(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"Themes\Blue.xaml", UriKind.Relative);
            ResourceDictionary dictionary = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                ResourceDictionary dictionary = new ResourceDictionary();
                Style btnStyle = new Style();
                colorParameter buttonTextColor = (colorParameter)buttonTextCB.SelectedItem;
                colorParameter buttonBgColor= (colorParameter)buttonTextCB.SelectedItem;
                colorParameter BgColor = (colorParameter)BgCB.SelectedItem;
                colorParameter TextColor = (colorParameter)TextCB.SelectedItem;
                colorParameter BorderColor = (colorParameter)BorderCB.SelectedItem;

                btnStyle.Setters.Add(new Setter { Property = Button.ForegroundProperty, Value = buttonTextColor?.Brush });
                btnStyle.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = buttonBgColor?.Brush});
                dictionary.Add("ButtonStyle", btnStyle);
                Style MenuItemStyle = new Style();
                MenuItemStyle.Setters.Add(new Setter { Property = MenuItem.ForegroundProperty, Value = buttonTextColor?.Brush });
                MenuItemStyle.Setters.Add(new Setter { Property = MenuItem.BackgroundProperty, Value = buttonBgColor?.Brush });
                dictionary.Add("MenuItemStyle", MenuItemStyle);
                Style GridBackground = new Style();
                GridBackground.Setters.Add(new Setter { Property = Grid.BackgroundProperty, Value = BgColor?.Brush});
                dictionary.Add("GridBackground", GridBackground);
                Style DGBackground = new Style();
                DGBackground.Setters.Add(new Setter { Property = DataGrid.BackgroundProperty, Value = BgColor?.Brush });
                dictionary.Add("DGBackground", DGBackground);
                Style TextColorStyle = new Style();
                TextColorStyle.Setters.Add(new Setter { Property = TextBox.ForegroundProperty, Value = TextColor?.Brush});
                dictionary.Add("TextColor", TextColorStyle);
                Style WindowColor = new Style();
                WindowColor.Setters.Add(new Setter { Property = Window.BackgroundProperty, Value = BorderColor?.Brush });
                dictionary.Add("WindowColor", WindowColor);
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
            catch
            {
                MsgBox f = new MsgBox("Ошибка", "Не во всех полях выбран цвет"); f.ShowDialog();
            }

        }
    }
}
