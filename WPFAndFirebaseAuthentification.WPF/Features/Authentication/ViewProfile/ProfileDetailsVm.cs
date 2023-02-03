using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Features.Authentication.ViewProfile; 

public class ProfileDetailsVm : BaseVm {
    private readonly AuthenticationStore _authenticationStore;

    public string Username => _authenticationStore.CurrentUser?.DisplayName ?? string.Empty;
    public string Email => _authenticationStore.CurrentUser?.Email ?? string.Empty;
    public bool IsEmailVerified => _authenticationStore.CurrentUser?.IsEmailVerified ?? false;
    
    public ICommand SendEmailVerificationEmailCommand { get; }
    public ICommand NavigateHomeCommand { get; }

    public ProfileDetailsVm(AuthenticationStore authenticationStore, INavigationService homeNavigationService) {
        _authenticationStore = authenticationStore;

        SendEmailVerificationEmailCommand = new SendEmailVerificationEmailCommand(_authenticationStore);
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }
}