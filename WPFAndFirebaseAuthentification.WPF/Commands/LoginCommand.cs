using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Commands;

public class LoginCommand : BaseAsyncCommand {
    private readonly LoginVm _loginVm;
    private readonly AuthentificationStore _authentificationStore;
    private readonly INavigationService _homeNavigationService;

    public LoginCommand(LoginVm loginVm, AuthentificationStore authentificationStore, INavigationService homeNavigationService) {
        _loginVm = loginVm;
        _authentificationStore = authentificationStore;
        _homeNavigationService = homeNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        try {
            await _authentificationStore.Login(_loginVm.Email, _loginVm.Password);
            MessageBox.Show("Successfully logged in!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _homeNavigationService.Navigate();
        } catch (Exception) {
            MessageBox.Show("Login failed. Please check your information or try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}