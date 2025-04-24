using CommunityToolkit.Mvvm.ComponentModel; // For ObservableObject to handle property change notifications
using CryptoViewer.Models; // For CryptoCurrency and PriceHistoryItem models
using CryptoViewer.Services; // For CoinGeckoService, used to fetch cryptocurrency data
using OxyPlot; // For creating and displaying plots
using OxyPlot.Axes; // For defining axes in the plot
using OxyPlot.Series; // For defining series in the plot (such as line series)

namespace CryptoViewer.ViewModels
{
    public class DetailsViewModel : ObservableObject
    {
        // Private fields for the selected cryptocurrency and plot model
        private CryptoCurrency _selectedCurrency;
        private PlotModel _priceHistoryPlotModel;

        // Property for the selected cryptocurrency
        public CryptoCurrency SelectedCurrency
        {
            get => _selectedCurrency;
            set => SetProperty(ref _selectedCurrency, value); // Notify when this property changes
        }

        // Property for the plot model that holds the price history chart
        public PlotModel PriceHistoryPlotModel
        {
            get => _priceHistoryPlotModel;
            set => SetProperty(ref _priceHistoryPlotModel, value); // Notify when the plot model changes
        }

        // Method to load the cryptocurrency and its price history asynchronously
        public async Task LoadCurrencyAsync(CryptoCurrency currency)
        {
            // Set the selected currency
            SelectedCurrency = currency;

            // Instantiate the service and fetch the price history
            var service = new CoinGeckoService();
            var history = await service.GetPriceHistoryAsync(currency.Id); // Asynchronous call to fetch price history

            // Assign the price history to the selected currency
            SelectedCurrency.PriceHistory = history;

            // Check if history is null or empty and log a message if so
            if (history == null || !history.Any())
            {
                System.Diagnostics.Debug.WriteLine("Price history is empty or null");
                return; // Exit the method if no history was fetched
            }

            // Log the count of price history points loaded
            System.Diagnostics.Debug.WriteLine($"Loaded {history.Count} price points");

            // Create the plot model for displaying the price history
            var plotModel = new PlotModel { Title = "Price History (7 days)" };

            // Define the DateTime axis (X-axis)
            plotModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom, // X-axis position
                StringFormat = "MM-dd", // Date format to display
                Title = "Дата", // Axis title in Ukrainian ("Date")
                IntervalType = DateTimeIntervalType.Days, // Interval between date points
                MinorIntervalType = DateTimeIntervalType.Days, // Minor intervals for dates
                MajorGridlineStyle = LineStyle.Solid, // Gridline style for major intervals
                MinorGridlineStyle = LineStyle.Dot // Gridline style for minor intervals
            });

            // Define the linear axis (Y-axis)
            plotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left, // Y-axis position
                Title = "Ціна (USD)", // Axis title in Ukrainian ("Price (USD)")
                MajorGridlineStyle = LineStyle.Solid, // Gridline style for major intervals
                MinorGridlineStyle = LineStyle.Dot // Gridline style for minor intervals
            });

            // Create a line series for the price history data
            var lineSeries = new LineSeries
            {
                Title = currency.Name, // Set the title of the series to the currency name
                Color = OxyColors.SteelBlue // Set the line color
            };

            // Add data points to the line series (price history)
            foreach (var item in history)
            {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Time), item.Value)); // Convert time and value to a DataPoint
            }

            // Add the line series to the plot model
            plotModel.Series.Add(lineSeries);

            // Set the plot model property, which will trigger the UI to update with the plot
            PriceHistoryPlotModel = plotModel;
        }
    }
}
