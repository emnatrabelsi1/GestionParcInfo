﻿using System.IO;

using App1.Contracts.Services;
using App1.Core.Contracts.Services;
using App1.Core.Models;
using App1.Helpers;
using App1.Models;
using App1.ViewModels;

using Microsoft.Extensions.Options;

namespace App1.Services;

public class UserDataService : IUserDataService
{
    private readonly IFileService _fileService;
    private readonly IIdentityService _identityService;
    private readonly IMicrosoftGraphService _microsoftGraphService;
    private readonly AppConfig _appConfig;
    private readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private UserViewModel _user;

    public event EventHandler<UserViewModel> UserDataUpdated;

    public UserDataService(IFileService fileService, IIdentityService identityService, IMicrosoftGraphService microsoftGraphService, IOptions<AppConfig> appConfig)
    {
        _fileService = fileService;
        _identityService = identityService;
        _microsoftGraphService = microsoftGraphService;
        _appConfig = appConfig.Value;
    }

    public void Initialize()
    {
        _identityService.LoggedIn += OnLoggedIn;
        _identityService.LoggedOut += OnLoggedOut;
    }

    public UserViewModel GetUser()
    {
        if (_user == null)
        {
            _user = GetUserFromCache();
            if (_user == null)
            {
                _user = GetDefaultUserData();
            }
        }

        return _user;
    }

    private async void OnLoggedIn(object sender, EventArgs e)
    {
        _user = await GetUserFromGraphApiAsync();
        UserDataUpdated?.Invoke(this, _user);
    }

    private void OnLoggedOut(object sender, EventArgs e)
    {
        _user = null;
        var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        var fileName = _appConfig.UserFileName;
        _fileService.Save<User>(folderPath, fileName, null);
    }

    private UserViewModel GetUserFromCache()
    {
        var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
        var fileName = _appConfig.UserFileName;
        var cacheData = _fileService.Read<User>(folderPath, fileName);
        return GetUserViewModelFromData(cacheData);
    }

    private async Task<UserViewModel> GetUserFromGraphApiAsync()
    {
        var accessToken = await _identityService.GetAccessTokenForGraphAsync();
        if (string.IsNullOrEmpty(accessToken))
        {
            return null;
        }

        var userData = await _microsoftGraphService.GetUserInfoAsync(accessToken);
        if (userData != null)
        {
            userData.Photo = await _microsoftGraphService.GetUserPhoto(accessToken);
            var folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder);
            var fileName = _appConfig.UserFileName;
            _fileService.Save<User>(folderPath, fileName, userData);
        }

        return GetUserViewModelFromData(userData);
    }

    private UserViewModel GetUserViewModelFromData(User userData)
    {
        if (userData == null)
        {
            return null;
        }

        var userPhoto = string.IsNullOrEmpty(userData.Photo)
            ? ImageHelper.ImageFromAssetsFile("DefaultIcon.png")
            : ImageHelper.ImageFromString(userData.Photo);

        return new UserViewModel()
        {
            Name = userData.DisplayName,
            UserPrincipalName = userData.UserPrincipalName,
            Photo = userPhoto
        };
    }

    private UserViewModel GetDefaultUserData()
    {
        return new UserViewModel()
        {
            Name = _identityService.GetAccountUserName(),
            Photo = ImageHelper.ImageFromAssetsFile("DefaultIcon.png")
        };
    }
}
