using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.Features.SecretMessage.ViewSecretMessage;
using WPFAndFirebaseAuthentification.WPF.Queries;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class HomeVm : BaseVm, IViewSecretMessageViewModel {
    private readonly CurrentUserStore _currentUserStore;

    public string Username => _currentUserStore.User?.DisplayName ?? "Unknown";

    private string _message;

    public string SecretMessage {
        get => _message;
        set {
            _message = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadSecretMessageCommand { get; }
    public ICommand NavigateProfileCommand { get; }
    public ICommand LogoutCommand { get; }

    public HomeVm(
        AuthenticationStore authenticationStore, CurrentUserStore currentUserStore, IGetSecretMessageQuery getSecretMessageQuery,
        INavigationService loginNavigationService, INavigationService profileNavigationService
    ) {
        _currentUserStore = currentUserStore;

        _message = "";

        LoadSecretMessageCommand = new LoadSecretMessageCommand(this, getSecretMessageQuery, currentUserStore);
        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        LogoutCommand = new LogoutCommand(authenticationStore, loginNavigationService);
    }

    public static HomeVm LoadVm(
        AuthenticationStore authenticationStore, CurrentUserStore currentUserStore, IGetSecretMessageQuery getSecretMessageQuery,
        INavigationService loginNavigationService, INavigationService profileNavigationService
    ) {
        HomeVm homeVm = new HomeVm(authenticationStore, currentUserStore, getSecretMessageQuery, loginNavigationService, profileNavigationService);

        homeVm.LoadSecretMessageCommand.Execute(null);

        return homeVm;
    }
}