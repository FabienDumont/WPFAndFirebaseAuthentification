using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class LoginVm : BaseVm {
    private string _email;

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    private string _password;

    public string Password {
        get => _password;
        set {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand NavigateRegisterCommand { get; }
    public ICommand NavigatePasswordResetCommand { get; }

    public LoginVm(
        AuthentificationStore authentificationStore, INavigationService registerNavigationService, INavigationService homeNavigationService,
        INavigationService passwordResetNavigationService
    ) {
        _email = "fabdum19@live.fr";
        _password = "password";

        SubmitCommand = new LoginCommand(this, authentificationStore, homeNavigationService);
        NavigateRegisterCommand = new NavigateCommand(registerNavigationService);
        NavigatePasswordResetCommand = new NavigateCommand(passwordResetNavigationService);
    }
}