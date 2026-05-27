using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Skyjo
{
    public partial class CreateGame : ContentPage
    {
        List<Entry> playerEntries;

        public CreateGame()
        {
            InitializeComponent();
            playerEntries = new List<Entry> { PlayerEntry1, PlayerEntry2, PlayerEntry3, PlayerEntry4 };
            foreach (var e in playerEntries)
            {
                if (e != null) e.TextChanged += (_, _) => UpdateUI();
            }
            UpdateUI();
        }

        void OnAddPlayerClicked(object sender, EventArgs e)
        {
            if (!PlayerRow3.IsVisible)
            {
                PlayerRow3.IsVisible = true;
                PlayerEntry3.Focus();
            }
            else if (!PlayerRow4.IsVisible)
            {
                PlayerRow4.IsVisible = true;
                PlayerEntry4.Focus();
            }
            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            UpdateUI();
        }

        void OnRemovePlayerClicked(object sender, EventArgs e)
        {
            if (sender is Button b && int.TryParse(b.CommandParameter?.ToString(), out int idx))
            {
                switch (idx)
                {
                    case 1:
                        PlayerEntry1.Text = string.Empty;
                        break;
                    case 2:
                        PlayerEntry2.Text = string.Empty;
                        break;
                    case 3:
                        PlayerEntry3.Text = string.Empty;
                        PlayerRow3.IsVisible = false;
                        break;
                    case 4:
                        PlayerEntry4.Text = string.Empty;
                        PlayerRow4.IsVisible = false;
                        break;
                }
            }
            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            UpdateUI();
        }

        async void OnStartClicked(object sender, EventArgs e)
        {
            var game = new ClassLibrary.Game();
            foreach (var entry in playerEntries)
            {
                if (entry != null && entry.IsVisible && !string.IsNullOrWhiteSpace(entry.Text))
                {
                    game.AddPlayer(entry.Text.Trim());
                }
            }

            if (game.Players.Count == 0)
            {
                await DisplayAlert("Erreur", "Ajoutez au moins un joueur avant de démarrer.", "OK");
                return;
            }

            GameSession.CurrentSession = game;
            await Shell.Current.GoToAsync(nameof(GamePage));
            
        }

        void UpdateUI()
        {
            int count = 0;
            foreach (var entry in playerEntries)
            {
                if (entry != null && entry.IsVisible && !string.IsNullOrWhiteSpace(entry.Text)) count++;
            }

            StartButton.IsEnabled = count >= 1 && count <= 4;
            StartButton.Opacity = StartButton.IsEnabled ? 1 : 0.4;

            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            AddPlayerButton.Opacity = AddPlayerButton.IsEnabled ? 1 : 0.4;
        }
    }
}
