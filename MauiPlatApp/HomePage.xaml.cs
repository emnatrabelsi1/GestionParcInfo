namespace MauiPlatApp;
using MauiPlatApp.Services.IServices;
public partial class HomePage : ContentPage
{
    private readonly IAuthService _authService;

    public HomePage()
    {
        InitializeComponent();
        _authService = App.ServiceProvider.GetRequiredService<IAuthService>();
    }

    private void HamburgerButton_Clicked(object sender, EventArgs e)
    {
        MenuList.IsVisible = !MenuList.IsVisible;
    }

    private async void MenuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count > 0)
        {
            var selectedItem = e.CurrentSelection[0] as string;
            switch (selectedItem)
            {
                case "Voir la liste des établissements":
                    WelcomeText.IsVisible = false;
                    EtablissementList.IsVisible = true;
                    // Load data into EtablissementList
                    break;
                case "Voir la liste des parcs":
                    // Handle showing parks list
                    break;
                case "Voir la liste des utilisateurs":
                    // Handle showing users list
                    break;
                case "Voir la liste des logs":
                    // Handle showing logs list
                    break;
                case "Voir la liste des salles":
                    // Handle showing rooms list
                    break;
                case "Voir la liste des postes":
                    // Handle showing posts list
                    break;
                case "Déconnexion":
                    await Navigation.PopToRootAsync();
                    break;
            }
            MenuList.SelectedItem = null;
        }
    }
}
