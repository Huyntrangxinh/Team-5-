using System.ComponentModel;
using System.Text.RegularExpressions;
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
            LoginCommand = new Command(OnLogin, CanExecuteLogin);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(IsEmailValid));
                OnPropertyChanged(nameof(EmailBackgroundColor));
                ((Command)LoginCommand).ChangeCanExecute();
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
                OnPropertyChanged(nameof(IsPasswordValid));
                OnPropertyChanged(nameof(PasswordBackgroundColor));
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set { errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); OnPropertyChanged(nameof(HasError)); }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChanged(nameof(IsBusy)); ((Command)LoginCommand).ChangeCanExecute(); }
        }

        public bool IsEmailValid => !string.IsNullOrWhiteSpace(Email) && Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        public bool IsPasswordValid => !string.IsNullOrWhiteSpace(Password) && Password.Length >= 3;
        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

        public Color EmailBackgroundColor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Email))
                    return Color.FromArgb("#F1F5F9");
                return IsEmailValid ? Color.FromArgb("#F1F5F9") : Color.FromArgb("#FFE0E0");
            }
        }

        public Color PasswordBackgroundColor
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Password))
                    return Color.FromArgb("#F1F5F9");
                return IsPasswordValid ? Color.FromArgb("#F1F5F9") : Color.FromArgb("#FFE0E0");
            }
        }

        public ICommand LoginCommand { get; }

        private bool CanExecuteLogin()
        {
            return !IsBusy && IsEmailValid && IsPasswordValid;
        }

        private async void OnLogin()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;
            // Validate
            if (!IsEmailValid)
            {
                ErrorMessage = "Invalid email format";
                IsBusy = false;
                return;
            }

            if (!IsPasswordValid)
            {
                ErrorMessage = "Password must be at least 3 characters";
                IsBusy = false;
                return;
            }

            // Login
            var user = _userService.Login(Email, Password);

            if (user != null)
            {
                AppSession.CurrentUser = user;
                await App.Current.MainPage.DisplayAlert("Success", "Login success", "OK");
            }
            else
            {
                ErrorMessage = "Invalid email or password";
            }

            IsBusy = false;
        }
    }
}