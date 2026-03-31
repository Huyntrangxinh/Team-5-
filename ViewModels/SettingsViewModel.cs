using System.ComponentModel;
using System.Runtime.CompilerServices;
using Campus.Services;

namespace Campus.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            _settingsService = new SettingsService();
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isDarkMode;

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode == value) return;
                _isDarkMode = value;
                OnPropertyChanged();
            }
        }

        private bool _isNotificationsEnabled;

        public bool IsNotificationsEnabled
        {
            get => _isNotificationsEnabled;
            set
            {
                if (_isNotificationsEnabled == value) return;
                _isNotificationsEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}