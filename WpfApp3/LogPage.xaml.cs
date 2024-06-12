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
    /// Logique d'interaction pour LogPage.xaml
    /// </summary>
    public partial class LogPage : Window
    {
        private readonly ILogService _LogService;

        public LogPage()
        {
            var serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            _LogService = serviceProvider.GetRequiredService<ILogService>();

            InitializeComponent();
            LoadLogs();
        }

        private async void LoadLogs()
        {
            var Logs = await _LogService.GetAllLogs();
            LogList.ItemsSource = Logs;
        }
    }
}
