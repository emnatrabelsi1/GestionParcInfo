using System.Text;

using Microsoft.Identity.Client;

namespace App1.Core.Contracts.Services;

public interface IIdentityCacheService
{
    void SaveMsalToken(byte[] token);

    byte[] ReadMsalToken();
}
