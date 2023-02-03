using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.Register;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

namespace WPFAndFirebaseAuthentification.WPF.Commands;

public class RegisterCommand : BaseAsyncCommand {
    private readonly RegisterFormVm _registerFormVm;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;
    private readonly INavigationService _loginNavigationService;

    public RegisterCommand(RegisterFormVm registerFormVm, FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService) {
        _registerFormVm = registerFormVm;
        _firebaseAuthProvider = firebaseAuthProvider;
        _loginNavigationService = loginNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        string? password = _registerFormVm.Password;
        string? confirmedPassword = _registerFormVm.ConfirmedPassword;
        if (!password.Equals(confirmedPassword)) {
            MessageBox.Show("Password and confirmed password must match.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        } else {
            try {
                await _firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(
                    _registerFormVm.Email, password, _registerFormVm.Username, _registerFormVm.ShouldSendVerificationEmail
                );
                MessageBox.Show("Successfully registered!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _loginNavigationService.Navigate();
            } catch (Exception) {
                MessageBox.Show("Registration failed. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}