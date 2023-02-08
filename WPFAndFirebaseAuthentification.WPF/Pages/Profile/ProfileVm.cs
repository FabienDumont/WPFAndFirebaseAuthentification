using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Entities.Users;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.ViewProfile;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class ProfileVm : BaseVm {
    public ProfileDetailsVm ProfileDetailsVm { get; }

    public ProfileVm(AuthenticationStore authenticationStore, CurrentUserStore currentUserStore, INavigationService homeNavigationService) {
        ProfileDetailsVm = new ProfileDetailsVm(authenticationStore, currentUserStore, homeNavigationService);
    }
}