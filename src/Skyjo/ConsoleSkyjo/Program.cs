using ClassLibrary;
using System.Net.Mail;
using System.Numerics;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Xml;

Game game1 = new();

// Persistance — déclarer AVANT toute utilisation
string xmlFile = Path.Combine("XML", "PersistanceGame.xml");
var serializer = new DataContractSerializer(typeof(Game));


/**
    * @brief Création d'un ou plusieurs joueurs avec saisie de nom
*/
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

/**
    * @brief Création d'une ou plusieures IA
    * @param nbJoueur le nombre de joueurs réels jouant dans cette partie
*/
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


/**
    * @brief Démarrage d'une partie déjà existante ou création d'une nouvelle partie
*/
void jouerPartie()
{
    string choose;

    Console.WriteLine("Voulez vous lancer une partie normale (1) ou une partie déja faite (2) ?");
    choose = Console.ReadLine();

    if (choose != "2")
    {
        if (xmlFile != null && File.Exists(xmlFile))
        {
            Console.WriteLine("Il existe une sauvegarde, voulez vous la lancer ?");
            choose = Console.ReadLine();
            if (choose == "yes" || choose == "y" || choose == "oui" || choose == "o" || choose == "Yes" || choose == "Y" || choose == "Oui" || choose == "O")
                load();
            else
            {
                int nbJoueur = CreerJoueur();
                CreerIA(nbJoueur);
            }
        }
        else
        {
        }
        PlayGame();
    }
    else { Stub.Stub stub = new(); }
}

jouerPartie();

/**
    * @brief Fais jouer une partie de Skyjo
*/
void PlayGame() 
{
    bool GameOver = game1.GameOver;
    List<int> pts = [];
    int idmin = 0;

    while (!GameOver)
    {
        pts.Clear();
        for (int i = 0; i < game1.Players.Count; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        for (int i = 0; i < game1.IAs.Count; i++) // créer un nouveau plateau au début de chaque tour
        {
            game1.Players[i].NewBoard(game1.Deck);
        }

        game1.Deck.DiscardACard(game1.Deck.PickACard(0));

        for (int i = 0; i < game1.Players.Count; i++)
        {
            pts.Add(game1.Players[i].FlipTwoCard());
        }

        for (int i = 0; i < game1.IAs.Count; i++)
        {
            game1.IAs[i].FlipTwoCard();
        }

        for (int i = 0; i < game1.Players.Count; i++)
        {
            if (game1.Players[i].GameScore >= 100) GameOver = true;
        }

        for (int i = 0; i < game1.IAs.Count(); i++)
        {
            if (game1.IAs[i].GameScore >= 100) GameOver = true;
        }

        if (GameOver) break;
        for (int i = 0; i < pts.Count; i++)
        {
            if (pts[i] < pts[idmin]) idmin = i;
        }
        JouerUneManche(idmin);
        save();
    }
    
    Console.Clear();
    int idWinner = 0;
    for (int i = 0;i < game1.Players.Count; i++)
    {
        Console.WriteLine("Score " + game1.Players[i].Name + " : " + game1.Players[i].GameScore);
        if (game1.Players[i].GameScore > game1.Players[idWinner].GameScore) idWinner = i;
    }

    Console.WriteLine("Le vainqueur est donc " + game1.Players[idWinner].Name);
}


/**
    * @brief Fais jouer une manche de Skyjo (1 tour équivaut à l'ensemble des joueurs ayant joué)
    * @param idPremier identifiant (modélisé par un nombre) du joueur qui commence la manche
*/
void JouerUneManche(int idPremier)
{
    int cpt = idPremier, temp;
    bool RoundFinished = game1.RoundFinished;

    while (!RoundFinished)
    {
        Console.Clear();
        for (int i = 0; i < game1.Players.Count; i++)
        {
            game1.Players[i].RemoveColumn();
            game1.Players[i].CountRoundScore();
        }
        if (cpt == game1.Players.Count + game1.IAs.Count) cpt = 0;
        AfficherLesPlateaux(game1.Players, game1.IAs);
        // le joueur joue à ce moment
        if (cpt < game1.Players.Count)
            PlayPlayer(game1.Players[cpt]);
        else
            game1.IAs[cpt - game1.Players.Count].PlayRound(game1.Deck);
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
        cpt++;
    }
    temp = cpt;
    cpt++;
    while (cpt != temp)
    {
        Console.Clear();
        for (int i = 0; i < game1.Players.Count; i++)
        {
            game1.Players[i].RemoveColumn();
            game1.Players[i].CountRoundScore();
        }
        AfficherLesPlateaux(game1.Players, game1.IAs);
        if (cpt == game1.Players.Count + game1.IAs.Count) cpt = 0;
        if (cpt == temp) break;
        // le joueur joue à ce moment
        if (cpt < game1.Players.Count)
            PlayPlayer(game1.Players[cpt]);
        else
            game1.IAs[cpt - game1.Players.Count].PlayRound(game1.Deck);
        cpt++;
    }
    // on retourne toutes les cartes à la fin d'une manche pour pouvoir voir ce que les autres avaient comme cartes
    for (int i = 0; i < game1.Players.Count; i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.Players[i].Board.ReturnACard(x, y);
            }
        }
    }
    for (int i = 0; i < game1.IAs.Count; i++)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                game1.IAs[i].Board.ReturnACard(x, y);
            }
        }
    }

    Console.Clear();
    AfficherLesPlateaux(game1.Players, game1.IAs);
    Console.Read();
}

