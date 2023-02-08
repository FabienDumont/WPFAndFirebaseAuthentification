using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.Initialization;

public class ApplicationInitializer {
    private readonly AuthenticationStore _authenticationStore;
    private readonly CurrentUserStore _currentUserStore;
    private readonly NavigationService<HomeVm> _homeNavigationService;
    private readonly NavigationService<LoginVm> _loginNavigationService;

    public ApplicationInitializer(
        AuthenticationStore authenticationStore, CurrentUserStore currentUserStore, NavigationService<HomeVm> homeNavigationService,
        NavigationService<LoginVm> loginNavigationService
    ) {
        _authenticationStore = authenticationStore;
        _currentUserStore = currentUserStore;
        _homeNavigationService = homeNavigationService;
        _loginNavigationService = loginNavigationService;
    }

    public async Task Initialize() {
        try {
            await _authenticationStore.Initialize();

            if (_currentUserStore.User.IsLoggedIn) {
                _homeNavigationService.Navigate();
            } else {
                _loginNavigationService.Navigate();
            }
        } catch (FirebaseAuthException) {
            _loginNavigationService.Navigate();
        } catch (Exception) {
            MessageBox.Show("Failed to load application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}