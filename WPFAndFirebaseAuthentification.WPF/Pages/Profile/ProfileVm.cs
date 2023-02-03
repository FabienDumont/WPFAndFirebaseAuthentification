using System.Windows.Input;
using MVVMEssentials.Commands;
using MVVMEssentials.Services;
using MVVMEssentials.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Commands;
using WPFAndFirebaseAuthentification.WPF.Features.Authentication.ViewProfile;
using WPFAndFirebaseAuthentification.WPF.Stores;

namespace WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;

public class ProfileVm : BaseVm {
    public ProfileDetailsVm ProfileDetailsVm { get; }

    public ProfileVm(AuthenticationStore authenticationStore, INavigationService homeNavigationService) {
        ProfileDetailsVm = new ProfileDetailsVm(authenticationStore, homeNavigationService);
    }
}