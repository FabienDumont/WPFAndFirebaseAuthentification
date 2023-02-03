using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;

namespace WPFAndFirebaseAuthentification.WPF.Features.Authentication.Register; 

public class RegisterFormVm : BaseVm {
    private string _email;

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged();
        }
    }

    private string _username;

    public string Username {
        get => _username;
        set {
            _username = value;
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

    private string _confirmedPassword;

    public string ConfirmedPassword {
        get => _confirmedPassword;
        set {
            _confirmedPassword = value;
            OnPropertyChanged();
        }
    }

    private bool _shouldSendVerificationEmail = true;

    public bool ShouldSendVerificationEmail {
        get => _shouldSendVerificationEmail ;
        set {
            _shouldSendVerificationEmail  = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand SubmitCommand { get; }
    public ICommand NavigateLoginCommand { get; }

    public RegisterFormVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {

        _email = "fabdum19@live.fr";
        _username = "Fabibi";
        _password = "password";
        _confirmedPassword = "password";
        
        SubmitCommand = new RegisterCommand(this, firebaseAuthProvider, loginNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }
}