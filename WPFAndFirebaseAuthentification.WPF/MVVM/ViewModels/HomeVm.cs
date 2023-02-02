using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Queries;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class HomeVm : BaseVm {
    private readonly AuthentificationStore _authentificationStore;

    public string Username => _authentificationStore.CurrentUser?.DisplayName ?? "Unknown";

    private string _message;

    public string Message {
        get => _message;
        set {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public ICommand LoadMessageCommand { get; }
    public ICommand NavigateProfileCommand { get; }
    public ICommand LogoutCommand { get; }

    public HomeVm(
        AuthentificationStore authentificationStore, IGetMessageQuery getMessageQuery, INavigationService loginNavigationService,
        INavigationService profileNavigationService
    ) {
        _authentificationStore = authentificationStore;

        _message = "";

        LoadMessageCommand = new LoadMessageCommand(this, getMessageQuery);
        NavigateProfileCommand = new NavigateCommand(profileNavigationService);
        LogoutCommand = new LogoutCommand(authentificationStore, loginNavigationService);
    }

    public static HomeVm LoadVm(
        AuthentificationStore authentificationStore, IGetMessageQuery getMessageQuery, INavigationService loginNavigationService,
        INavigationService profileNavigationService
    ) {
        HomeVm homeVm = new HomeVm(authentificationStore, getMessageQuery, loginNavigationService, profileNavigationService);

        homeVm.LoadMessageCommand.Execute(null);

        return homeVm;
    }
}