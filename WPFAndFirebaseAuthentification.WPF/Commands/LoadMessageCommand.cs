using System;
using System.Threading.Tasks;
using System.Windows;
using MVVMEssentials.Commands;
using WPFAndFirebaseAuthentification.Core.Responses;
using WPFAndFirebaseAuthentification.WPF.MVVM.ViewModels;
using WPFAndFirebaseAuthentification.WPF.Queries;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class LoadMessageCommand : BaseAsyncCommand {
    private readonly HomeVm _homeVm;
    private readonly IGetMessageQuery _getMessageQuery;
    
    public LoadMessageCommand(HomeVm homeVm, IGetMessageQuery getMessageQuery) {
        _homeVm = homeVm;
        _getMessageQuery = getMessageQuery;
    }

    protected override async Task ExecuteAsync(object? parameter) {

        try {
            MessageResponse messageResponse = await _getMessageQuery.Execute();

            _homeVm.Message = messageResponse.Message;
        } catch (Exception e) {
            MessageBox.Show("Unable to load data from API. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}