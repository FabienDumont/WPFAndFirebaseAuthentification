using System;
using System.Threading.Tasks;
using System.Windows;
using MVVMEssentials.Commands;
using WPFAndFirebaseAuthentification.Core.Responses;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Queries;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class LoadSecretMessageCommand : BaseAsyncCommand {
    private readonly HomeVm _homeVm;
    private readonly IGetSecretMessageQuery _getSecretMessageQuery;
    
    public LoadSecretMessageCommand(HomeVm homeVm, IGetSecretMessageQuery getSecretMessageQuery) {
        _homeVm = homeVm;
        _getSecretMessageQuery = getSecretMessageQuery;
    }

    protected override async Task ExecuteAsync(object? parameter) {

        try {
            MessageResponse messageResponse = await _getSecretMessageQuery.Execute();

            _homeVm.Message = messageResponse.Message;
        } catch (Exception e) {
            MessageBox.Show("Unable to load data from API. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}