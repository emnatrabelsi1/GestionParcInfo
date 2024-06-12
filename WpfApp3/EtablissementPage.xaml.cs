using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp3.Services.IServices;

namespace WpfApp3
{
    public partial class EtablissementPage : Window
    {
        private readonly IEtablissementService _etablissementService;

        public EtablissementPage()
        {
            var serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            _etablissementService = serviceProvider.GetRequiredService<IEtablissementService>();

            InitializeComponent();
            LoadEtablissements();
        }

        private async void LoadEtablissements()
        {
            var etablissements = await _etablissementService.GetAllEtablissements();
            EtablissementList.ItemsSource = etablissements;
        }
    }
}
