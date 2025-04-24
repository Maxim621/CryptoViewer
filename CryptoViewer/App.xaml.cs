using System.Globalization;
using System.Threading;
using System.Windows;

namespace CryptoViewer
{
    public partial class App : Application
    {
        public string CurrentTheme { get; set; } = "LightTheme";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load the default theme (Light)
            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri($"/Themes/{CurrentTheme}.xaml", UriKind.Relative)
            };

            // Add the theme to the application resources
            Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public void ChangeCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
        }
    }
}
