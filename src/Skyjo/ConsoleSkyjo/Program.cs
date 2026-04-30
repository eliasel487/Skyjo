
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

void PlayGame()
{
    int cpt, temp, nbJoueur = game1.Players.Count;
    bool RoundFinished = game1.GetRoundFinished(), GameOver = game1.GetGameOver();


    while (!GameOver)
    {
        for (int i = 0; i < nbJoueur; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        RoundFinished = false;
        for (int i = 0; i < game1.Players.Count(); i++)
        {
            if (game1.Players[i]._gameScore >= 100) GameOver = true;
        }

        if (GameOver) break;

        cpt = 0;
        while (!RoundFinished)
        {
            game1.AfficherGame(nbJoueur);
            if (cpt == game1.Players.Count()) cpt = 0;
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
            // le joueur joue à ce moment
            PlayPlayer(game1.Players[cpt]);
            cpt++;
        }
    }
}

void PlayPlayer(Player p)
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
        Console.WriteLine("Entrer les coordoner X et Y une par une pour placer votre carte : ");
        x = Convert.ToInt32(Console.ReadLine());
        y = Convert.ToInt32(Console.ReadLine());
        while (x >= 0 && x < 3 && y >= 0 && y < 4)
        {
            Console.WriteLine("Coordoner incorect ! Entrer les coordoner X et Y une par une pour placer une carte : ");
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
        while (x >= 0 && x < 3 && y >= 0 && y < 4 )
        {
            Console.WriteLine("Coordoner incorect ! Entrer les coordoner X et Y une par une pour retourner une carte face caché : ");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());
        }
        p.ActionCard(c, choose, x, y);
    }
}

void AfficherLesPlateaux()
{
    int i;
    Board b;
    List<Column> _columns = new();
    List<Card> _cards = new();
    for (i = 0; i < game1.Players.Count; i++)
    {
        b = game1.Players[i].GetBoard();
        _columns = b.GetColumns();
        //Players[i].AfficherBoardPlayer();
        for (i = 0; i < 4; i++)
        {
            _columns[i].GetCards();
            //_columns[i].AfficherColumn();
            for (i = 0; i < 3; i++)
            {
                if (_cards[i].IsFlipped() == false)
                    Console.Write(" X ");
                else
                    Console.Write(" " + _cards[i].GetNumber() + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}