using Microsoft.EntityFrameworkCore;
using platapp.Domain;
using platapp.ServicesAPI.IServicesAPI;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace platapp.ServicesAPI
{
    public class LogServiceAPI : ILogServiceAPI
    {
        private readonly PContext _pContext;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogServiceAPI(PContext context, IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _pContext = context;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateLog(string activity)
        {
            var user = await _authService.GetAuthenticatedUser();

            var log = new Log
            {
                LastActivity = activity,
                LastActivityDate = DateTime.Now,
                UtilisateurFk = user?.Id
            };

            await _pContext.Log.AddAsync(log);
            await _pContext.SaveChangesAsync();
        }
    }
}
