using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp3.Services.IServices;

namespace WpfApp3
{
    /// <summary>
    /// Logique d'interaction pour ParcPage.xaml
    /// </summary>
    public partial class ParcPage : Window
    {
        private readonly IParcService _ParcService;

        public ParcPage()
        {
            var serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            _ParcService = serviceProvider.GetRequiredService<IParcService>();

            InitializeComponent();
            LoadParcs();
        }

        private async void LoadParcs()
        {
            var Parcs = await _ParcService.GetAllParcs();
            ParcList.ItemsSource = Parcs;
        }
    }
}
