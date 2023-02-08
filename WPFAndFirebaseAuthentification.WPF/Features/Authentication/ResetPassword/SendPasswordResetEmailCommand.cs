using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.ResetPassword;

namespace WPFAndFirebaseAuthentification.WPF.Commands;

public class SendPasswordResetEmailCommand : BaseAsyncCommand {
    private readonly PasswordResetFormVm _passwordResetFormVm;
    private readonly FirebaseAuthProvider _firebaseAuthProvider;
    private readonly INavigationService _loginNavigationService;

    public SendPasswordResetEmailCommand(
        PasswordResetFormVm passwordResetFormVm, FirebaseAuthProvider firebaseAuthProvider, INavigationService loginNavigationService
    ) {
        _passwordResetFormVm = passwordResetFormVm;
        _firebaseAuthProvider = firebaseAuthProvider;
        _loginNavigationService = loginNavigationService;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        string email = _passwordResetFormVm.Email;
        try {
            await _firebaseAuthProvider.SendPasswordResetEmailAsync(email);
            MessageBox.Show(
                "Successfully sent password reset email. Check your email to finish resetting your password.", "Success", MessageBoxButton.OK,
                MessageBoxImage.Information
            );
            _loginNavigationService.Navigate();
        } catch (Exception e) {
            MessageBox.Show("Failed to send password reset email. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}