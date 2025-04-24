namespace CryptoViewer.Helpers
{
    // The ThemeManager class is a static helper class that manages the application's theme.
    public static class ThemeManager
    {
        // Property to hold the state of the theme.
        // If the value is true, the dark theme is active; otherwise, the light theme is active.
        // The default value is false, meaning the light theme is initially active.
        public static bool IsDarkTheme { get; set; } = false;
    }
}