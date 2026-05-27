
using ClassLibrary;
using System.Net.Mail;
using System.Numerics;

Game game1 = new();

int CreerJoueur()
{
    string nbJoueurStr;
    int nbJoueur;
    string nomJoueur;
    Console.WriteLine("Combien de joueur voulez vous créer ? (entre 1 et 4) : ");

    nbJoueurStr = Console.ReadLine()!;
    while (nbJoueurStr != "1" && nbJoueurStr != "2" && nbJoueurStr != "3" && nbJoueurStr != "4")
    {
        Console.WriteLine("Vous n'avez pas mis un chiffre entre 1 et 4, combien de joueur voulez vous créer ? (entre 1 et 4) : ");
        try
        {
            nbJoueurStr = Console.ReadLine()!;
        }
        catch (System.ArgumentNullException)
        {
            Console.WriteLine("Vous n'avez rien mis, recommencez");
            nbJoueurStr = "";
        }
    }
    nbJoueur = Convert.ToInt32(nbJoueurStr);
    for (int i = 0; i < nbJoueur; i++)
    {
        Console.WriteLine("Insere le nom du joueur numero " + (i+1) + " : ");
        nomJoueur = Console.ReadLine()!;
        if (nomJoueur == "") nomJoueur = "Joueur " + (i+1);        
        game1.AddPlayer(nomJoueur);
    }
    return nbJoueur;
}

void CreerIA(int nbJoueur)
{
    if (nbJoueur != 4)
    {
        int nbIA;
        Console.WriteLine("Combien d'IA' voulez vous créer ? (0 si vous voulez pas en créer) : ");
        try
        {
            nbIA = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.FormatException)
        {
            Console.WriteLine("Vous avez mis une (ou plusieurs) lettres, recommencez.");
            nbIA = -1024;
        }
        catch (System.ArgumentNullException)
        {
            Console.WriteLine("Vous n'avez rien mis, recommencez.");
            nbIA = -1024;
        }

        while (nbIA > 4 - nbJoueur || nbIA == -1024) 
        {
            if (nbIA == -1024)
                Console.WriteLine("Combien d'IA' voulez vous créer ? (0 si vous voulez pas en créer) : ");
            else Console.WriteLine("Vous avez mis trop d'IA , combien d'Ia voulez vous créer ? (0 si vous voulez pas en créer) : ");
            try
            {
                nbIA = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Vous avez mis une (ou plusieurs lettres, recommencez.");
                nbIA = -1024;
            }
            catch (System.ArgumentNullException)
            {
                Console.WriteLine("Vous n'avez rien mis, recommencez.");
                nbIA = -1024;
            }
    }
        if (nbIA > 0)
            for (int i = 0; i < nbIA; i++)
            {
                game1.AddIA();
            }
    }
}

void jouerPartie()
{
    int nbJoueur = CreerJoueur();
    CreerIA(nbJoueur);
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

        for (int i = 0; i < game1.Players.Count; i++)
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
        if (cpt == game1.Players.Count + game1.IAs.Count) cpt = 0;
        AfficherLesPlateaux(game1.Players, game1.IAs);
        if (cpt < game1.Players.Count)
        {
            if (game1.Players[cpt].RoundOver())
            {
                break;
            }
        }
        else
        {
            if (game1.IAs[cpt - game1.Players.Count].RoundOver())
            {
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
        if (cpt == game1.Players.Count + game1.IAs.Count) cpt = 0;
        if (cpt == temp) break;
        // le joueur joue à ce moment
        if (cpt < game1.Players.Count)
            PlayPlayer(game1.Players[cpt]);
        else
            game1.IAs[cpt - game1.Players.Count].PlayRound(game1.GetDeck());
        cpt++;
    }
    // on retourne toutes les cartes à la fin d'une manche pour pouvoir voir ce que les autres avaient comme cartes
    for (int i = 0; i < game1.Players.Count; i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.Players[i].GetBoard().ReturnACard(x, y);
            }
        }
    }
    for (int i = 0; i < game1.IAs.Count; i++)
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
    string choose;
    int chooseInt, x, y;
    Console.WriteLine("Tour de '" + p._name + "' :");
    if (!game1.Deck.IsEmptyDiscard() && !game1.Deck.IsEmptyPull())
    {
        Console.WriteLine("Voulez vous tirer dans le deck ou la défausse (0 : tirer, 1 : défausse) : ");
        choose = Console.ReadLine()!;
        while (choose != "0" && choose != "1")
        {
            Console.WriteLine("Vous n'avez pas mis 0 ou 1, voulez vous tirer dans le deck ou la défausse (0 : tirer, 1 : défausse) : ");
            choose = Console.ReadLine()!;
        }
        chooseInt = Convert.ToInt32(choose);
    }
    else if (game1.Deck.IsEmptyDiscard()) chooseInt = 0;
    else chooseInt = 1;
    Card c = Player.PickACard(game1.Deck, chooseInt);
    c.FlipCard();

    Console.WriteLine("Voici la carte tirée : " + c.GetNumber());
    Console.WriteLine("Voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
    choose = Console.ReadLine()!;
    while (choose != "0" && choose != "1")
    {
        Console.WriteLine("Vous n'avez pas mis 0 ou 1, voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
        choose = Console.ReadLine()!;
    }

    chooseInt = Convert.ToInt32(choose);

    if (chooseInt == 0)
    {
        Console.WriteLine("Entrer les coordonées X et Y une par une pour placer votre carte : ");
        try
        {
            y = Convert.ToInt32(Console.ReadLine());
            x = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.FormatException)
        {
            x = -1;
            y = -1;

        }
        while (x < 0 && x >= 3 && y < 0 && y >= 4)
        {
            Console.WriteLine("Coordoneés incorrect ! Entrez les coordonées X et Y une par une pour placer une carte : ");
            try
            {
                y = Convert.ToInt32(Console.ReadLine());
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                x = -1;
                y = -1;

            }
        }
    }
    else
    {
        Console.WriteLine("Entrer les coordonées X et Y une par une pour retourner une carte face cachée : ");
        try
        {
            y = Convert.ToInt32(Console.ReadLine());
            x = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.FormatException)
        {
            x = -1;
            y = -1;

        }
        while (x < 0 && x >= 3 && y < 0 && y >= 4)
        {
            Console.WriteLine("Coordonées incorrect ! Entrer les coordonées X et Y une par une pour retourner une carte face cachée : ");
            try
            {
                y = Convert.ToInt32(Console.ReadLine());
                x = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.FormatException)
            {
                x = -1;
                y = -1;

            }
        }
        game1.Deck.DiscardACard(c);
    }
    p.ActionCard(c, chooseInt, x, y);
}

void AfficherLesPlateaux(List<Player> p, List<IA> ia) // affiche le plateau de chaque joueur
{
    int j, k;
    Board b;
    List<Column> columns;
    List<Card> cards;
    Console.Clear();
    for (int i = 0; i < p.Count; i++)
    {
        b = p[i].GetBoard();
        columns = b.GetColumns();
        Console.WriteLine(p[i]._name + ":");
        for (k = 0; k < 3; k++)
        {
            Console.WriteLine("---------------------");
            Console.Write("|");
            for (j = 0; j < 4; j++)
            {
                cards = columns[j].GetCards();
                if (!cards[k].IsFlipped())
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
        Console.WriteLine("---------------------\n");
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
                if (!cards[k].IsFlipped())
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
        Console.WriteLine("---------------------\n");
    }
}