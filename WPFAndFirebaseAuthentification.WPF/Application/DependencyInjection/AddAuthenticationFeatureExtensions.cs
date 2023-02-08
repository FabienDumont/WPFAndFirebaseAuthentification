using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFAndFirebaseAuthentification.WPF.Http;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection; 

public static class AddAuthenticationFeatureExtensions {
    public static IHostBuilder AddAuthenticationFeature(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                string firebaseApiKey = context.Configuration.GetValue<string>("FIREBASE_API_KEY");
                serviceCollection.AddSingleton(new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey)));
                serviceCollection.AddSingleton<AuthenticationStore>();
                serviceCollection.AddTransient<FirebaseAuthHttpMessageHandler>();
            }
        );

        return host;
    }
}