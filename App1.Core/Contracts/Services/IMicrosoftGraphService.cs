using App1.Core.Models;

namespace App1.Core.Contracts.Services;

public interface IMicrosoftGraphService
{
    Task<User> GetUserInfoAsync(string accessToken);

    Task<string> GetUserPhoto(string accessToken);
}
