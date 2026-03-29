using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Campus.ViewModels;

public class ProfileViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private string _email;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _fullName;
    public string FullName
    {
        get => _fullName;
        set
        {
            _fullName = value;
            OnPropertyChanged();
        }
    }
    public ICommand LogoutCommand { get; }

    public ProfileViewModel()
    {
        LogoutCommand = new Command(OnLogout);
    }

    private async void OnLogout()
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}