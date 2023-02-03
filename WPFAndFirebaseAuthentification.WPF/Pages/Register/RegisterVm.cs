using System.Windows.Input;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.Register;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class RegisterVm : BaseVm {
    public RegisterFormVm RegisterFormVm { get; }

    public RegisterVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {
        RegisterFormVm = new RegisterFormVm(firebaseAuthProvider, loginNavigationService);
    }
}