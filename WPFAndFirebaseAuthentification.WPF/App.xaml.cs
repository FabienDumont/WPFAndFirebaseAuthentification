using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection;
using WPFAndFirebaseAuthentification.WPF.Application.Initialization;
using WPFAndFirebaseAuthentification.WPF.MVVM.Views;

namespace WPFAndFirebaseAuthentification.WPF;

public partial class App : System.Windows.Application {
    private readonly IHost _host;

    public App() {
        _host = Host.CreateDefaultBuilder().AddNavigation().AddRegisterPage().AddLoginPage().AddHomePage().AddPasswordResetPage().AddProfilePage()
            .AddMainWindow().AddUserEntity().AddSecretMessageFeature().AddAuthenticationFeature().Build();
    }

    protected override async void OnStartup(StartupEventArgs e) {
        ApplicationInitializer applicationInitializer = _host.Services.GetRequiredService<ApplicationInitializer>();
        await applicationInitializer.Initialize();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }
}