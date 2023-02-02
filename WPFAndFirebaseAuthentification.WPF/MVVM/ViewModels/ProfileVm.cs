using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels; 

public class ProfileVm : BaseVm {

    private readonly AuthentificationStore _authentificationStore;

    public string Username => _authentificationStore.CurrentUser?.DisplayName ?? string.Empty;
    public string Email => _authentificationStore.CurrentUser?.Email ?? string.Empty;
    public bool IsEmailVerified => _authentificationStore.CurrentUser?.IsEmailVerified ?? false;
    
    public ICommand SendEmailVerificationEmailCommand { get; }
    public ICommand NavigateHomeCommand { get; }

    public ProfileVm(AuthentificationStore authentificationStore, INavigationService homeNavigationService) {
        _authentificationStore = authentificationStore;

        SendEmailVerificationEmailCommand = new SendEmailVerificationEmailCommand(_authentificationStore);
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }

}