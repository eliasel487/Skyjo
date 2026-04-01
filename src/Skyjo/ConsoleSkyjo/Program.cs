
using ClassLibrary;

Card testc = new Card(5);

Console.WriteLine(testc.ToString());

Deck testd = new Deck();

Console.WriteLine(testd.ToString());

for (int i = 0; i < 75; i++)
{
    testd.DiscradACard(testd.PickACard(0));
}

Console.WriteLine(testd.ToString());