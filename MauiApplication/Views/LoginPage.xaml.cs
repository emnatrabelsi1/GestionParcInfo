using MauiApplication.Services.IServices;

namespace MauiApplication;

public partial class LoginPage : ContentPage
{
    private readonly IAuthService _authService;

    public LoginPage()
    {
        InitializeComponent();
        _authService = App.ServiceProvider.GetRequiredService<IAuthService>();
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // Exécutez les appels réseau sur un thread en arrière-plan
        var user = await Task.Run(() => _authService.Login(username, password));

        if (user != null)
        {
            await DisplayAlert("Success", $"Bienvenue, {user.username}!", "OK");
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            MessageLabel.Text = "La connexion a échoué. Veuillez vérifier vos identifiants.";
            MessageLabel.IsVisible = true;
        }
    }

}
