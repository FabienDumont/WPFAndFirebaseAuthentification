using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.Http; 

public class FirebaseAuthHttpMessageHandler : DelegatingHandler {
    private readonly AuthentificationStore _authentificationStore;

    public FirebaseAuthHttpMessageHandler(AuthentificationStore authentificationStore) {
        _authentificationStore = authentificationStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
        FirebaseAuthLink? firebaseAuthLink = await _authentificationStore.GetFreshAuthAsync();

        if (firebaseAuthLink != null) {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", firebaseAuthLink.FirebaseToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}