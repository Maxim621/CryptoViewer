# CryptoViewer

**CryptoViewer** is a WPF desktop application developed in C# (.NET 8) that displays real-time information about cryptocurrencies using the [CoinGecko API](https://www.coingecko.com/).  
The application supports light/dark themes and multilingual UI (English, Ukrainian and Other), and includes a price chart for selected currencies.

---

Features

- View top cryptocurrencies with name, symbol, and price
- Interactive chart of 7-day price history (OxyPlot)
- Light/Dark theme switching
- Language switching (English/Ukrainian/Other)
- MVVM architecture
- No third-party HTTP libraries used

---

Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022+ with WPF support

### Build & Run

1. Clone the repository.

2. Open the solution file CryptoViewer.sln in Visual Studio.

3. Build and run the project (F5).

---

### API Used
This app uses the CoinGecko API:

- Endpoint: /coins/{id}/market_chart

- No authentication required

---

### Project Structure
CryptoViewer/
│
├── Models/            # Data models (e.g., CryptoCurrency, PricePoint)
├── ViewModels/        # MVVM ViewModels (Home, Details)
├── Views/             # WPF Pages (HomePage, DetailsPage)
├── Services/          # API service (CoinGeckoService), ThemeService
├── Themes/            # Light and Dark ResourceDictionaries
├── Resources/         # Localization files (en.xaml, uk.xaml)
├── Helpers/           # Utilities (e.g., ThemeManager)
├── App.xaml           # Application entry with theme setup
├── MainWindow.xaml    # Main container with frame navigation
