
using ClassLibrary;
using System.Numerics;

Game game1 = new();

void CreerJoueur()
{
    int nbJoueur;
    string nomJoueur;
    Console.WriteLine("Combien de joueur voulez vous créer ? : ");
    nbJoueur = Convert.ToInt32(Console.ReadLine());
    while (nbJoueur <= 0)
    {
        Console.WriteLine("Combien de joueur voulez vous créer ? : ");
        nbJoueur = Convert.ToInt32(Console.ReadLine());
    }
    for (int i = 0; i < nbJoueur; i++)
    {
        Console.WriteLine("Insere le nom du joueur : ");
        nomJoueur = Console.ReadLine();
        game1.AddPlayer(nomJoueur);
    }
}

void CreerIA()
{
    int nbIA;
    Console.WriteLine("Combien d'IA' voulez vous créer ? (0 si vous voulez pas en créer) : ");
    nbIA = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i < nbIA; i++)
    {
        game1.AddIA();
    }
}

void jouerPartie()
{
    CreerJoueur();
    CreerIA();
    PlayGame();
}

jouerPartie();

void PlayGame() // fait joueur une game
{
    bool GameOver = game1.GetGameOver();

    while (!GameOver)
    {
        for (int i = 0; i < game1.Players.Count; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        for (int i = 0; i < game1.IAs.Count; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        for (int i = 0; i < game1.Players.Count(); i++)
        {
            if (game1.Players[i]._gameScore >= 100) GameOver = true;
        }

        for (int i = 0; i < game1.IAs.Count(); i++)
        {
            if (game1.IAs[i]._gameScore >= 100) GameOver = true;
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
        if (cpt == game1.Players.Count() + game1.IAs.Count) cpt = 0;
        AfficherLesPlateaux(game1.Players, game1.IAs);
        if (cpt < game1.Players.Count)
        {
            if (game1.Players[cpt].RoundOver())
            {
                RoundFinished = true;
                break;
            }
        }
        else
        {
            if (game1.IAs[cpt - game1.Players.Count].RoundOver())
            {
                RoundFinished = true;
                break;
            }
        }
        // le joueur joue à ce moment
        if (cpt < game1.Players.Count)
            PlayPlayer(game1.Players[cpt]);
        else
            game1.IAs[cpt - game1.Players.Count].PlayRound(game1.GetDeck());
        cpt++;
    }
    temp = cpt;
    cpt++;
    while (cpt != temp)
    {
        if (cpt == game1.Players.Count() + game1.IAs.Count) cpt = 0;
        if (cpt == temp) break;
        // le joueur joue à ce moment
        if (cpt < game1.Players.Count)
            PlayPlayer(game1.Players[cpt]);
        else
            game1.IAs[cpt - game1.Players.Count].PlayRound(game1.GetDeck());
        cpt++;
    }
    // on retourne toutes les cartes à la fin d'une manche pour pouvoir voir ce que les autres avaient comme cartes
    for (int i = 0; i < game1.Players.Count(); i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.Players[i].GetBoard().ReturnACard(x, y);
            }
        }
    }
    for (int i = 0; i < game1.IAs.Count(); i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.IAs[i].GetBoard().ReturnACard(x, y);
            }
        }
    }

    AfficherLesPlateaux(game1.Players, game1.IAs);
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

void AfficherLesPlateaux(List<Player> p, List<IA> ia) // affiche le plateau de chaque joueur
{
    int j, k;
    Board b;
    List<Column> columns = new();
    List<Card> cards = new();
    for (int i = 0; i < p.Count; i++)
    {
        b = p[i].GetBoard();
        columns = b.GetColumns();
        Console.WriteLine(p[i]._name + ":");
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
    for (int i = 0; i < ia.Count; i++)
    {
        b = ia[i].GetBoard();
        columns = b.GetColumns();
        Console.WriteLine(ia[i]._name + ":");
        for (k = 0; k < 3; k++)
        {
            Console.WriteLine("---------------------");
            Console.Write("|");
            for (j = 0; j < 4; j++)
            {
                cards = columns[j].GetCards();
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
}