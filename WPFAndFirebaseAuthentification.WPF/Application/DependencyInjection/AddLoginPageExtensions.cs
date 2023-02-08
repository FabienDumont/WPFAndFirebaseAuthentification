using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddLoginPageExtensions {
    public static IHostBuilder AddLoginPage(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddTransient<LoginVm>(
                    s => new LoginVm(
                        s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<NavigationService<RegisterVm>>(),
                        s.GetRequiredService<NavigationService<HomeVm>>(), s.GetRequiredService<NavigationService<PasswordResetVm>>()
                    )
                );

                serviceCollection.AddSingleton(s => new NavigationService<LoginVm>(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<LoginVm>));
            }
        );

        return host;
    }
}