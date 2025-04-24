using System;
using System.Windows.Markup;
using System.Windows;

namespace CryptoViewer.Localization
{
    // Define the Loc class, which inherits from MarkupExtension to create a custom markup extension for localization.
    public class Loc : MarkupExtension
    {
        // Property to hold the key for the resource to be fetched.
        public string Key { get; set; }

        // Constructor that accepts the key for the resource.
        public Loc(string key)
        {
            Key = key;
        }

        // This method is called by the XAML parser to retrieve the localized value.
        // It overrides the ProvideValue method of MarkupExtension.
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            // Try to find the resource by the provided key.
            // If the resource is found, return its value.
            // If the resource is not found, return the key surrounded by square brackets (to indicate missing resource).
            return Application.Current.TryFindResource(Key) ?? $"[{Key}]";
        }
    }
}