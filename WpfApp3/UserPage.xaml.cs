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
    /// Logique d'interaction pour UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        private readonly IUserService _UserService;

        public UserPage()
        {
            var serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            _UserService = serviceProvider.GetRequiredService<IUserService>();

            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var Users = await _UserService.GetAllUsers();
            UserList.ItemsSource = Users;
        }
    }
}
