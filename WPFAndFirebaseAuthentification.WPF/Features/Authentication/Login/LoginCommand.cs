using System;
using System.Threading.Tasks;
using System.Windows;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.Login;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Commands;

public class LoginCommand : BaseAsyncCommand {
    private readonly LoginFormVm _loginFormVm;
    private readonly AuthenticationStore _authenticationStore;
    private readonly INavigationService _homeNavigationService;

    public LoginCommand(LoginFormVm loginFormVm, AuthenticationStore authenticationStore, INavigationService homeNavigationService) {
        _loginFormVm = loginFormVm;
        _authenticationStore = authenticationStore;
        _homeNavigationService = homeNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        try {
            await _authenticationStore.Login(_loginFormVm.Email, _loginFormVm.Password);
            MessageBox.Show("Successfully logged in!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            _homeNavigationService.Navigate();
        } catch (Exception) {
            MessageBox.Show("Login failed. Please check your information or try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}