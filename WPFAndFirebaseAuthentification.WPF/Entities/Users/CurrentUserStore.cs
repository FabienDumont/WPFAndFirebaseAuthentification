using Firebase.Auth;

namespace WPFAndFirebaseAuthentification.WPF.Entities.Users; 

public class CurrentUserStore {
    public User User { get; private set; }
    
    public CurrentUserStore() {
        User = new User();
    }

    public void UpdateAuth(FirebaseAuthLink auth) {
        User = new User(auth);
    }
}