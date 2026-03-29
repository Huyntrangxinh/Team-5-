using System.ComponentModel;
using System.Windows.Input;
using Campus.Services;
using Campus.Session;

namespace Campus.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;

        public LoginViewModel()
        {
            _userService = new UserService();
            LoginCommand = new Command(OnLogin);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        private async void OnLogin()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter all fields", "OK");
                return;
            }

            // Login
            var user = _userService.Login(Username, Password);

            if (user != null)
            {
                // Save session (đã sửa namespace)
                AppSession.CurrentUser = user;

                await App.Current.MainPage.DisplayAlert("Success", "Login success", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Invalid login", "OK");
            }
        }
    }
}