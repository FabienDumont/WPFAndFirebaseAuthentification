using System;
using System.Threading.Tasks;
using System.Windows;
using MVVMEssentials.Commands;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Commands;

public class SendEmailVerificationEmailCommand : BaseAsyncCommand {
    private AuthentificationStore _authentificationStore;

    public SendEmailVerificationEmailCommand(AuthentificationStore authentificationStore) {
        _authentificationStore = authentificationStore;
    }

    protected override async Task ExecuteAsync(object? parameter) {
        try {
            await _authentificationStore.SendEmailVerificationEmail();
            MessageBox.Show(
                "Successfully sent email verification email. Check your email to verify.", "Success", MessageBoxButton.OK, MessageBoxImage.Information
            );
        } catch (Exception) {
            MessageBox.Show("Failed to send email verification email. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}