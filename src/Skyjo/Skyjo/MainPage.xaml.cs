using Microsoft.Maui.Controls;
using System;
namespace Skyjo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCreateGameClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(CreateGame));
        }

        private async void OnContinueGameClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ContinueGame));
        }

        private async void OnRulesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RulesPage));
        }
        
        private void CloseAppClicked(object sender, EventArgs e)
        {
            Application.Current?.Quit();
        }
    }
}
