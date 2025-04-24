using System.Net.Http;
using System.Text.Json;
using CryptoViewer.Models;

namespace CryptoViewer.Services
{
    // Service class for interacting with CoinGecko API
    public class CoinGeckoService
    {
        // The HttpClient instance for making HTTP requests
        private readonly HttpClient _http;

        // Constructor to initialize HttpClient and set custom headers
        public CoinGeckoService()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("User-Agent", "CryptoViewerApp/1.0"); // Adding a User-Agent header
        }

        // Method to get top cryptocurrencies based on market capitalization
        public async Task<List<CryptoCurrency>> GetTopCurrenciesAsync(int count = 10)
        {
            // Construct the API URL to fetch the top cryptocurrencies in USD, ordered by market cap
            var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1&sparkline=false";

            // Make an asynchronous HTTP GET request to the CoinGecko API
            var response = await _http.GetAsync(url);

            // If the response is not successful, log the error and return an empty list
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"Request to CoinGecko failed. StatusCode: {response.StatusCode}");
                return new List<CryptoCurrency>();
            }

            // Read the response content as a stream for better memory usage with large responses
            using var stream = await response.Content.ReadAsStreamAsync();

            // Parse the response JSON
            var json = await JsonDocument.ParseAsync(stream);

            // Initialize a list to hold the parsed cryptocurrency data
            var currencies = new List<CryptoCurrency>();

            // Iterate through each cryptocurrency object in the response array
            foreach (var item in json.RootElement.EnumerateArray())
            {
                // Map the JSON data to CryptoCurrency objects and add them to the list
                currencies.Add(new CryptoCurrency
                {
                    Id = item.GetProperty("id").GetString(),
                    Name = item.GetProperty("name").GetString(),
                    Symbol = item.GetProperty("symbol").GetString(),
                    PriceUsd = item.GetProperty("current_price").GetDouble(),
                    ChangePercent24Hr = item.GetProperty("price_change_percentage_24h").GetDouble(),
                    VolumeUsd24Hr = item.GetProperty("total_volume").GetDouble(),
                });
            }

            // Return the list of cryptocurrency data
            return currencies;
        }

        // Method to get the historical price data for a specific cryptocurrency
        public async Task<List<PriceHistoryItem>> GetPriceHistoryAsync(string coinId, int days = 7)
        {
            // Construct the API URL to fetch the price history for a given coin over the last 'days' days
            var url = $"https://api.coingecko.com/api/v3/coins/{coinId}/market_chart?vs_currency=usd&days={days}";

            // Make an asynchronous HTTP GET request to the CoinGecko API
            var response = await _http.GetAsync(url);

            // If the response is not successful, log the error and return an empty list
            if (!response.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to get the history for {coinId}");
                return new List<PriceHistoryItem>();
            }

            // Read the response content as a stream
            using var stream = await response.Content.ReadAsStreamAsync();

            // Parse the response JSON
            var json = await JsonDocument.ParseAsync(stream);

            // Initialize a list to hold the parsed historical price data
            var list = new List<PriceHistoryItem>();

            // Iterate through the "prices" array in the response JSON, which contains price data points
            foreach (var point in json.RootElement.GetProperty("prices").EnumerateArray())
            {
                // Convert the Unix timestamp to DateTime
                var time = DateTimeOffset.FromUnixTimeMilliseconds((long)point[0].GetDouble()).DateTime;

                // Get the price value
                var price = point[1].GetDouble();

                // Add the price data to the list
                list.Add(new PriceHistoryItem { Time = time, Value = price });
            }

            // Return the list of historical price data
            return list;
        }
    }
}