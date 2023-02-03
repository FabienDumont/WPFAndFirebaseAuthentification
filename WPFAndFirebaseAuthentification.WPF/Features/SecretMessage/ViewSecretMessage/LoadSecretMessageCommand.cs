using System;
using System.Threading.Tasks;
using System.Windows;
using MVVMEssentials.Commands;
using WPFAndFirebaseAuthentification.Core.Responses;
using WPFAndFirebaseAuthentification.WPF.Features.SecretMessage.ViewSecretMessage;
using WPFAndFirebaseAuthentification.WPF.Queries;

namespace WPFAndFirebaseAuthentification.WPF.Commands; 

public class LoadSecretMessageCommand : BaseAsyncCommand {
    private readonly IViewSecretMessageViewModel _viewSecretMessageVm;
    private readonly IGetSecretMessageQuery _getSecretMessageQuery;
    
    public LoadSecretMessageCommand(IViewSecretMessageViewModel viewSecretMessageVm, IGetSecretMessageQuery getSecretMessageQuery) {
        _viewSecretMessageVm = viewSecretMessageVm;
        _getSecretMessageQuery = getSecretMessageQuery;
    }

    protected override async Task ExecuteAsync(object? parameter) {

        try {
            MessageResponse messageResponse = await _getSecretMessageQuery.Execute();

            _viewSecretMessageVm.SecretMessage = messageResponse.Message;
        } catch (Exception e) {
            MessageBox.Show("Unable to load data from API. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}