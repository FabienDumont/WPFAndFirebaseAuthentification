using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Application.Initialization;
using WPFAndFirebaseAuthentification.WPF.MVVM.Views;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;

public static class AddMainWindowExtensions {
    public static IHostBuilder AddMainWindow(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddSingleton<ApplicationInitializer>();
                serviceCollection.AddSingleton<MainVm>();
                serviceCollection.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainVm>() });
            }
        );

        return host;
    }
}