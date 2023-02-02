using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class LogoutCommand : BaseCommand {
    private readonly AuthenticationStore _authenticationStore;
    private readonly INavigationService _loginNavigationService;

    public LogoutCommand(AuthenticationStore authenticationStore, INavigationService loginNavigationService) {
        _authenticationStore = authenticationStore;
        _loginNavigationService = loginNavigationService;
    }

    public override void Execute(object? parameter) {
        _authenticationStore.Logout();
        _loginNavigationService.Navigate();
    }
}