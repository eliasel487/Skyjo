
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
    PlayGame();
}

jouerPartie();

void PlayGame() // fait joueur une game
{
    int nbJoueur = game1.Players.Count;
    bool GameOver = game1.GetGameOver();

    while (!GameOver)
    {
        for (int i = 0; i < nbJoueur; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        for (int i = 0; i < game1.Players.Count(); i++)
        {
            if (game1.Players[i]._gameScore >= 100) GameOver = true;
        }

        if (GameOver) break;
        JouerUneManche();

    }
}
void JouerUneManche()
{
    int cpt = 0, temp;
    bool RoundFinished = game1.GetRoundFinished();

    while (!RoundFinished)
    {
        if (cpt == game1.Players.Count()) cpt = 0;
        AfficherLesPlateaux(game1.Players[cpt]);
        if (game1.Players[cpt].RoundOver())
        {
            RoundFinished = true;
            break;
        }
        // le joueur joue à ce moment
        PlayPlayer(game1.Players[cpt]);
        cpt++;
    }
    temp = cpt;
    cpt++;
    while (cpt != temp)
    {
        if (cpt == game1.Players.Count()) cpt = 0;
        if (cpt == temp) break;
        // le joueur joue à ce moment
        PlayPlayer(game1.Players[cpt]);
        cpt++;
    }
    for (int i = 0; i < game1.Players.Count(); i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.Players[i].GetBoard().ReturnACard(x, y);
            }
        }
        AfficherLesPlateaux(game1.Players[i]);
    }
}

void PlayPlayer(Player p) // fait jouer le tour du joueur
{
    int choose, x, y;
    Console.WriteLine("Voulez vous tirer dans le deck ou la défausse (0 : tirer, 1 : défausse) : ");
    choose = Convert.ToInt32(Console.ReadLine());
    Card c = p.PickACard(game1.Deck, choose);
    c.FlipCard();

    Console.WriteLine("Voici la carte tirer : " + c.GetNumber());
    Console.WriteLine("Voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
    choose = Convert.ToInt32(Console.ReadLine());

    if (choose == 0)
    {
        Console.WriteLine("Entrer les coordonée X et Y une par une pour placer votre carte : ");
        y = Convert.ToInt32(Console.ReadLine());
        x = Convert.ToInt32(Console.ReadLine());
        while (x < 0 && x >= 3 && y < 0 && y >= 4)
        {
            Console.WriteLine("Coordoneé incorect ! Entrer les coordoner X et Y une par une pour placer une carte : ");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());
        }
        p.ActionCard(c, choose, x, y);
    }
    else
    {
        Console.WriteLine("Entrer les coordoner X et Y une par une pour retourner une carte face caché : ");
        x = Convert.ToInt32(Console.ReadLine());
        y = Convert.ToInt32(Console.ReadLine());
        while (x < 0 && x >= 3 && y < 0 && y >= 4)
        {
            Console.WriteLine("Coordoner incorect ! Entrer les coordoner X et Y une par une pour retourner une carte face caché : ");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());
        }
        p.ActionCard(c, choose, x, y);
    }
}

void AfficherLesPlateaux(Player p) // affiche le plateau de chaque joueur
{
    int j, k;
    Board b;
    List<Column> columns = new();
    List<Card> cards = new();
    b = p.GetBoard();
    columns = b.GetColumns();
    Console.WriteLine(p._name + ":");
    //Players[i].AfficherBoardPlayer();
    for (k = 0; k < 3; k++)
    {
        Console.WriteLine("---------------------");
        Console.Write("|");
        for (j = 0; j < 4; j++)
        {
            cards = columns[j].GetCards();
            //_columns[i].AfficherColumn();
            if (cards[k].IsFlipped() == false)
                Console.Write(" XX |");
            else
                if (cards[k].GetNumber() < 0 || cards[k].GetNumber() >= 10)
                {
                    Console.Write(" " + cards[k].GetNumber() + " |");
                }
                else
                    Console.Write(" 0" + cards[k].GetNumber() + " |");
        }
        Console.WriteLine();
    }
    Console.WriteLine("---------------------");
}