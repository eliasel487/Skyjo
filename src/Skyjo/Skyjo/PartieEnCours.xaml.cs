using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using ClassLibrary;

namespace Skyjo;

public partial class GamePage : ContentPage
{
    public List<Entry> nomJoueurs = new List<Entry>();

    //objectif : récupérer game qui est créer dans gameSession et qui est utiliser dans CreateGame, pour l'utiliser dans PartieEnCours

    public GameSession CurrentSession => (ContentPage.current as GameSession).CurrentSession;



    public GamePage()
    {
        
        InitializeComponent();
        //objectif : aprés avoir ue l'accées à game dans gameSession il faut écrire le nombre de joueur dans les labels correspondant et afficher le bon nombre de plateau (si y a 2 joueurs on ne vas pas afficher 4 plateaux)
        /*
        if (nomJoueurs[0] != null)
        {
            player1.Text = nomJoueurs[0].Text;
        }
        if (nomJoueurs[1] != null)
        {
            player2.Text = nomJoueurs[1].Text;
        }
        */
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