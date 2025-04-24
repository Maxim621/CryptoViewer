using System.ComponentModel; // Import the namespace for INotifyPropertyChanged and PropertyChangedEventArgs
using System.Runtime.CompilerServices; // Import for CallerMemberName attribute, which helps with property change notification

namespace CryptoViewer.ViewModels
{
    // The BaseViewModel class implements INotifyPropertyChanged, allowing it to notify the view when properties change.
    public class BaseViewModel : INotifyPropertyChanged
    {
        // This event is triggered whenever a property in the ViewModel changes.
        public event PropertyChangedEventHandler PropertyChanged;

        // This method raises the PropertyChanged event to notify the view about the property change.
        // It uses the CallerMemberName attribute to automatically pass the property name when the method is called.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // The PropertyChanged event is triggered if there are any listeners (views or other components).
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This is a helper method to set a property value and automatically notify the view when the value changes.
        // It checks if the value has changed and if so, updates the property and calls OnPropertyChanged.
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // If the value hasn't changed, return false to indicate no update was made.
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            // If the value has changed, update the storage field and notify listeners about the change.
            storage = value;
            OnPropertyChanged(propertyName); // Notify the view of the property change
            return true; // Indicate that the property value has been updated
        }
    }
}
