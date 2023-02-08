using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using WPFAndFirebaseAuthentification.WPF.Http;
using WPFAndFirebaseAuthentification.WPF.Queries;

namespace WPFAndFirebaseAuthentification.WPF.Application.DependencyInjection; 

public static class AddSecretMessageFeatureExtensions {
    public static IHostBuilder AddSecretMessageFeature(this IHostBuilder host) {
        host.ConfigureServices(
            (context, serviceCollection) => {
                serviceCollection.AddRefitClient<IGetSecretMessageQuery>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(context.Configuration.GetValue<string>("API_BASE_URL")))
                    .AddHttpMessageHandler<FirebaseAuthHttpMessageHandler>();
            }
        );

        return host;
    }
}