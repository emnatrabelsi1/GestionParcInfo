using App1.Core.Contracts.Services;
using App1.Core.Helpers;
using App1.Properties;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App1.ViewModels;

public class LogInViewModel : ObservableObject
{
    private readonly IIdentityService _identityService;
    private string _statusMessage;
    private bool _isBusy;
    private RelayCommand _loginCommand;

    public string StatusMessage
    {
        get => _statusMessage;
        set => SetProperty(ref _statusMessage, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            SetProperty(ref _isBusy, value);
            LoginCommand.NotifyCanExecuteChanged();
        }
    }

    public RelayCommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(OnLogin, () => !IsBusy));

    public LogInViewModel(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    private async void OnLogin()
    {
        IsBusy = true;
        StatusMessage = string.Empty;
        var loginResult = await _identityService.LoginAsync();
        StatusMessage = GetStatusMessage(loginResult);
        IsBusy = false;
    }

    private string GetStatusMessage(LoginResultType loginResult)
    {
        switch (loginResult)
        {
            case LoginResultType.Unauthorized:
                return Resources.StatusUnauthorized;
            case LoginResultType.NoNetworkAvailable:
                return Resources.StatusNoNetworkAvailable;
            case LoginResultType.UnknownError:
                return Resources.StatusLoginFails;
            case LoginResultType.Success:
            case LoginResultType.CancelledByUser:
                return string.Empty;
            default:
                return string.Empty;
        }
    }
}
