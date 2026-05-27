using Microsoft.Maui.Controls;
using ClassLibrary;

namespace Skyjo;

public partial class GamePage : ContentPage
{
    public List<Entry> nomJoueurs = new List<Entry>();
    public GamePage(List<Entry> nomJoueurs)
    {
        this.nomJoueurs = nomJoueurs;
    }
    public Game game1 { get; set; }
        = new Game();

    


    public GamePage()
    {
        InitializeComponent();
        game1.AddPlayer(nomJoueurs[0].Text);
    }

    private async void OnDrawCardClicked(object sender, EventArgs e)
    {
        // TODO: logique pioche
        PlayerStatusLabel.Text = "Carte tirée — échangez ou défaussez";
        await DisplayAlert("Piocher", "Tu pioches une carte !", "OK");
    }

    private async void OnTakeDiscardClicked(object sender, EventArgs e)
    {
        // TODO: logique prise défausse
        PlayerStatusLabel.Text = "Tu prends la défausse — choisis une carte";
        await DisplayAlert("Défausse", "Tu prends la carte de la défausse !", "OK");
    }

    private async void OnShowScoresClicked(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Scores de la manche",
            "Lucas : 14 pts\nMarie : 8 pts\nThomas : 21 pts\nToi : 12 pts",
            "Fermer");
    }
}