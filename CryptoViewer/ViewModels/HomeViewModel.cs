using CommunityToolkit.Mvvm.ComponentModel; // For ObservableObject and ObservableProperty attributes
using CryptoViewer.Models; // For CryptoCurrency model
using CryptoViewer.Services; // For CoinGeckoService to fetch cryptocurrency data
using System.Collections.ObjectModel; // For ObservableCollection, which supports data binding in WPF

namespace CryptoViewer.ViewModels
{
    // The HomeViewModel class manages the logic and data for the home screen of the app
    public partial class HomeViewModel : ObservableObject
    {
        // Create an instance of CoinGeckoService to fetch cryptocurrency data
        private readonly CoinGeckoService _cryptoService = new();

        // ObservableProperty automatically generates the backing fields and PropertyChanged notifications
        [ObservableProperty]
        private string searchQuery;

        [ObservableProperty]
        private CryptoCurrency selectedCurrency;

        // Observable collections for holding all cryptocurrencies and the filtered ones based on search query
        public ObservableCollection<CryptoCurrency> AllCryptocurrencies { get; } = new();
        public ObservableCollection<CryptoCurrency> FilteredCryptocurrencies { get; } = new();

        // Constructor which loads data asynchronously when the ViewModel is created
        public HomeViewModel()
        {
            _ = LoadDataAsync();
        }

        // This method is triggered when the search query changes and calls ApplySearchFilter
        partial void OnSearchQueryChanged(string value)
        {
            ApplySearchFilter(value); // Apply the search filter whenever the search query changes
        }

        // A method that forces a refresh of the filtered list based on the current search query
        public void ApplySearch()
        {
            // Trigger the search query to reapply the filter
            SearchQuery = SearchQuery; // This will invoke the OnSearchQueryChanged method
        }

        // Private method that applies the filtering based on the search query
        private void ApplySearchFilter(string value)
        {
            // Clear the filtered list before adding the new filtered items
            FilteredCryptocurrencies.Clear();

            // If search query is empty, show all cryptocurrencies, otherwise filter them
            var filtered = string.IsNullOrWhiteSpace(value)
                ? AllCryptocurrencies // If the search query is empty, show all items
                : AllCryptocurrencies.Where(c =>
                    c.Name.Contains(value, StringComparison.OrdinalIgnoreCase) || // Match by name
                    c.Symbol.Contains(value, StringComparison.OrdinalIgnoreCase)); // Match by symbol

            // Add filtered items to the filtered list
            foreach (var item in filtered)
            {
                FilteredCryptocurrencies.Add(item);
            }
        }

        // Asynchronous method to load the list of cryptocurrencies from the CoinGecko API
        private async Task LoadDataAsync()
        {
            // Fetch the top cryptocurrencies from the CoinGecko API
            var currencies = await _cryptoService.GetTopCurrenciesAsync();

            // Clear the existing list and populate it with the new data
            AllCryptocurrencies.Clear();
            foreach (var c in currencies)
                AllCryptocurrencies.Add(c);

            // Apply the current search filter to update the filtered list
            ApplySearchFilter(SearchQuery);
        }

        // Method to select a cryptocurrency, called when a user selects an item from the list
        public void SelectCurrency(CryptoCurrency currency)
        {
            SelectedCurrency = currency; // Set the selected cryptocurrency
        }
    }
}
