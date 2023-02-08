using Firebase.Auth;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.Register;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class RegisterVm : BaseVm {
    public RegisterFormVm RegisterFormVm { get; }

    public RegisterVm(FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {
        RegisterFormVm = new RegisterFormVm(firebaseAuthProvider, loginNavigationService);
    }
}