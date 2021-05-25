using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ShopControlApp
{
    public class AppConfig
    {
        public static JsonParams JsonParameters;

        public async static void Load()
        {
            ResourceDictionary dictionary;
            Uri uri;
            using (FileStream fs = new FileStream("config.json", FileMode.OpenOrCreate))
            {
                JsonParameters = await JsonSerializer.DeserializeAsync<JsonParams>(fs);
            }
            if (JsonParameters.Language == "RU-ru")
            {
                 uri = new Uri(@"Languages\Russian.xaml", UriKind.Relative);
                dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
            else if (JsonParameters.Language == "EN-en")
            {
                uri = new Uri(@"Languages\English.xaml", UriKind.Relative);
                dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }

            switch (JsonParameters.Theme)
            {             
                case "Default":
                    break;
                case "Dark":
                    uri = new Uri(@"Themes\Dark.xaml", UriKind.Relative);
                    dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                case "Light":
                    uri = new Uri(@"Themes\Light.xaml", UriKind.Relative);
                    dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                case "Blue":
                    uri = new Uri(@"Themes\Blue.xaml", UriKind.Relative);
                    dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
                case "Custom":
                    uri = new Uri(@"Themes\Custom.xaml", UriKind.Relative);
                    dictionary = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                    break;
            }
        }
        public async static void SaveChanges()
        {
            using (FileStream fs = new FileStream("config.json", FileMode.Truncate))
            {
                await JsonSerializer.SerializeAsync<JsonParams>(fs,JsonParameters);
            }
        }
        public class JsonParams
        {
            public string Language { get; set; }
            public string Theme { get; set; }

            public JsonParams() { }
        }
    }
}
