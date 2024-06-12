using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorApp1.Services;
using BlazorApp1.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using platapp.ServicesAPI;

var builder = WebApplication.CreateBuilder(args);

// Ajouter des services au conteneur.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });
builder.Services.AddScoped<IParcService, ParcService>();
builder.Services.AddScoped<IEtablissementService, EtablissementService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPosteService, PosteService>();
builder.Services.AddScoped<ISalleService, SalleService>();
builder.Services.AddScoped<ILogService, LogService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<BlazorApp1.Services.AuthService>();
builder.Services.AddScoped<IAuthService, BlazorApp1.Services.AuthService>();

var app = builder.Build();

// Configurer le pipeline de requêtes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
