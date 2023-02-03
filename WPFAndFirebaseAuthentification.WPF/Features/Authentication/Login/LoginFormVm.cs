using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Features.Authentication.Login; 

public class LoginFormVm : BaseVm {
    private string _email;

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _password;

    public string Password {
        get => _password;
        set {
            _password = value;
            OnPropertyChanged();
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand NavigateRegisterCommand { get; }
    public ICommand NavigatePasswordResetCommand { get; }

    public LoginFormVm(
        AuthenticationStore authenticationStore, INavigationService registerNavigationService, INavigationService homeNavigationService,
        INavigationService passwordResetNavigationService
    ) {
        _email = "fabdum19@live.fr";
        _password = "password";

        SubmitCommand = new LoginCommand(this, authenticationStore, homeNavigationService);
        NavigateRegisterCommand = new NavigateCommand(registerNavigationService);
        NavigatePasswordResetCommand = new NavigateCommand(passwordResetNavigationService);
    }
}