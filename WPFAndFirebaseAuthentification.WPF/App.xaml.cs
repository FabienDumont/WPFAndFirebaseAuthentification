﻿using System;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVMEssentials.Services;
using MVVMEssentials.Stores;
using MVVMEssentials.ViewModels;
using Refit;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.Http;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.MVVM.Views;
using WPFAndFirebaseAuthentification.WPF.Queries;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF;

public partial class App {
    private readonly IHost _host;

    public App() {
        _host = Host.CreateDefaultBuilder().ConfigureServices(
            (context, serviceCollection) => {
                string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");
                serviceCollection.AddSingleton(new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey)));

                serviceCollection.AddTransient<FirebaseAuthHttpMessageHandler>();

                serviceCollection.AddRefitClient<IGetSecretMessageQuery>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(context.Configuration.GetValue<string>("API_BASE_URL")))
                    .AddHttpMessageHandler<FirebaseAuthHttpMessageHandler>();

                serviceCollection.AddSingleton<NavigationStore>();
                serviceCollection.AddSingleton<ModalNavigationStore>();
                serviceCollection.AddSingleton<AuthenticationStore>();
                serviceCollection.AddSingleton<CurrentUserStore>();

                serviceCollection.AddSingleton(
                    s => new NavigationService<RegisterVm>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new RegisterVm(s.GetRequiredService<FirebaseAuthProvider>(), s.GetRequiredService<NavigationService<LoginVm>>())
                    )
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<LoginVm>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new LoginVm(
                            s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<NavigationService<RegisterVm>>(),
                            s.GetRequiredService<NavigationService<HomeVm>>(), s.GetRequiredService<NavigationService<PasswordResetVm>>()
                        )
                    )
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<HomeVm>(
                        s.GetRequiredService<NavigationStore>(),
                        () => HomeVm.LoadVm(
                            s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<CurrentUserStore>(),
                            s.GetRequiredService<IGetSecretMessageQuery>(), s.GetRequiredService<NavigationService<LoginVm>>(),
                            s.GetRequiredService<NavigationService<ProfileVm>>()
                        )
                    )
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<PasswordResetVm>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new PasswordResetVm(s.GetRequiredService<FirebaseAuthProvider>(), s.GetRequiredService<NavigationService<LoginVm>>())
                    )
                );

                serviceCollection.AddSingleton(
                    s => new NavigationService<ProfileVm>(
                        s.GetRequiredService<NavigationStore>(),
                        () => new ProfileVm(
                            s.GetRequiredService<AuthenticationStore>(), s.GetRequiredService<CurrentUserStore>(),
                            s.GetRequiredService<NavigationService<HomeVm>>()
                        )
                    )
                );

                serviceCollection.AddSingleton<MainVm>();

                serviceCollection.AddSingleton(s => new MainWindow() { DataContext = s.GetRequiredService<MainVm>() });
            }
        ).Build();
    }

    protected override async void OnStartup(StartupEventArgs e) {
        await Initialize();

        MainWindow = _host.Services.GetRequiredService<MainWindow>();
        MainWindow.Show();

        base.OnStartup(e);
    }

    private async Task Initialize() {
        AuthenticationStore authenticationStore = _host.Services.GetRequiredService<AuthenticationStore>();
        CurrentUserStore currentUserStore = _host.Services.GetRequiredService<CurrentUserStore>();

        try {
            await authenticationStore.Initialize();

            if (currentUserStore.User.IsLoggedIn) {
                var navigationService = _host.Services.GetRequiredService<NavigationService<HomeVm>>();
                navigationService.Navigate();
            } else {
                var navigationService = _host.Services.GetRequiredService<NavigationService<LoginVm>>();
                navigationService.Navigate();
            }
        } catch (FirebaseAuthException) {
            var navigationService = _host.Services.GetRequiredService<NavigationService<LoginVm>>();
            navigationService.Navigate();
        } catch (Exception) {
            MessageBox.Show("Failed to load application.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}