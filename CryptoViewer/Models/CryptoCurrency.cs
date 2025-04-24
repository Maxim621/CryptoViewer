namespace CryptoViewer.Models
{
    // This class represents a cryptocurrency.
    // It contains basic information about the cryptocurrency, such as name, symbol, price, and a price history.
    public class CryptoCurrency
    {
        // A unique identifier for the cryptocurrency (e.g., "bitcoin", "ethereum").
        public string Id { get; set; }

        // The name of the cryptocurrency (e.g., "Bitcoin", "Ethereum").
        public string Name { get; set; }

        // The symbol of the cryptocurrency (e.g., "BTC", "ETH").
        public string Symbol { get; set; }

        // The current price of the cryptocurrency in USD.
        public double PriceUsd { get; set; }

        // The percentage change in price over the past 24 hours.
        public double ChangePercent24Hr { get; set; }

        // The trading volume (in USD) of the cryptocurrency over the past 24 hours.
        public double VolumeUsd24Hr { get; set; }

        // A list to hold the price history of the cryptocurrency.
        // Each entry in the list is a PriceHistoryItem that contains the timestamp and the price value at that time.
        public List<PriceHistoryItem> PriceHistory { get; set; }

        // Constructor to initialize the properties.
        // The PriceHistory list is initialized to avoid null reference errors when trying to add items to it.
        public CryptoCurrency()
        {
            PriceHistory = new List<PriceHistoryItem>();  // Initializing the list to an empty list.
        }
    }

    // This class represents a single entry in the price history.
    // It contains a timestamp (DateTime) and the price value (Value) at that time.
    public class PriceHistoryItem
    {
        // The timestamp when the price was recorded.
        public DateTime Time { get; set; }

        // The price value of the cryptocurrency at the given time.
        public double Value { get; set; }
    }
}