using Firebase.Auth;

namespace WPFAndFirebaseAuthentification.WPF.Entities.Users; 

public class User {
    public FirebaseAuthLink? Auth { get; }

    public string DisplayName => Auth?.User?.DisplayName;
    public string Email => Auth?.User?.Email;
    public bool IsEmailVerified => Auth?.User?.IsEmailVerified ?? false;
    
    public bool IsLoggedIn => (!Auth?.IsExpired()) ?? false;
    public User(FirebaseAuthLink auth) {
        Auth = auth;
    }

    public User() {
        
    }
}