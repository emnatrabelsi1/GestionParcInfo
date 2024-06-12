using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp3.Services;
using WpfApp3.Services.IServices;

namespace WpfApp3
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configurer HttpClient pour les appels API
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });
            services.AddScoped<IEtablissementService, EtablissementService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IParcService, ParcService>();
            // Ajouter les services d'authentification
            services.AddScoped<IAuthService, AuthService>();

            return services.BuildServiceProvider();
        }
    }
}