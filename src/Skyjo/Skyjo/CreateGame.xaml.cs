using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Skyjo
{
    public partial class CreateGame : ContentPage
    {
        // true = joueur humain, false = IA
        private readonly bool[] _isHuman = { true, true, true, true };

        private List<Entry> playerEntries;
        private List<Button> toggleBtns;
        private List<Label> typeLabels;

        public CreateGame()
        {
            InitializeComponent();

            playerEntries = new List<Entry> { PlayerEntry1, PlayerEntry2, PlayerEntry3, PlayerEntry4 };
            toggleBtns    = new List<Button> { ToggleBtn1, ToggleBtn2, ToggleBtn3, ToggleBtn4 };
            typeLabels    = new List<Label>  { TypeLabel1, TypeLabel2, TypeLabel3, TypeLabel4 };

            foreach (var e in playerEntries)
                if (e != null) e.TextChanged += (_, _) => UpdateUI();

            UpdateUI();
        }

        // --- Toggle Joueur / IA ---
        void OnToggleTypeClicked(object sender, EventArgs e)
        {
            if (sender is Button b && int.TryParse(b.CommandParameter?.ToString(), out int idx))
            {
                int i = idx - 1;
                _isHuman[i] = !_isHuman[i];

                toggleBtns[i].Text  = _isHuman[i] ? "👤" : "🤖";
                typeLabels[i].Text  = _isHuman[i] ? "J"  : "IA";

                playerEntries[i].IsEnabled   = _isHuman[i];
                playerEntries[i].Opacity     = _isHuman[i] ? 1 : 0.4;
                if (!_isHuman[i]) playerEntries[i].Text = string.Empty;
            }
            UpdateUI();
        }

        // --- Ajouter une ligne ---
        void OnAddPlayerClicked(object sender, EventArgs e)
        {
            if (!PlayerRow3.IsVisible)
            {
                PlayerRow3.IsVisible = true;
                // reset au type joueur par défaut
                ResetRow(3);
                PlayerEntry3.Focus();
            }
            else if (!PlayerRow4.IsVisible)
            {
                PlayerRow4.IsVisible = true;
                ResetRow(4);
                PlayerEntry4.Focus();
            }
            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            UpdateUI();
        }

        // --- Supprimer une ligne ---
        void OnRemovePlayerClicked(object sender, EventArgs e)
        {
            if (sender is Button b && int.TryParse(b.CommandParameter?.ToString(), out int idx))
            {
                int i = idx - 1;
                playerEntries[i].Text = string.Empty;

                if (idx == 3)
                {
                    PlayerRow3.IsVisible = false;
                    ResetRow(3);
                }
                else if (idx == 4)
                {
                    PlayerRow4.IsVisible = false;
                    ResetRow(4);
                }
                // Les lignes 1 et 2 ne se cachent pas, on efface juste le nom
            }
            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            UpdateUI();
        }

        // --- Démarrer ---
        async void OnStartClicked(object sender, EventArgs e)
        {
            var game = new ClassLibrary.Game();

            var rows = new List<(bool visible, int idx)>
            {
                (true,                   0),
                (true,                   1),
                (PlayerRow3.IsVisible,   2),
                (PlayerRow4.IsVisible,   3),
            };

            foreach (var (visible, i) in rows)
            {
                if (!visible) continue;

                if (_isHuman[i])
                {
                    string name = playerEntries[i].Text?.Trim() ?? string.Empty;
                    if (!string.IsNullOrEmpty(name))
                        game.AddPlayer(name);
                }
                else
                {
                    game.AddIA();
                }
            }

            if (game.Players.Count + game.IAs.Count == 0)
            {
                await DisplayAlert("Erreur", "Ajoutez au moins un joueur ou une IA avant de démarrer.", "OK");
                return;
            }

            GameSession.CurrentSession = game;
            GameSession.CurrentSaveFile = null;
            await Shell.Current.GoToAsync(nameof(GamePage));
        }

        // --- UI helpers ---
        void UpdateUI()
        {
            int count = 0;

            // Lignes 1 & 2 toujours visibles
            for (int i = 0; i < 2; i++)
                count += SlotCounts(i);

            if (PlayerRow3.IsVisible) count += SlotCounts(2);
            if (PlayerRow4.IsVisible) count += SlotCounts(3);

            StartButton.IsEnabled = count >= 1;
            StartButton.Opacity   = StartButton.IsEnabled ? 1 : 0.4;

            AddPlayerButton.IsEnabled = !(PlayerRow3.IsVisible && PlayerRow4.IsVisible);
            AddPlayerButton.Opacity   = AddPlayerButton.IsEnabled ? 1 : 0.4;
        }

        // Un slot compte si : c'est une IA, ou c'est un humain avec un nom
        int SlotCounts(int i) =>
            !_isHuman[i] ? 1 :
            !string.IsNullOrWhiteSpace(playerEntries[i].Text) ? 1 : 0;

        void ResetRow(int idx)
        {
            int i = idx - 1;
            _isHuman[i]            = true;
            toggleBtns[i].Text     = "👤";
            typeLabels[i].Text     = "J";
            playerEntries[i].IsEnabled = true;
            playerEntries[i].Opacity   = 1;
        }
    }
}
