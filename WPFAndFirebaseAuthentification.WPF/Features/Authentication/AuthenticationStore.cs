using System;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Auth;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using User = Firebase.Auth.User;

namespace WPFAndFirebaseAuthentification.WPF.Stores;

public class AuthenticationStore {
    private FirebaseAuthProvider _firebaseAuthProvider;
    private CurrentUserStore _currentUserStore;

    public AuthenticationStore(FirebaseAuthProvider firebaseAuthProvider, CurrentUserStore currentUserStore) {
        _firebaseAuthProvider = firebaseAuthProvider;
        _currentUserStore = currentUserStore;
    }

    public async Task Initialize() {
        string firebaseAuthJson = Properties.Settings.Default.FirebaseAuth;

        if (string.IsNullOrEmpty(firebaseAuthJson)) {
            return;
        }

        FirebaseAuth? firebaseAuth = JsonSerializer.Deserialize<FirebaseAuth>(firebaseAuthJson);

        if (firebaseAuth == null) {
            return;
        }

        _currentUserStore.UpdateAuth(new FirebaseAuthLink(_firebaseAuthProvider, firebaseAuth));

        await GetFreshAuthAsync();
    }

    public async Task Login(string email, string password) {
        _currentUserStore.UpdateAuth(await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password));
        SaveAuthentificationState();
    }

    public void Logout() {
        _currentUserStore.UpdateAuth(null);

        ClearAuthentificationState();
    }

    public async Task<FirebaseAuthLink?> GetFreshAuthAsync() {
        if (_currentUserStore.User.Auth == null) {
            return null;
        }

        _currentUserStore.UpdateAuth(await _currentUserStore.User.Auth.GetFreshAuthAsync());
        SaveAuthentificationState();
        return _currentUserStore.User.Auth;
    }

    public async Task SendEmailVerificationEmail() {
        if (_currentUserStore.User.Auth == null) {
            throw new Exception("User is not authenticated.");
        }

        await GetFreshAuthAsync();

        await _firebaseAuthProvider.SendEmailVerificationAsync(_currentUserStore.User.Auth.FirebaseToken);
    }

    private void SaveAuthentificationState() {
        string firebaseAuthLinkJson = JsonSerializer.Serialize(_currentUserStore.User.Auth);
        Properties.Settings.Default.FirebaseAuth = firebaseAuthLinkJson;
        Properties.Settings.Default.Save();
    }

    private void ClearAuthentificationState() {
        Properties.Settings.Default.FirebaseAuth = null;
        Properties.Settings.Default.Save();
    }
}