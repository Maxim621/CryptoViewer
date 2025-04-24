using CryptoViewer.Models;
using CryptoViewer.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CryptoViewer.Views
{
    public partial class DetailsPage : Page
    {
        public DetailsPage(CryptoCurrency currency)
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
                // Check if DataContext is set and is of the correct type
                var vm = DataContext as DetailsViewModel;
                if (vm == null)
                {
                    MessageBox.Show("Failed to load data. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Check for null currency
                if (currency == null)
                {
                    MessageBox.Show("Cryptocurrency data not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await vm.LoadCurrencyAsync(currency);

                // Check if price history is available
                if (currency.PriceHistory == null || currency.PriceHistory.Count == 0)
                {
                    MessageBox.Show("Price history is not available for this cryptocurrency.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Create the chart model
                var plotModel = new PlotModel { Title = "Price History" };
                plotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm" });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });

                var lineSeries = new LineSeries { Title = "Price (USD)", Color = OxyColors.DodgerBlue };

                // Validate data for the chart
                foreach (var item in currency.PriceHistory)
                {
                    if (item.Time == null || item.Value <= 0)
                    {
                        MessageBox.Show("Invalid data for chart rendering.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.Time), item.Value));
                }

                plotModel.Series.Add(lineSeries);
                plotView.Model = plotModel;
            };
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if navigation back is possible
            if (NavigationService != null && NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
