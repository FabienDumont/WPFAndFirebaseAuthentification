using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels; 

public class PasswordResetVm : BaseVm {
    private string _email;

    public string Email {
        get => _email;
        set {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public ICommand SendPasswordResetEmailCommand { get; }
    public ICommand NavigateLoginCommand { get; }

    public PasswordResetVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {

        _email = "";
        
        SendPasswordResetEmailCommand = new SendPasswordResetEmailCommand(this, firebaseAuthProvider, loginNavigationService);
        NavigateLoginCommand = new NavigateCommand(loginNavigationService);
    }
}