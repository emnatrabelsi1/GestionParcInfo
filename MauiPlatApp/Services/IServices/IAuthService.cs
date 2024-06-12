using platapp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPlatApp.Services.IServices
{
    public interface IAuthService
    {
        Task<Utilisateur> Login(string username, string password);
        Task<Utilisateur> GetUser();
    }
}
