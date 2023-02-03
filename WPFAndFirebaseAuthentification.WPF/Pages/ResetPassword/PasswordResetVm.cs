using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.ResetPassword;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels; 

public class PasswordResetVm : BaseVm {
    
    public PasswordResetFormVm PasswordResetFormVm { get; }

    public PasswordResetVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {
        PasswordResetFormVm = new PasswordResetFormVm(firebaseAuthProvider, loginNavigationService);
    }
}