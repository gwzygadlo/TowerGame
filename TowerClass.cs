using System;
using System.Collections;

namespace TowerGame
{
    public class TowerClass
    {
        public int towerSize { get; set; }
        public ArrayList elementsInTower = new ArrayList();
        static int redCount = 0;
        static int greenCount = 0;
        static int yellowCount = 0;

        public TowerClass(int size)
        {
            towerSize = size;
            elementsInTower = RandomLettersGenerator(size);
        }

        public ArrayList RandomLettersGenerator(int size)
        {
            int towerHeight = size;
            string chars = "RGY";
            ArrayList randomLetterList = new ArrayList();
            var random = new Random();
            var index = 0;
            for (int i = 0; i < towerHeight; i++)
            {
                index = random.Next(0, chars.Length);
                char chosenRandomChar = chars[index];

                if (chosenRandomChar.Equals('R'))
                {
                    redCount++;
                    if (redCount > towerHeight)
                    {
                        bool isLargerRed = true;
                        while (isLargerRed)
                        {
                            index = random.Next(0, chars.Length);
                            chosenRandomChar = chars[index];

                            if (!chosenRandomChar.Equals('R'))
                            {
                                isLargerRed = false;
                            }
                        }
                    }
                }
                if (chosenRandomChar.Equals('G'))
                {
                    greenCount++;
                    if (greenCount > towerHeight)
                    {
                        bool isLargerGreen = true;
                        while (isLargerGreen)
                        {
                            index = random.Next(0, chars.Length);
                            chosenRandomChar = chars[index];

                            if (!chosenRandomChar.Equals('G'))
                            {
                                isLargerGreen = false;
                            }
                        }
                    }
                }
                if (chosenRandomChar.Equals('Y'))
                {
                    yellowCount++;
                    if (yellowCount > towerHeight)
                    {
                        bool isLargerYellow = true;
                        while (isLargerYellow)
                        {
                            index = random.Next(0, chars.Length);
                            chosenRandomChar = chars[index];

                            if (!chosenRandomChar.Equals('Y'))
                            {
                                isLargerYellow = false;
                            }
                        }
                    }
                }
                randomLetterList.Insert(i, chosenRandomChar);
            }
            return randomLetterList;
        }

        public void PrintTower(TowerClass[] towersToPrint)
        {
            Console.Write("\r\n");
            for (int i = 0; i < towersToPrint.Length; i++)
            {
                Console.WriteLine("Tower " + i);
                for (int j = 0; j < towersToPrint[i].elementsInTower.Count; j++)
                {
                    Console.WriteLine(towersToPrint[i].elementsInTower[j]);
                }
            }
            Console.Write("\r\n");

            /* My Move Method broke this because this printing method assumes that
             * all arrays of elements in each tower are the same length but they are not
             * I tried to tweak it to include the fact that they are not all the same length by adding the
             * in element total line but that didn't help the loop
             *
            for (int i = 0; i < towersToPrint.Length; i++)
            {
                Console.Write("Tower " + i + "\t");
                if (i >= towersToPrint.Length - 1)
                {
                    Console.Write("\r\n");
                }
            }
            for (int i = 0; i < towersToPrint.Length; i++)
            {
                int elementTotal = towersToPrint[i].elementsInTower.Count;
                Console.WriteLine("total elements for tower "+ i + " = " + elementTotal);
                for (int j = 0; j < towersToPrint[i].elementsInTower.Count; j++)
                {
                    Console.Write(towersToPrint[j].elementsInTower[i] + "\t");
                    if (j >= towersToPrint.Length - 1)
                    {
                        Console.Write("\r\n");
                    }
                }
            }
            */
        }

        public bool PlayTowerGame(TowerClass[] allTowersArranged)
        {
            bool isCompleted = false;
            int[] userInput = new int[3];
            isCompleted = CheckIfSorted(allTowersArranged);
            if (isCompleted)
            {
                return isCompleted;
            }
            else
            {
                userInput = AskUserForMoves(allTowersArranged);
                MoveDisksMethod(allTowersArranged, userInput);
                return isCompleted;
            }
        }

        public bool CompareChars(char a, char b)
        {
            bool isEqual = false;
            if (a.Equals(b))
            {
                isEqual = true;
            }
            return isEqual;
        }

