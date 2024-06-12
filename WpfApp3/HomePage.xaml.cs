using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp3
{
    public partial class HomePage : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public HomePage()
        {
            _serviceProvider = ServiceCollectionExtensions.ConfigureServices();
            InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuList.Visibility == Visibility.Collapsed)
            {
                MenuList.Visibility = Visibility.Visible;
            }
            else
            {
                MenuList.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuList.SelectedItem is ListBoxItem selectedItem)
            {
                switch (selectedItem.Tag.ToString())
                {
                    case "1":
                        var etablissementPage = new EtablissementPage();
                        etablissementPage.Show();
                        this.Close();
                        break;

                    case "4":
                        var logPage = new LogPage();
                        logPage.Show();
                        this.Close();
                        break;

                    // Vous pouvez ajouter des actions pour d'autres items de menu ici
                    default:
                        // Optional: handle cases where the tag is not "1" or "4"
                        break;
                }
            }
        }
    }
}
