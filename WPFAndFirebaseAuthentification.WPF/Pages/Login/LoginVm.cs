using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.Login;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class LoginVm : BaseVm {
    public LoginFormVm LoginFormVm { get; }

    public LoginVm(
        AuthenticationStore authenticationStore, INavigationService registerNavigationService, INavigationService homeNavigationService,
        INavigationService passwordResetNavigationService
    ) {
        LoginFormVm = new LoginFormVm(authenticationStore, registerNavigationService, homeNavigationService, passwordResetNavigationService);
    }
}