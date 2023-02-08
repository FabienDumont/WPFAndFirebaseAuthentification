using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection; 

public static class AddNavigationExtensions {
    public static IHostBuilder AddNavigation(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddSingleton<NavigationStore>();
                serviceCollection.AddSingleton<ModalNavigationStore>();
            }
        );

        return host;
    }
}