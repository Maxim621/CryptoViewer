using CryptoViewer.Helpers; // Importing the helper class ThemeManager
using System;
using System.Windows; // Importing the necessary namespaces for WPF functionality

namespace CryptoViewer.Services
{
    // Static service class for applying themes to the application
    public static class ThemeService
    {
        // Method to apply the selected theme
        public static void ApplyTheme(string themeName)
        {
            // Create a new instance of ResourceDictionary to hold the theme resources
            var dict = new ResourceDictionary();

            // Check the themeName and set the appropriate theme resources
            switch (themeName)
            {
                case "Light":
                    // Set the resource dictionary for Light Theme
                    dict.Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative);
                    // Set the global theme flag to false for Light theme
                    ThemeManager.IsDarkTheme = false;
                    break;
                case "Dark":
                    // Set the resource dictionary for Dark Theme
                    dict.Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative);
                    // Set the global theme flag to true for Dark theme
                    ThemeManager.IsDarkTheme = true;
                    break;
            }

            // Access the global theme flag for Dark/Light theme (not used directly here but can be used elsewhere)
            var isDarkTheme = ThemeManager.IsDarkTheme;

            // Clear all currently merged resource dictionaries
            Application.Current.Resources.MergedDictionaries.Clear();

            // Add the selected theme's resource dictionary to the merged dictionaries
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
