using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class RegisterCommand : BaseAsyncCommand {
    private readonly RegisterVm _registerVm;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;
    private readonly INavigationService _loginNavigationService;

    public RegisterCommand(RegisterVm registerVm, FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {
        _registerVm = registerVm;
        _firebaseAuthProvider = firebaseAuthProvider;
        _loginNavigationService = loginNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        string? password = _registerVm.Password;
        string? confirmedPassword = _registerVm.ConfirmedPassword;
        if(!password.Equals(confirmedPassword)) {
            MessageBox.Show("Password and confirmed password must match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        } else {
            try {
                await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(_registerVm.Email, password, _registerVm.Username);
                MessageBox.Show("Successfully registered!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _loginNavigationService.Navigate();
            } catch (Exception) {
                MessageBox.Show("Registration failed. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}