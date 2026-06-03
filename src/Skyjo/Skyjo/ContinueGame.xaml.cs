using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using ClassLibrary;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Skyjo
{
    public partial class ContinueGame : ContentPage
    {
        static string SaveDirectory => FileSystem.Current.AppDataDirectory;

        public ContinueGame()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadSaves();
        }

        void LoadSaves()
        {
            while (SavesList.Children.Count > 1)
                SavesList.Children.RemoveAt(0);

            var saveFiles = Directory.GetFiles(SaveDirectory, "save_*.xml")
                .OrderByDescending(f => f)
                .ToList();

            if (saveFiles.Count == 0)
            {
                EmptyLabel.IsVisible = true;
                return;
            }

            EmptyLabel.IsVisible = false;

            foreach (var filePath in saveFiles)
            {
                Game? game = null;
                try
                {
                    var serializer = new DataContractSerializer(typeof(Game));
                    using (Stream s = File.OpenRead(filePath))
                        game = serializer.ReadObject(s) as Game;
                }
                catch { }

                if (game == null) continue;

                var names = game.Players.Select(p => p._name)
                    .Concat(game.IAs.Select(ia => ia._name + " (IA)"))
                    .ToList();
                string playersText = "👥 " + string.Join(", ", names);

                string saveDate = ParseDateFromFileName(filePath);
                var card = BuildSaveCard(playersText, saveDate, game, filePath);
                SavesList.Children.Insert(SavesList.Children.Count - 1, card);
            }
        }

        string ParseDateFromFileName(string filePath)
        {
            try
            {
                string name = Path.GetFileNameWithoutExtension(filePath); // save_20240115_143000
                string[] parts = name.Split('_');
                if (parts.Length >= 3)
                {
                    string datePart = parts[1]; // 20240115
                    string timePart = parts[2]; // 143000
                    string formatted = $"{datePart[6..8]}/{datePart[4..6]}/{datePart[..4]} à {timePart[..2]}:{timePart[2..4]}";
                    return formatted;
                }
            }
            catch { }
            return "Date inconnue";
        }

        Border BuildSaveCard(string playersText, string saveDate, Game game, string filePath)
        {
            var nameLabel = new Label
            {
                Text = $"Partie du {saveDate}",
                TextColor = Colors.White,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold
            };
            var playersLabel = new Label
            {
                Text = playersText,
                TextColor = Color.FromArgb("#88c39bd3"),
                FontSize = 12
            };

            var info = new VerticalStackLayout
            {
                Spacing = 4,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, playersLabel }
            };

            var deleteBorder = new Border
            {
                StrokeThickness = 0,
                WidthRequest = 40,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Center
            };
            deleteBorder.Background = new SolidColorBrush(Color.FromArgb("#339b59b6"));
            deleteBorder.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(50) };
            deleteBorder.Content = new Label
            {
                Text = "✕",
                TextColor = Color.FromArgb("#c39bd3"),
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            deleteBorder.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await OnDeleteSave(filePath))
            });

            var row = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection(
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)),
                Padding = new Thickness(20, 16)
            };
            Grid.SetColumn(info, 0);
            Grid.SetColumn(deleteBorder, 1);
            row.Children.Add(info);
            row.Children.Add(deleteBorder);

            var card = new Border
            {
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Color.FromArgb("#44c39bd3")),
                Content = row
            };
            card.Background = new SolidColorBrush(Color.FromArgb("#1acc99ff"));
            card.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(18) };

            card.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    GameSession.CurrentSession = game;
                    GameSession.CurrentSaveFile = filePath;
                    await Shell.Current.GoToAsync(nameof(GamePage));
                })
            });

            return card;
        }

        async Task OnDeleteSave(string filePath)
        {
            bool confirm = await DisplayAlert("Supprimer", "Supprimer cette sauvegarde ?", "Oui", "Annuler");
            if (!confirm) return;

            if (File.Exists(filePath))
                File.Delete(filePath);

            LoadSaves();
        }
    }
}
