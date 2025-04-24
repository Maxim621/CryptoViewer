using System.Windows;
using CryptoViewer.Views;

namespace CryptoViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Navigate to the HomePage on startup
            MainFrame.Navigate(new HomePage());
        }
    }
}
