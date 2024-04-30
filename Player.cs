using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_JustePrix_Serialized
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public List<int> TryedNumbers { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            TryedNumbers = new List<int>();
        }

        public static Player AskPlayerName()
        {
            Console.WriteLine("Quel est votre pseudo ?");
            string name = Console.ReadLine();

            Player player = new Player(name);

            return player;
        }

        public bool AddingAndCheckingTryedList(int parsedEntry)
        {
            bool toReturn = true;

            if (TryedNumbers != null)
            {
                foreach (int number in TryedNumbers)
                {
                    if (number == parsedEntry)
                    {
                        toReturn = false;
                        return toReturn;
                    }
                }
            }

            TryedNumbers.Add(parsedEntry);

            return toReturn;
        }
        public void DisplayTriedNumbers()
        {
            if (TryedNumbers != null)
            {
                Console.Write("Vos essais : ");
                foreach (int number in TryedNumbers)
                {
                    Console.Write(number + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