/**
    * @brief Donne les possibilités de jeu au joueur qui doit jouer son tour
    * @param p Le joueur qui doit jouer son tour
*/
void PlayPlayer(Player p)
{
    string choose;
    int chooseInt, x, y;
    Console.WriteLine("Tour de '" + p.Name + "' :");
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

    if (chooseInt == 0)
    {
        Console.WriteLine("Voici la carte tirée : " + c.Number);
        Console.WriteLine("Voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
        choose = Console.ReadLine()!;
        while (choose != "0" && choose != "1")
        {
            Console.WriteLine("Vous n'avez pas mis 0 ou 1, voulez vous placer votre carte ou la défausser (0 : placer, 1 : défausser) : ");
            choose = Console.ReadLine()!;
        }

        chooseInt = Convert.ToInt32(choose);
    }

    else
    {
        Console.WriteLine("La carte de la défausse est : " + c.Number);
        chooseInt = 0;
    }
    
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
        if ((y >= 0 && y < 5) && !p.Board.GetColumns()[y].GetValued())
        {
            Console.WriteLine("Cette colonne à été suprimée.");
            x = -1;
            y = -1;
        }
        while (x < 0 || x >= 3 || y < 0 || y >= 4)
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
            if ((y >= 0 && y < 5) && !p.Board.GetColumns()[y].GetValued())
            {
                Console.WriteLine("Cette colonne à été suprimée.");
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
        if (!p.Board.GetColumns()[y].GetValued())
        {
            Console.WriteLine("Cette colonne à été suprimée.");
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
            if (!p.Board.GetColumns()[y].GetValued())
            {
                Console.WriteLine("Cette colonne à été suprimée.");
                x = -1;
                y = -1;
            }
        }
        game1.Deck.DiscardACard(c);
    }
    p.ActionCard(game1.Deck, c, chooseInt, x, y);
}

/**
    * @brief Création d'un ou plusieurs joueurs avec saisie de nom
    * @param p Une liste de joueurs à qui on va afficher leur plateau (cartes disposées de manières ordonnées)
    * @param ia Une liste d'IA auxquelles on va afficher leur plateau
*/
void AfficherLesPlateaux(List<Player> p, List<IA> ia) // affiche le plateau de chaque joueur
{
    int j, k;
    Board b;
    List<Column> columns;
    List<Card> cards;
    for (int i = 0; i < p.Count; i++)
    {
        b = p[i].Board;
        columns = b.GetColumns();
        Console.WriteLine(p[i].Name + " (Game Score = " + p[i].GameScore + ", Round Score = " + p[i].RoundScore +") :");
        for (k = 0; k < 3; k++)
        {
            Console.WriteLine("---------------------");
            Console.Write("|");
            for (j = 0; j < 4; j++)
            {
                cards = columns[j].GetCards();
                if (!columns[j].GetValued()) Console.Write(" // |");
                else if (!cards[k].IsFlipped()) Console.Write(" XX |");
                else
                if (cards[k].Number < 0 || cards[k].Number >= 10)
                {
                    Console.Write(" " + cards[k].Number + " |");
                }
                else Console.Write(" 0" + cards[k].Number + " |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------------\n");
    }
    for (int i = 0; i < ia.Count; i++)
    {
        b = ia[i].Board;
        columns = b.GetColumns();
        Console.WriteLine(ia[i].Name + ":");
        for (k = 0; k < 3; k++)
        {
            Console.WriteLine("---------------------");
            Console.Write("|");
            for (j = 0; j < 4; j++)
            {
                cards = columns[j].GetCards();
                if (!columns[j].GetValued()) Console.Write(" // |");
                else if (!cards[k].IsFlipped()) Console.Write(" XX |");
                else
                if (cards[k].Number < 0 || cards[k].Number >= 10)
                {
                    Console.Write(" " + cards[k].Number + " |");
                }
                else Console.Write(" 0" + cards[k].Number + " |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("---------------------\n");
    }

    Console.WriteLine(game1.Deck.GetFirstCardDiscard() + "\n");
}

// Persistance


/**
    * @brief Sauvegarde d'une partie dans un ficher .xml
*/
void save()
{
    XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
    using (TextWriter tw = File.CreateText(xmlFile))
    {
        using (XmlWriter writer = XmlWriter.Create(tw, settings))
        {
            serializer.WriteObject(writer, game1);
        }
    }
}

/**
    * @brief Chargement d'une partie enregistrée dans un ficher .xml
*/
void load()
{
    Game? p2;

    using (Stream s = File.OpenRead(xmlFile))
    {
        p2 = serializer.ReadObject(s) as Game;
    }

    if (p2 != null)
    {
        game1 = p2; // <- correction essentielle : remplacer l'instance vide par l'état chargé
        Console.WriteLine("Après sauvegarde");
        foreach (var n in game1.Players)
        {
            Console.WriteLine($"\t{n.Name} - {n.RoundScore} - {n.Board} - {n.GameScore}");
        }
        foreach (var i in game1.IAs)
        {
            Console.WriteLine($"\t{i.Name} - {i.RoundScore} - {i.Board} - {i.GameScore}");
        }
    }
    else
        Console.WriteLine("Non");
}