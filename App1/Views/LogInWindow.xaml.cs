using App1.Contracts.Views;
using App1.ViewModels;

using MahApps.Metro.Controls;

namespace App1.Views;

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
