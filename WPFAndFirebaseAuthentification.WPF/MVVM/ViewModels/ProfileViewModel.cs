using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels; 

public class ProfileViewModel : BaseVm {

    private readonly AuthentificationStore _authentificationStore;

    public string Username => _authentificationStore.CurrentUser?.DisplayName ?? string.Empty;
    public string Email => _authentificationStore.CurrentUser?.Email ?? string.Empty;
    public bool IsEmailVerified => _authentificationStore.CurrentUser?.IsEmailVerified ?? false;

    public ProfileViewModel(AuthentificationStore authentificationStore) {
        _authentificationStore = authentificationStore;
    }

}