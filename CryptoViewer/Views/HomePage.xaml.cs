using CryptoViewer.Models;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Linq;

namespace CryptoViewer.Views
{
    public partial class HomePage : Page
    {
        private CultureInfo _currentCulture;  // Declare a variable for the current culture

        public HomePage()
        {
            InitializeComponent();

            // Load default language resources (English)
            _currentCulture = CultureInfo.CurrentUICulture;  // Initialize
        }

        // Method for handling "Toggle Theme" button click
        private bool _isDarkTheme = false;

        private void OnToggleThemeClick(object sender, RoutedEventArgs e)
        {
            // Load resource for dark or light theme
            var dict = new ResourceDictionary();
            if (_isDarkTheme)
            {
                dict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative); // If dark, load DarkTheme.xaml
            }
            else
            {
                dict.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative); // If light, load LightTheme.xaml
            }

            // Clear and add the new resource
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);

            // Toggle theme state
            _isDarkTheme = !_isDarkTheme;
        }

        // Method for handling cryptocurrency selection
        private void CurrencySelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedCurrency = (sender as ListView)?.SelectedItem as CryptoCurrency;
            if (selectedCurrency != null)
            {
                var detailsPage = new DetailsPage(selectedCurrency);
                var window = Window.GetWindow(this) as MainWindow;
                window?.MainFrame.Navigate(detailsPage);
            }
            else
            {
                MessageBox.Show("Please select a cryptocurrency to view details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method for handling "Change Language" selection
        private void OnLanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LanguageSelector.SelectedItem is ComboBoxItem selectedItem &&
                selectedItem.Tag is string cultureCode)
            {
                ChangeLanguage(cultureCode);
            }
        }

        private void ChangeLanguage(string cultureCode)
        {
            var newCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            var dict = new ResourceDictionary
            {
                Source = new Uri($"Resources/{cultureCode}.xaml", UriKind.Relative)
            };

            // Remove previous localization resource
            var oldDict = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Resources/"));

            if (oldDict != null)
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);

            Application.Current.Resources.MergedDictionaries.Add(dict);

            // Reload the current page
            NavigationService?.Navigate(new HomePage());
        }
    }
}
