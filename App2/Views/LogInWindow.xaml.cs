using App2.Contracts.Views;
using App2.ViewModels;

using MahApps.Metro.Controls;

namespace App2.Views;

public partial class LogInWindow : MetroWindow, ILogInWindow
{
    public LogInWindow(LogInViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();
}
