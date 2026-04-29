
using ClassLibrary;
using System.Numerics;

Game game1 = new();

void CreerJoueur()
{
    int nbJoueur;
    string nomJoueur;
    Console.WriteLine("Combien de joueur voulez vous créer ? : ");
    nbJoueur = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i < nbJoueur; i++)
    {
        Console.WriteLine("Insere le nom du joeur : ");
        nomJoueur = Console.ReadLine();
        game1.AddPlayer(nomJoueur);
    }
}

void jouerPartie()
{
    CreerJoueur();
    game1.PlayGame();
}

jouerPartie();

