using System.ComponentModel;
using System.Runtime.CompilerServices;
using Campus.Services;
using Campus.Models;
using System.Collections.ObjectModel;

namespace Campus.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly SettingsService _settingsService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            _settingsService = new SettingsService();

            var settings = _settingsService.GetSettings();
            _isDarkMode = settings.ThemeMode == ThemeMode.Dark;
            _isNotificationsEnabled = settings.NotificationsEnabled;

            _selectedLanguage = _settingsService.GetLanguage();
            if (string.IsNullOrWhiteSpace(_selectedLanguage))
            {
                _selectedLanguage = SupportedLanguages[0];
            }
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

                var mode = value ? ThemeMode.Dark : ThemeMode.Light;
                _settingsService.SaveThemeMode(mode);

                if (Application.Current != null)
                {
                    Application.Current.UserAppTheme =
                        value ? AppTheme.Dark : AppTheme.Light;
                }
            }
        }

        private bool _isNotificationsEnabled;
        private string? _selectedLanguage;

        public bool IsNotificationsEnabled
        {
            get => _isNotificationsEnabled;
            set
            {
                if (_isNotificationsEnabled == value) return;
                _isNotificationsEnabled = value;
                OnPropertyChanged();
                _settingsService.SaveNotificationsEnabled(value);
            }
        }

        public ObservableCollection<string> SupportedLanguages { get; } =
            new ObservableCollection<string>
            {
                "English",    
                "Vietnamese"
            };
        public string? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (_selectedLanguage == value) return;
                _selectedLanguage = value;
                OnPropertyChanged();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    _settingsService.SaveLanguage(value);
                }
            }
        }
    }
}