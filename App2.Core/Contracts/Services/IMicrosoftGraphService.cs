using App2.Core.Models;

namespace App2.Core.Contracts.Services;

public interface IMicrosoftGraphService
{
    Task<User> GetUserInfoAsync(string accessToken);

    Task<string> GetUserPhoto(string accessToken);
}
