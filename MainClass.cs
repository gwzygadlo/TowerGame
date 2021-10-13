using System;

namespace TowerGame
{
    class MainClass
    {

        static void Main(string[] args)
        {
            TowerGame myGame = new TowerGame();
            TowerClass T1 = new TowerClass(myGame, 3);
            TowerClass T2 = new TowerClass(myGame, 3);
            TowerClass T3 = new TowerClass(myGame, 3);

            TowerClass[] AllTowersArranged = { T1, T2, T3 };

            Console.WriteLine("Welcome to the Tower Game!\r\n");

            T1.PrintTower(AllTowersArranged);



            int counter = 0;
            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = T1.PlayTowerGame(AllTowersArranged);
                if (!isSorted)
                {
                    counter++;
                    T1.PrintTower(AllTowersArranged);
                }
            }
            Console.WriteLine("You completed the puzzle in " + counter + " move(s).");
        }

    }
}


