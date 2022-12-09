using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class LogoutCommand : BaseCommand {
    private readonly AuthentificationStore _authentificationStore;
    private readonly INavigationService _loginNavigationService;

    public LogoutCommand(AuthentificationStore authentificationStore, INavigationService loginNavigationService) {
        _authentificationStore = authentificationStore;
        _loginNavigationService = loginNavigationService;
    }

    public override void Execute(object? parameter) {
        _authentificationStore.Logout();
        _loginNavigationService.Navigate();
    }
}