using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class RegisterVm : BaseVm {
    private string _email;

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    private string _username;

    public string Username {
        get => _username;
        set {
            _username = value;
            OnPropertyChanged(nameof(Username));
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

    private string _confirmedPassword;

    public string ConfirmedPassword {
        get => _confirmedPassword;
        set {
            _confirmedPassword = value;
            OnPropertyChanged(nameof(ConfirmedPassword));
        }
    }
    
    public ICommand SubmitCommand { get; }
    public ICommand NavigateLoginCommand { get; }

    public RegisterVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {

        _email = "fabdum19@live.fr";
        _username = "Fabibi";
        _password = "password";
        _confirmedPassword = "password";
        
        SubmitCommand = new RegisterCommand(this, firebaseAuthProvider, loginNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }
}