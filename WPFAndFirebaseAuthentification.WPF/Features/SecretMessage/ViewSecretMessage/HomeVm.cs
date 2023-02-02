using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Queries;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class HomeVm : BaseVm {
    private readonly AuthenticationStore _authenticationStore;

    public string Username => _authenticationStore.CurrentUser?.DisplayName ?? "Unknown";

    private string _message;

    public string Message {
        get => _message;
        set {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public ICommand LoadSecretMessageCommand { get; }
    public ICommand NavigateProfileCommand { get; }
    public ICommand LogoutCommand { get; }

    public HomeVm(
        AuthenticationStore authenticationStore, IGetSecretMessageQuery getSecretMessageQuery, INavigationService loginNavigationService,
        INavigationService profileNavigationService
    ) {
        _authenticationStore = authenticationStore;

        _message = "";

        LoadSecretMessageCommand = new LoadSecretMessageCommand(this, getSecretMessageQuery);
        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
    }

    public static HomeVm LoadVm(
        AuthenticationStore authenticationStore, IGetSecretMessageQuery getSecretMessageQuery, INavigationService loginNavigationService,
        INavigationService profileNavigationService
    ) {
        HomeVm homeVm = new HomeVm(authenticationStore, getSecretMessageQuery, loginNavigationService, profileNavigationService);

        homeVm.LoadSecretMessageCommand.Execute(null);

        return homeVm;
    }
}