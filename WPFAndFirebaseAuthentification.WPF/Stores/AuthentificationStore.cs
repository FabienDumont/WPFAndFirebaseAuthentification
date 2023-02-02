using System;
using System.Text.Json;
using System.Threading.Tasks;
using Firebase.Auth;

namespace WPFAndFirebaseAuthentification.WPF.Stores; 

public class AuthentificationStore {
    private FirebaseAuthProvider _firebaseAuthProvider;
    private FirebaseAuthLink? _currentFirebaseAuthLink;

    public AuthentificationStore(FirebaseAuthProvider firebaseAuthProvider) {
        _firebaseAuthProvider = firebaseAuthProvider;
    }
    public User? CurrentUser => _currentFirebaseAuthLink?.User;
    public bool IsLoggedIn => (!_currentFirebaseAuthLink?.IsExpired()) ?? false;

    public async Task Initialize() {
        string firebaseAuthJson = Properties.Settings.Default.FirebaseAuth;

        if (string.IsNullOrEmpty(firebaseAuthJson)) {
            return;
        }
        
        FirebaseAuth? firebaseAuth = JsonSerializer.Deserialize<FirebaseAuth>(firebaseAuthJson);

        if (firebaseAuth == null) {
            return;
        }

        _currentFirebaseAuthLink = new FirebaseAuthLink(_firebaseAuthProvider, firebaseAuth);

        await GetFreshAuthAsync();
    }

    public async Task Login(string email, string password) {
        _currentFirebaseAuthLink = await _firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
        SaveAuthentificationState();
    }

    public void Logout() {
        _currentFirebaseAuthLink = null;
        
        ClearAuthentificationState();
    }

    public async Task<FirebaseAuthLink?> GetFreshAuthAsync() {
        if (_currentFirebaseAuthLink != null) {
            _currentFirebaseAuthLink = await _currentFirebaseAuthLink.GetFreshAuthAsync();
            SaveAuthentificationState();
            return _currentFirebaseAuthLink;
        }

        return null;
    }

    public async Task SendEmailVerificationEmail() {

        if (_currentFirebaseAuthLink == null) {
            throw new Exception("User is not authenticated.");
        }

        await GetFreshAuthAsync();

        await _firebaseAuthProvider.SendEmailVerificationAsync(_currentFirebaseAuthLink.FirebaseToken);
    }
    
    private void SaveAuthentificationState() {
        string firebaseAuthLinkJson = JsonSerializer.Serialize(_currentFirebaseAuthLink);
        Properties.Settings.Default.FirebaseAuth = firebaseAuthLinkJson;
        Properties.Settings.Default.Save();
    }
    
    private void ClearAuthentificationState() {
        Properties.Settings.Default.FirebaseAuth = null;
        Properties.Settings.Default.Save();
    }
}