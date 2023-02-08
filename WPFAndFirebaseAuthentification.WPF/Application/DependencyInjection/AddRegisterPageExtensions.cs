using Firebase.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddRegisterPageExtensions {
    public static IHostBuilder AddRegisterPage(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddTransient<RegisterVm>(
                    s => new RegisterVm(s.GetRequiredService<FirebaseAuthProvider>(), s.GetRequiredService<NavigationService<LoginVm>>())
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<RegisterVm>(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<RegisterVm>)
                );
            }
        );

        return host;
    }
}