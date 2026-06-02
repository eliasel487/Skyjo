using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using ClassLibrary;

namespace Skyjo;

public partial class GamePage : ContentPage
{
    // ── État de la partie ──────────────────────────────────────────────
    Game _game => GameSession.CurrentSession;

    // Liste unifiée : Player ou IA dans l'ordre de jeu
    List<Player> _turnOrder = new();
    int _currentIndex = 0;

    // Carte en main (piochée ou prise de la défausse)
    Card? _cardInHand = null;
    bool _mustPlaceCard = false;   // true = on a une carte, on doit la placer ou retourner

    // ── Init ───────────────────────────────────────────────────────────
    public GamePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        BuildTurnOrder();
        RefreshAll();
    }

    void BuildTurnOrder()
    {
        _turnOrder.Clear();
        // Joueurs humains d'abord, puis IAs
        foreach (var p in _game.Players) _turnOrder.Add(p);
        foreach (var ia in _game.IAs)    _turnOrder.Add(ia);
        _currentIndex = 0;
    }

    Player CurrentPlayer => _turnOrder[_currentIndex];

    // ── Rafraîchissement global ────────────────────────────────────────
    void RefreshAll()
    {
        RefreshHeader();
        RefreshOpponents();
        RefreshPlayerBoard();
        RefreshDiscard();
        RefreshActionButtons();
    }

    void RefreshHeader()
    {
        var p = CurrentPlayer;
        string name = p._name ?? "IA";
        CurrentPlayerName.Text    = name;
        CurrentPlayerInitial.Text = name.Length >= 2 ? name[..2].ToUpper() : name.ToUpper();
        p.CountRoundScore();
        PlayerScoreLabel.Text = p._roundScore.ToString();
        TurnBadgeLabel.Text   = p.GetIA() ? $"🤖  Tour de {name}" : $"✦  Ton tour, {name}";
        PlayerStatusLabel.Text = "Pioche une carte ou prends la défausse";
    }

    // ── Adversaires (mini grilles) ─────────────────────────────────────
    void RefreshOpponents()
    {
        OpponentsGrid.Children.Clear();
        OpponentsGrid.ColumnDefinitions.Clear();

        var opponents = _turnOrder
            .Where((p, i) => i != _currentIndex)
            .ToList();

        // Reconstruire les colonnes
        for (int i = 0; i < opponents.Count; i++)
            OpponentsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        for (int col = 0; col < opponents.Count; col++)
        {
            var p = opponents[col];
            p.CountRoundScore();
            string name = p._name ?? "IA";
            string initials = name.Length >= 2 ? name[..2].ToUpper() : name.ToUpper();

            var stack = new VerticalStackLayout { Spacing = 8, HorizontalOptions = LayoutOptions.Center };

            // Avatar
            var avatarBorder = MakeAvatarBorder(initials, p.GetIA());
            stack.Children.Add(avatarBorder);

            // Nom
            stack.Children.Add(new Label
            {
                Text = name,
                TextColor = Color.FromArgb("#c39bd3"),
                FontSize = 10,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });

            // Mini grille
            stack.Children.Add(BuildMiniGrid(p));

            // Score
            stack.Children.Add(new Label
            {
                Text = p._roundScore.ToString(),
                TextColor = Colors.White,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });
            stack.Children.Add(new Label
            {
                Text = "pts manche",
                TextColor = Color.FromArgb("#88c39bd3"),
                FontSize = 9,
                HorizontalOptions = LayoutOptions.Center
            });

            var card = new Border
            {
                StrokeThickness = 1,
                Stroke = new SolidColorBrush(Color.FromArgb("#33c39bd3")),
                Padding = new Thickness(10),
                Content = stack
            };
            card.Background = new SolidColorBrush(Color.FromArgb("#3d2d1050"));
            card.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(16) };

            Grid.SetColumn(card, col);
            OpponentsGrid.Children.Add(card);
        }
    }

    Border MakeAvatarBorder(string initials, bool isIA)
    {
        var border = new Border { WidthRequest = 32, HeightRequest = 32, StrokeThickness = 0 };
        border.Background = new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Color.FromArgb(isIA ? "#448844" : "#9b59b6"), 0f),
                new GradientStop(Color.FromArgb(isIA ? "#66cc66" : "#c39bd3"), 1f)
            },
            new Point(0, 0), new Point(1, 1));
        border.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(16) };
        border.Content = new Label
        {
            Text = initials,
            TextColor = Colors.White,
            FontSize = 11,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        return border;
    }

    Grid BuildMiniGrid(Player p)
    {
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition(new GridLength(14)),
                new ColumnDefinition(new GridLength(14)),
                new ColumnDefinition(new GridLength(14)),
                new ColumnDefinition(new GridLength(14))),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition(new GridLength(18)),
                new RowDefinition(new GridLength(18)),
                new RowDefinition(new GridLength(18))),
            ColumnSpacing = 2,
            RowSpacing = 2,
            HorizontalOptions = LayoutOptions.Center
        };

        var columns = p.GetBoard().GetColumns();
        for (int col = 0; col < 4 && col < columns.Count; col++)
        {
            var cards = columns[col].GetCards();
            for (int row = 0; row < 3 && row < cards.Count; row++)
            {
                var card = cards[row];
                Color bg = card.IsFlipped()
                    ? CardBgColor(card.Number)
                    : Color.FromArgb("#2d1255");

                var box = new BoxView { Color = bg, CornerRadius = 2 };
                Grid.SetRow(box, row);
                Grid.SetColumn(box, col);
                grid.Children.Add(box);
            }
        }
        return grid;
    }

    // ── Plateau du joueur actuel (grand, clicable) ─────────────────────
    void RefreshPlayerBoard()
    {
        PlayerBoardGrid.Children.Clear();

        var p = CurrentPlayer;
        var columns = p.GetBoard().GetColumns();

        for (int col = 0; col < 4 && col < columns.Count; col++)
        {
            var cards = columns[col].GetCards();
            for (int row = 0; row < 3 && row < cards.Count; row++)
            {
                var card = cards[row];
                int capturedCol = col, capturedRow = row;

                var border = BuildLargeCardBorder(card);
                border.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OnCardTapped(capturedCol, capturedRow))
                });

                Grid.SetRow(border, row);
                Grid.SetColumn(border, col);
                PlayerBoardGrid.Children.Add(border);
            }
        }
    }

    Border BuildLargeCardBorder(Card card)
    {
        string text;
        Color bg, fg, stroke;

        if (!card.IsFlipped())
        {
            text   = "?";
            bg     = Color.FromArgb("#2d1255");
            fg     = Color.FromArgb("#44c39bd3");
            stroke = Color.FromArgb("#669b59b6");
        }
        else
        {
            text   = card.Number.ToString();
            bg     = CardBgColor(card.Number);
            fg     = CardTextColor(card.Number);
            stroke = bg;
        }

        var border = new Border
        {
            StrokeThickness = 2,
            Stroke = new SolidColorBrush(stroke),
            Padding = new Thickness(0)
        };
        border.Background = new SolidColorBrush(bg);
        border.StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = new CornerRadius(12) };
        border.Content = new Label
        {
            Text = text,
            TextColor = fg,
            FontSize = 26,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        return border;
    }

    // ── Défausse ───────────────────────────────────────────────────────
    void RefreshDiscard()
    {
        if (!_game.Deck.IsEmptyDiscard())
        {
            // GetFirstCardDiscard renvoie une string ; on extrait juste le chiffre
            string raw = _game.Deck.GetFirstCardDiscard(); // "La défausse contient : X"
            var parts = raw.Split(':');
            DiscardValueLabel.Text = parts.Length > 1 ? parts[1].Trim() : "?";
        }
        else
        {
            DiscardValueLabel.Text = "—";
        }
    }

    void RefreshActionButtons()
    {
        // Si une carte est en main, on désactive Piocher/Défausse
        bool canPick = !_mustPlaceCard;
        ActionButtonsGrid.Opacity = canPick ? 1 : 0.4;
        foreach (var child in ActionButtonsGrid.Children)
        {
            if (child is Border b && b.Content is Button btn)
            {
                if (btn.Text == "Piocher" || btn.Text == "Défausse")
                    btn.IsEnabled = canPick;
            }
        }
    }

    // ── Actions joueur ─────────────────────────────────────────────────

    async void OnDrawCardClicked(object sender, EventArgs e)
    {
        if (_mustPlaceCard) return;

        _cardInHand = Player.PickACard(_game.Deck, 0); // pioche aléatoire
        _cardInHand.FlipCard();
        _mustPlaceCard = true;

        PlayerStatusLabel.Text = $"Carte piochée : {_cardInHand.Number} — clique une case pour échanger, ou défausse-la";
        RefreshActionButtons();

        // Option : défausser la carte piochée et retourner une carte cachée
        // (cf. règles Skyjo : on peut aussi juste défausser + retourner)
    }

    async void OnTakeDiscardClicked(object sender, EventArgs e)
    {
        if (_mustPlaceCard) return;
        if (_game.Deck.IsEmptyDiscard())
        {
            await DisplayAlert("Défausse", "La défausse est vide !", "OK");
            return;
        }

        _cardInHand = Player.PickACard(_game.Deck, 1); // prend la défausse
        _cardInHand.FlipCard();
        _mustPlaceCard = true;

        PlayerStatusLabel.Text = $"Carte de la défausse : {_cardInHand.Number} — clique une case pour l'échanger";
        RefreshActionButtons();
    }

    void OnCardTapped(int col, int row)
    {
        if (!_mustPlaceCard)
        {
            // Pas de carte en main : retourner une carte cachée
            var p = CurrentPlayer;
            var card = p.GetBoard().GetColumns()[col].GetCards()[row];
            if (!card.IsFlipped())
            {
                p.GetBoard().ReturnACard(col, row);
                RefreshPlayerBoard();
                p.CountRoundScore();
                PlayerScoreLabel.Text = p._roundScore.ToString();
                // Retourner une carte sans carte en main = fin du tour
                EndTurn();
            }
            return;
        }

        // On a une carte en main : on l'échange avec la case cliquée
        CurrentPlayer.GetBoard().ReplaceCard(_game.Deck, col, row, _cardInHand!);
        _cardInHand = null;
        _mustPlaceCard = false;

        CurrentPlayer.RemoveColumn(); // vérifie colonnes identiques
        CurrentPlayer.CountRoundScore();
        PlayerScoreLabel.Text = CurrentPlayer._roundScore.ToString();

        RefreshDiscard();
        RefreshPlayerBoard();
        EndTurn();
    }

    // ── Fin de tour ────────────────────────────────────────────────────
    async void EndTurn()
    {
        // Vérifier si le joueur a tout retourné (fin de manche)
        if (CurrentPlayer.RoundOver())
        {
            await DisplayAlert("Manche terminée !",
                $"{CurrentPlayer._name} a retourné toutes ses cartes !\nCalcul des scores...", "OK");
            EndRound();
            return;
        }

        // Passer au joueur suivant
        _currentIndex = (_currentIndex + 1) % _turnOrder.Count;

        // Si c'est une IA, elle joue automatiquement
        if (CurrentPlayer is IA ia)
        {
            await Task.Delay(600);
            ia.PlayRound(_game.Deck);
            ia.RemoveColumn();
            RefreshDiscard();
            RefreshAll();

            if (ia.RoundOver())
            {
                await DisplayAlert("Manche terminée !",
                    $"L'IA a retourné toutes ses cartes !\nCalcul des scores...", "OK");
                EndRound();
                return;
            }

            _currentIndex = (_currentIndex + 1) % _turnOrder.Count;

            // Si le joueur suivant est aussi une IA, continuer
            while (CurrentPlayer is IA nextIA)
            {
                await Task.Delay(600);
                nextIA.PlayRound(_game.Deck);
                nextIA.RemoveColumn();
                RefreshDiscard();
                _currentIndex = (_currentIndex + 1) % _turnOrder.Count;
            }
        }

        _mustPlaceCard = false;
        _cardInHand    = null;
        RefreshAll();
    }

    // ── Fin de manche ──────────────────────────────────────────────────
    async void EndRound()
    {
        // Calculer les scores de tous
        foreach (var p in _turnOrder)
        {
            p.CountRoundScore();
            p.CountGameScore();
        }

        string scores = string.Join("\n",
            _turnOrder.Select(p => $"{p._name} : {p._roundScore} pts (total : {p._gameScore})"));

        await DisplayAlert("Scores de la manche", scores, "Continuer");

        // Vérifier si la partie est terminée (100 pts)
        var loser = _turnOrder.FirstOrDefault(p => p._gameScore >= 100);
        if (loser != null)
        {
            var winner = _turnOrder.OrderBy(p => p._gameScore).First();
            await DisplayAlert("Partie terminée !",
                $"🏆 {winner._name} gagne avec {winner._gameScore} pts !\n\n{scores}", "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        // Nouvelle manche
        _game.NewDeck();
        foreach (var p in _turnOrder)
            p.NewBoard(_game.Deck);

        _currentIndex  = 0;
        _mustPlaceCard = false;
        _cardInHand    = null;
        RefreshAll();
    }

    // ── Scores ─────────────────────────────────────────────────────────
    async void OnShowScoresClicked(object sender, EventArgs e)
    {
        foreach (var p in _turnOrder) p.CountRoundScore();
        string scores = string.Join("\n",
            _turnOrder.Select(p => $"{p._name} : {p._roundScore} pts manche | {p._gameScore} pts total"));
        await DisplayAlert("Scores", scores, "Fermer");
    }

    // ── Couleurs cartes ────────────────────────────────────────────────
    static Color CardBgColor(int n) => n switch
    {
        -2          => Color.FromArgb("#1a3a5c"),
        -1          => Color.FromArgb("#12503a"),
        0           => Color.FromArgb("#3a2a5c"),
        >= 1 and <= 4  => Color.FromArgb("#3d2a12"),
        >= 5 and <= 8  => Color.FromArgb("#5c2a12"),
        _           => Color.FromArgb("#5c1212")
    };

    static Color CardTextColor(int n) => n switch
    {
        -2          => Color.FromArgb("#85b7eb"),
        -1          => Color.FromArgb("#5dcaa5"),
        0           => Color.FromArgb("#afa9ec"),
        >= 1 and <= 4  => Color.FromArgb("#ef9f27"),
        >= 5 and <= 8  => Color.FromArgb("#f09575"),
        _           => Color.FromArgb("#f09595")
    };
}
