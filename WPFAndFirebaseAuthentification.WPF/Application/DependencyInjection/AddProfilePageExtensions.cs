using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddProfilePageExtensions {
    public static IHostBuilder AddProfilePage(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddTransient<ProfileVm>(
                    s => new ProfileVm(
                        s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<CurrentUserStore>(), s.GetRequiredService<NavigationService<HomeVm>>()
                    )
                );

                serviceCollection.AddSingleton(s => new NavigationService<ProfileVm>(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<ProfileVm>));
            }
        );

        return host;
    }
}