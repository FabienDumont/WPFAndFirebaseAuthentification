using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Queries;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddHomePageExtensions {
    public static IHostBuilder AddHomePage(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddTransient<HomeVm>(
                    s => HomeVm.LoadVm(
                        s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<CurrentUserStore>(), s.GetRequiredService<IGetSecretMessageQuery>(),
                        s.GetRequiredService<NavigationService<LoginVm>>(), s.GetRequiredService<NavigationService<ProfileVm>>()
                    )
                );

                serviceCollection.AddSingleton(s => new NavigationService<HomeVm>(s.GetRequiredService<NavigationStore>(), s.GetRequiredService<HomeVm>));
            }
        );

        return host;
    }
}