using Microsoft.Extensions.DependencyInjection;
using platapp;
using platapp.Domain;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.Services.IServices;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;

        public MainWindow()
        {
            var serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            _authService = serviceProvider.GetRequiredService<IAuthService>();

            InitializeComponent();
            LogOpening();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = await _authService.Login(username, password);

            if (user != null)
            {
                MessageBox.Show($"Bienvenue, {user.username}!");
                // Rediriger vers une nouvelle fenêtre ou fonctionnalité

                HomePage homePage = new HomePage();
                homePage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("La connexion a échoué. Veuillez vérifier vos identifiants.");
            }
        }

        private void LogOpening()
        {
            // Code pour enregistrer l'ouverture de l'application dans le log
            using (var dbContext = new PContext()) // Remplacez YourDbContext par votre contexte de base de données
            {
                var log = new Log
                {
                    LastActivity = "Ouverture poste",
                    LastActivityDate = DateTime.Now
                };
                dbContext.Log.Add(log);
                dbContext.SaveChanges();
            }
        }
    }
}