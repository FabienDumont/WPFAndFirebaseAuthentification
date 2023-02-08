using Firebase.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddResetPasswordPageExtensions {
    public static IHostBuilder AddPasswordResetPage(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddTransient<PasswordResetVm>(
                    s => new PasswordResetVm(s.GetRequiredService<FirebaseAuthProvider>(), s.GetRequiredService<NavigationService<LoginVm>>())
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<PasswordResetVm>(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<PasswordResetVm>)
                );
            }
        );

        return host;
    }
}