using System.Runtime.Serialization;
using System.Xml;
using ClassLibrary;

string xmlFile = Path.Combine("XML", "PersistanceGame.xml");
// Pour un seul joueur

/*

Player j1 = new("John Doe", new Deck(), 10, 0);
Console.WriteLine(j1);

Player? j2 = null;

*/
/*
// Pour une collection de joueur

IEnumerable<Player> somePlayers = new List<Player>
{
    new Player("John Doe",new Deck(), 10, 0),
    new Player("John 117",new Deck(), 117, 20),
    new Player("zavala",new Deck(), 3, 5),
    new Player("mathieu rabany",new Deck(), 100, 7)
};

Console.WriteLine("Avant sauvegarde");
foreach (var n in somePlayers)
{
    Console.WriteLine($"\t{n}");
}

var serializer = new DataContractSerializer(typeof(IEnumerable<Player>));

// Sauvegarde
XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
using (TextWriter tw = File.CreateText(xmlFile))
{
    using (XmlWriter writer = XmlWriter.Create(tw, settings))
    {
        serializer.WriteObject(writer, somePlayers);
    }
}

IEnumerable<Player> somePlayers2;

// Chargement
using (Stream s = File.OpenRead(xmlFile))
{
    somePlayers2 = serializer.ReadObject(s) as IEnumerable<Player>;
}

if (somePlayers2 != null)
{
    foreach(var n in somePlayers2)
    {
        Console.WriteLine($"\t{n}");
    }
}
else
    Console.WriteLine("Non");
*/

// Pour une instance de partie qui contient une collection de joueurs

Game partie = new Game();

partie.AddPlayer("Elias");
partie.AddPlayer("Tohem");
partie.AddIA();

Console.WriteLine("Avant sauvegarde");
foreach(var n in partie.Players)
{
    Console.WriteLine($"\t{n._name} - {n._roundScore} - {n._board} - {n._gameScore}");
}
foreach (var i in partie.IAs)
{
    Console.WriteLine($"\t{i._name} - {i._roundScore} - {i._board} - {i._gameScore}");
}

//string file présent au début

var serializer = new DataContractSerializer(typeof(Game));

//Sauvegarde
XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
using ( TextWriter tw = File.CreateText(xmlFile))
{
    using (XmlWriter writer = XmlWriter.Create(tw, settings))
    {
        serializer.WriteObject(writer, partie);
    }
}

Game? p2;

//Chargement
using (Stream s = File.OpenRead(xmlFile))
{
    p2 = serializer.ReadObject(s) as Game;
}

if (p2 != null)
{
    Console.WriteLine("Après sauvegarde");
    foreach (var n in partie.Players)
    {
        Console.WriteLine($"\t{n._name} - {n._roundScore} - {n._board} - {n._gameScore}");
    }
    foreach (var i in partie.IAs)
    {
        Console.WriteLine($"\t{i._name} - {i._roundScore} - {i._board} - {i._gameScore}");
    }
}
else
    Console.WriteLine("Non");