        public bool CheckIfSorted(TowerClass[] allTowersArranged)
        {
            bool isSorted = false;
            bool CheckForEach = false;
            bool CheckEqualChars = false;

            //for each tower
            for (int i = 0; i < allTowersArranged.Length; i++)
            {
                //in an individual tower's chars
                for (int j = 0; j < allTowersArranged[i].elementsInTower.Count; j++)
                {
                    if (allTowersArranged[i].elementsInTower.Count == 1 || allTowersArranged[i].elementsInTower.Count == 0)
                    {
                        CheckEqualChars = true;
                        CheckForEach = CheckEqualChars;
                        break;
                    }
                    if (j >= allTowersArranged[i].elementsInTower.Count - 1)
                    {
                        CheckEqualChars = CompareChars((char)allTowersArranged[i].elementsInTower[j], (char)allTowersArranged[i].elementsInTower[j - 1]);
                        if (!CheckEqualChars)
                        {
                            CheckForEach = CheckEqualChars;
                            break;
                        }
                    }
                    else
                    {
                        CheckEqualChars = CompareChars((char)allTowersArranged[i].elementsInTower[j], (char)allTowersArranged[i].elementsInTower[j + 1]);
                        if (!CheckEqualChars)
                        {
                            CheckForEach = CheckEqualChars;
                            break;
                        }
                    }
                }
                CheckForEach = CheckEqualChars;
                if (!CheckForEach)
                {
                    break;
                }
            }
            isSorted = CheckForEach;
            return isSorted;
        }

        public int[] AskUserForMoves(TowerClass[] allTowersArranged)
        {
            int[] userChoices = new int[3];

            bool isInt2 = false;
            while (!isInt2)
            {
                Console.WriteLine("\r\nPlease enter the tower to move the disks from: ");
                string input = Console.ReadLine();
                try
                {
                    int towerToMoveFrom = Int32.Parse(input);
                    if (towerToMoveFrom < 0 || towerToMoveFrom >= 3)
                    {
                        Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                            $"Please enter an integer between 0 and 2 inclusive.\r\n");
                        isInt2 = false;
                    }
                    else
                    {
                        userChoices[1] = towerToMoveFrom;
                        isInt2 = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                        $"Please enter an integer between 0 and 2 inclusive.\r\n");
                }
            }

            bool isInt3 = false;
            while (!isInt3)
            {
                Console.WriteLine("Please enter the tower to move the disks to: ");
                string input = Console.ReadLine();
                try
                {
                    int towerToMoveTo = Int32.Parse(input);
                    if (towerToMoveTo < 0 || towerToMoveTo >= 3 || towerToMoveTo == userChoices[1])
                    {
                        Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                            $"Please enter an integer between 0 and 2 inclusive.\r\n" +
                            $"Tower to move to cannot be the same as tower to move from.\r\n");
                        isInt3 = false;
                    }
                    else
                    {
                        userChoices[2] = towerToMoveTo;
                        isInt3 = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                            $"Please enter an integer between 0 and 2 inclusive.\r\n" +
                            $"Tower to move to cannot be the same as tower to move from.\r\n");
                }
            }

            bool isInt = false;
            while (!isInt)
            {
                Console.WriteLine("Please enter the amount of disks to be moved: ");
                string input = Console.ReadLine();
                try
                {
                    int disksToMove = Int32.Parse(input);
                    if (disksToMove <= 0 || disksToMove > allTowersArranged[userChoices[1]].elementsInTower.Count)
                    {
                        Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                            $"Please enter an integer between 1 and the amount of disks inclusive: " + allTowersArranged[userChoices[1]].elementsInTower.Count + ".\r\n");
                        isInt = false;
                    }
                    else
                    {
                        userChoices[0] = disksToMove;
                        isInt = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"This is an invalid input: '{input}'.\r\n" +
                        $"Please enter an integer between 1 and the amount of disks inclusive " + allTowersArranged[userChoices[1]].elementsInTower.Count + ".\r\n");
                }
            }

            return userChoices;
        }

        public void MoveDisksMethod(TowerClass[] allTowersArranged, int[] userInput)
        {
            int numberDisksToMove = userInput[0];
            int towerToMoveFrom = userInput[1];
            int towerToMoveTo = userInput[2];
            ArrayList disksToMove = new ArrayList();
            for (int i = 0; i < numberDisksToMove; i++)
            {
                char moved = (char)allTowersArranged[towerToMoveFrom].elementsInTower[i];
                disksToMove.Insert(i, moved);
            }
            allTowersArranged[towerToMoveTo].elementsInTower.InsertRange(0, disksToMove);
            allTowersArranged[towerToMoveFrom].elementsInTower.RemoveRange(0, numberDisksToMove);
        }
    }
}