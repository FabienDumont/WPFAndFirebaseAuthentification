using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Features.Authentication.ViewProfile;

public class ProfileDetailsVm : BaseVm {
    private readonly CurrentUserStore _currentUserStore;

    public string Username => _currentUserStore.User?.DisplayName ?? string.Empty;
    public string Email => _currentUserStore.User?.Email ?? string.Empty;
    public bool IsEmailVerified => _currentUserStore.User?.IsEmailVerified ?? false;

    public ICommand SendEmailVerificationEmailCommand { get; }
    public ICommand NavigateHomeCommand { get; }

    public ProfileDetailsVm(AuthenticationStore authentication, CurrentUserStore currentUserStore, INavigationService homeNavigationService) {
        _currentUserStore = currentUserStore;

        SendEmailVerificationEmailCommand = new SendEmailVerificationEmailCommand(authentication);
        NavigateHomeCommand = new NavigateCommand(homeNavigationService);
    }
}